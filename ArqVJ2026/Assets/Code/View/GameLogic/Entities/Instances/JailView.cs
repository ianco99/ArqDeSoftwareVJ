using System;
using System.Collections.Generic;
using UnityEngine;
using ZooArchitect.Architecture.Entities;
using ZooArchitect.Architecture.Math;
using ZooArchitect.View.Mapping;

namespace ZooArchitect.View.Entities
{
    [ViewOf(typeof(Jail))]
    internal sealed class JailView : StructureView
    {
        private List<GameObject> perimeterInstances;
        public override Type ArchitectureEntityType => typeof(Jail);

        public override void Init(params object[] parameters)
        {
            base.Init(parameters);

            perimeterInstances = new List<GameObject>();
            GameObject perimeterPrefab = (GameObject)parameters[0];

            foreach (Point perimeterPoint in ArchitectureEntity.coordinate.Perimeter)
            {
                //GameObject instance = Instantiate
                //Instantiate(perimeterPrefab, GameScene.PointToWorld(perimeterPoint), Quaternion.identity);
                //perimeterInstances.Add(instance);
            }
        }
    }
}
