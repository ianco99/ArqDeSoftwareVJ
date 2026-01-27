using ianco99.ToolBox.Blueprints;
using ianco99.ToolBox.Services;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using ZooArchitect.View.Data;

namespace ZooArchitect.View.Resources
{
    internal sealed class PrefabsRegistryView : IService
    {
        private BlueprintRegistry BlueprintRegistry => ServiceProvider.Instance.GetService<BlueprintRegistry>();
        private BlueprintBinder BlueprintBinder => ServiceProvider.Instance.GetService<BlueprintBinder>();
        public bool IsPersistance => true;

        private Dictionary<string, string> prefabPaths;
        private Dictionary<string, GameObject> prefabs;

        public PrefabsRegistryView()
        {
            prefabs = new Dictionary<string, GameObject>();
            prefabPaths = new Dictionary<string, string>();

            foreach (string id in BlueprintRegistry.BlueprintsOf(TableNamesView.PREFABS_VIEW_TABLE_NAME))
            {
                object prefabPath = new PrefabsPath();
                BlueprintBinder.Apply(ref prefabPath, TableNamesView.PREFABS_VIEW_TABLE_NAME, id);
                prefabPaths.Add(((PrefabsPath)prefabPath).architectureID, ((PrefabsPath)prefabPath).PrefabResourcePath);
            }
        }

        public GameObject Get(string resourcePath)
        {
            if (prefabs.ContainsKey(resourcePath))
            {
                return prefabs[resourcePath];
            }

            GameObject prefab = UnityEngine.Resources.Load<GameObject>(resourcePath);
            prefabs.Add(resourcePath, prefab);
            return prefab;
        }

        private struct PrefabsPath
        {
            [BlueprintParameter("Architecture ID")] public string architectureID;
            [BlueprintParameter("Prefab resource path")] private string prefabResourcePath;

            public string PrefabResourcePath => prefabResourcePath.Replace('/', Path.DirectorySeparatorChar);
        }
    }
}
