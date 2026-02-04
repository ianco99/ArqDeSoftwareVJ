using ianco99.ToolBox.DataFlow;
using ianco99.ToolBox.Services;
using System;
using ZooArchitect.Architecture.Entities;
using ZooArchitect.Architecture.GameLogic;
using ZooArchitect.Architecture.GameLogic.Controllers;
using ZooArchitect.Architecture.Math;

namespace ZooArchitect.Architecture
{
    internal class Scene : IService, IInitable, ITickable, IDisposable
    {
        private EntitiesLogic EntitiesLogic => ServiceProvider.Instance.GetService<EntitiesLogic>();
        private Wallet Wallet => ServiceProvider.Instance.GetService<Wallet>();
        public bool IsPersistance => false;

        private SpawnEntityControllerArchitecture spawnEntityControllerArchitecture;
        private Map map;


        public void Init()
        {
            ServiceProvider.Instance.AddService<Time>(new Time());
            ServiceProvider.Instance.AddService<DayNightCycle>(new DayNightCycle());
            ServiceProvider.Instance.AddService<Wallet>(new Wallet());
            ServiceProvider.Instance.AddService<EntityRegistry>(new EntityRegistry());
            ServiceProvider.Instance.AddService<EntityFactory>(new EntityFactory());
            ServiceProvider.Instance.AddService<EntitiesLogic>(new EntitiesLogic());
        }

        public void LateInit()
        {
            map = new Map(10, 10);

            spawnEntityControllerArchitecture = new SpawnEntityControllerArchitecture();
            
        }

        public void Tick(float deltaTime)
        {
            EntitiesLogic.Tick(deltaTime);
        }

        public void Dispose()
        {
            spawnEntityControllerArchitecture.Dispose();
            EntitiesLogic.Dispose();
            Wallet.Dispose();
        }

        public bool IsCoordinateInsideMap(Coordinate coordinate)
        {
            return map.IsCoordinateInsideMap(coordinate);
        }
    }
}