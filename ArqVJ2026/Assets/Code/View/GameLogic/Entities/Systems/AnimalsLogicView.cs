using ianco99.ToolBox.DataFlow;
using ianco99.ToolBox.Services;
using System;
using ZooArchitect.Architecture.Entities;
using ZooArchitect.View.Mapping;

namespace ZooArchitect.View.Entities
{
    [ViewOf(typeof(AnimalsLogic))]
    internal sealed class AnimalsLogicView : IInitable, ITickable, IDisposable
    {
        private EntityRegistryView EntityRegistryView => ServiceProvider.Instance.GetService<EntityRegistryView>();

        public void Init()
        {
            throw new NotImplementedException();
        }

        public void LateInit()
        {
            throw new NotImplementedException();
        }

        public void Tick(float deltaTime)
        {
            throw new NotImplementedException();
        }

        public void Dispose()
        {

        }

        internal void OnFeedAnimalFail(uint animalID)
        {
            EntityRegistryView.GetAs<AnimalView>(animalID).OnFeedFail();
        }

        internal void OnFeedAnimalSuccess(uint animalID)
        {
            EntityRegistryView.GetAs<AnimalView>(animalID).OnFeedSuccess();
        }
    }
}