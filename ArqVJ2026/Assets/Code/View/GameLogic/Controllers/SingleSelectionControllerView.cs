using System;
using System.Collections.Generic;
using UnityEngine;
using ZooArchitect.Architecture.Math;

namespace ZooArchitect.View.Controller
{
    internal abstract class SingleSelectionControllerView : ControllerView
    {
        public override void Tick(float deltaTime)
        {
            if (Input.GetMouseButtonDown(1))
            {
                CreateController();
            }
        }

        private override void CreateController()
        {
            Coordinate clickPoint = new Coordinate(GameScene.GetMouseGridCoordinate());
            List<string> blueprints = GetValidBlueprints(clickPoint);
            if (blueprints.Count == 0)
                return;


            Display(GetActionsToDsiplay(clickPoint, blueprints));
        }

        protected abstract List<string> GetValidBlueprints(Coordinate clickpoint);
        protected abstract Dictionary<string, Action> GetActionsToDsiplay(Coordinate clickPoint, List<string> blueprints);
    }
}
