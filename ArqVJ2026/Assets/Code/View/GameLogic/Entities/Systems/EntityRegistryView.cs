using ianco99.ToolBox.Services;
using System;
using System.Collections.Generic;
using ZooArchitect.Architecture.Entities;

namespace ZooArchitect.View.Entities
{
    class EntityRegistryView : IService
    {
        public bool IsPersistance => false;

        private Dictionary<uint, EntityView> entities;
        private Dictionary<Type, List<uint>> entityIdsPerType;

        public  EntityRegistryView()
        {
            entities = new Dictionary<uint, EntityView>();
            entityIdsPerType = new Dictionary<Type, List<uint>>();
        }

        internal string RegisterMethodName => nameof(Register);

        private void Register(EntityView entityView)
        {
            entities.Add(entityView.ArchitectureEntityID, entityView);
            Type currentEntityType = entityView.GetType();

            do
            {
                if (!entityIdsPerType.ContainsKey(currentEntityType))
                {
                    entityIdsPerType.Add(currentEntityType, new List<uint>());
                }
                entityIdsPerType[currentEntityType].Add(entityView.ArchitectureEntityID);
                currentEntityType = currentEntityType.BaseType;
            } while (currentEntityType != typeof(Entity));
        }

        public EntityType GetAs<EntityType>(uint ID) where EntityType : EntityView
        {
            if (ID == Entity.UNASSIGNED_ENTITY_ID)
            {
                throw new NullReferenceException("Entity id 0 represents a null entity");
            }

            if (entities.ContainsKey(ID))
            {
                throw new KeyNotFoundException(ID.ToString());
            }
            if (entities[ID] is not EntityType)
            {
                throw new InvalidCastException($"An attempt was made to obtain a type {entities[ID].GetType().Name}" + $"entity as type {typeof(EntityType).Name} from the EntityRegistry"); ;
            }

            return entities[ID] as EntityType;
        }

        public IEnumerable<EntityView> Entities => FilterEntities<EntityView>();
        public IEnumerable<Animal> Animals => FilterEntities<AnimalView>();
        public IEnumerable<Worker> Workers => FilterEntities<WorkerView>();

        private IEnumerable<EntityType> FilterEntities<EntityType>() where EntityType : EntityView
        {
            if (entityIdsPerType.ContainsKey(typeof(EntityType)))
            {
                foreach (uint ID in entityIdsPerType[typeof(EntityType)])
                {
                    yield return entities[ID] as EntityType;
                }
            }
        }
    }
}
