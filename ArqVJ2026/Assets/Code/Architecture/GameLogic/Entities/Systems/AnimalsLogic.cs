using ianco99.ToolBox.DataFlow;
using ianco99.ToolBox.Services;
using System;

namespace ZooArchitect.Architecture.Entities
{
    public sealed class AnimalsLogic : ITickable, IDisposable
    {
        private EntityRegistry EntityRegistry => ServiceProvider.Instance.GetService<EntityRegistry>();

        public void Tick(float deltaTime)
        {
        }

        internal void FeedAnimals()
        {
            foreach (Animal animal in EntityRegistry.Animals)
            {
                animal.Feed();
            }
        }

        public void Dispose()
        {

        }
    }
}