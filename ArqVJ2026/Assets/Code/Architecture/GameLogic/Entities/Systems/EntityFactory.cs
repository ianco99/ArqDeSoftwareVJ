using ianco99.ToolBox.Services;
using ZooArchitect.Architecture.Math;
using System.Reflection;
using System;
using System.Collections.Generic;
using ianco99.ToolBox.Events;
using ZooArchitect.Architecture.Entities.Events;
using ianco99.ToolBox.Blueprints;
using ianco99.ToolBox.Bluprints;

namespace ZooArchitect.Architecture.Entities
{
    public sealed class EntityFactory : IService
    {
        private EventBus EventBus => ServiceProvider.Instance.GetService<EventBus>();
        private EntityRegistry EntityRegistry => ServiceProvider.Instance.GetService<EntityRegistry>();
        private BlueprintBinder BlueprintBinder => ServiceProvider.Instance.GetService<BlueprintBinder>();
        private uint lastAssignedEntityId;
        public bool IsPersistance => false;

        private Dictionary<Type, ConstructorInfo> entityConstructors;
        private MethodInfo RegisterEntityMethod;
        private MethodInfo raiseEntityCreatedMethod;

        public EntityFactory()
        {
            this.lastAssignedEntityId = Entity.UNASSIGNED_ENTITY_ID + 1;
            entityConstructors = new Dictionary<Type, ConstructorInfo>();

            if (EntityRegistry == null)
            {
                throw new NullReferenceException("Accessed Registry before creation");
            }

            RegisterEntityMethod = EntityRegistry.GetType().GetMethod(EntityRegistry.RegisterMethodName, BindingFlags.NonPublic | BindingFlags.Instance);

            raiseEntityCreatedMethod = GetType().GetMethod(nameof(RaiseEntityCreated), BindingFlags.NonPublic | BindingFlags.Instance);

            //TODO: Consult Leandro
            // RegisterEntityMethods<Animal>();
            // CreateInstance<Animal>(new Coordinate(0, 0));
        }

        public void CreateInstance<EntityType>(Coordinate coordinate) where EntityType : Entity
        {
            lastAssignedEntityId++;
            uint newEntityId = lastAssignedEntityId;

            if (!entityConstructors.ContainsKey(typeof(EntityType)))
            {
                throw new MissingMethodException($"Missing constructor for {typeof(EntityType).Name}");
            }

            object newEntity = entityConstructors[typeof(EntityType)].Invoke(new object[] { newEntityId, coordinate });

            //TODO: Consult Leandro
            //BlueprintBinder.Apply(ref newEntity, "Animals", "Monkey");

            if (RegisterEntityMethod == null)
            {
                throw new MissingMethodException($"Missing EntityRegistry register method");
            }

            RegisterEntityMethod.Invoke(EntityRegistry, new object[] { newEntity });

            List<Type> entityTypes = new List<Type>();
            Type currentType = null;

            do
            {
                currentType = currentType == null ? newEntity.GetType() : currentType.BaseType;
                entityTypes.Add(currentType);
            } while (currentType != typeof(Entity));

            for (int i = entityTypes.Count - 1; i >= 0; i--)
            {
                raiseEntityCreatedMethod.MakeGenericMethod(entityTypes[i]).Invoke(this, new object[] { newEntity });
            }
        }

        private void RaiseEntityCreated<EntityType>(EntityType newEntity) where EntityType : Entity
        {
            EventBus.Raise<EntityCreatedEvent<EntityType>>(newEntity.ID);
        }

        private void RegisterEntityMethods<EntityType>() where EntityType : Entity
        {
            Type entityType = typeof(EntityType);
            if (entityType.IsClass && !entityType.IsAbstract)
            {
                foreach (ConstructorInfo constructor in entityType.GetConstructors(BindingFlags.NonPublic | BindingFlags.Instance))
                {
                    ParameterInfo[] parameters = constructor.GetParameters();
                    if (parameters.Length == 2 &&
                        parameters[0].ParameterType == typeof(uint) &&
                        parameters[1].ParameterType == typeof(Coordinate))
                    {
                        entityConstructors.Add(entityType, constructor);
                        break;
                    }
                }
            }
        }
    }

}
