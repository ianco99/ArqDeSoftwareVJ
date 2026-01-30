using ianco99.ToolBox.DataFlow;
using ianco99.ToolBox.Services;
using System;
using ZooArchitect.Architecture.Entities;
using ZooArchitect.Architecture.GameLogic;
using ZooArchitect.Architecture.GameLogic.Controllers;

namespace ZooArchitect.Architecture
{
    internal class Scene : IInitable, ITickable, IDisposable
    {
        private SpawnEntityControllerArchitecture spawnEntityControllerArchitecture;
        private Map map;



        public void Init()
        {
            ServiceProvider.Instance.AddService<Time>(new Time());
            ServiceProvider.Instance.AddService<DayNightCycle>(new DayNightCycle());
            ServiceProvider.Instance.AddService<Wallet>(new Wallet());
            ServiceProvider.Instance.AddService<EntityRegistry>(new EntityRegistry());
            ServiceProvider.Instance.AddService<EntityFactory>(new EntityFactory());
        }

        public void LateInit()
        {
            new Map(10, 10);

            spawnEntityControllerArchitecture = new SpawnEntityControllerArchitecture();
        }

        public void Tick(float deltaTime)
        {

        }

        public void Dispose()
        {
            spawnEntityControllerArchitecture.Dispose();
        }
    }
}