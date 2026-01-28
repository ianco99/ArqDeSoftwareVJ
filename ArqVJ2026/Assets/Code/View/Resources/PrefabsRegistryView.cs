using ianco99.ToolBox.Blueprints;
using ianco99.ToolBox.Events;
using ianco99.ToolBox.Services;
using ZooArchitect.Architecture.Logs;
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

        public bool IsPersistance => false;

        private Dictionary<string, string> prefabPaths;
        private Dictionary<string, GameObject> prefabs;

        private GameObject missingPrefab;

        public PrefabsRegistryView()
        {
            prefabs = new Dictionary<string, GameObject>();
            prefabPaths = new Dictionary<string, string>();
            missingPrefab = UnityEngine.Resources.Load<GameObject>(Path.Combine("Prefabs", "Missing Prefab"));
            foreach (string id in BlueprintRegistry.BlueprintsOf(TableNamesView.PREFABS_VIEW_TABLE_NAME))
            {
                object prefabPath = new PrefabPath();
                BlueprintBinder.Apply(ref prefabPath, TableNamesView.PREFABS_VIEW_TABLE_NAME, id);
                prefabPaths.Add(((PrefabPath)prefabPath).architectureID, ((PrefabPath)prefabPath).PrefabResourcePath);
            }
        }

        public GameObject Get(string architectureID)
        {
            string resourcePath = prefabPaths[architectureID];

            if (prefabs.ContainsKey(resourcePath))
                return prefabs[resourcePath];

            GameObject prefab = UnityEngine.Resources.Load<GameObject>(resourcePath);

            if (prefab == null)
            {
                prefab = missingPrefab;
                Console.Warning($"Missing prefab in resource folder: {resourcePath}");
            }

            prefabs.Add(resourcePath, prefab);
            return prefab;
        }

        private struct PrefabPath
        {
            [BlueprintParameter("Architecture ID")] public string architectureID;
            [BlueprintParameter("Prefab resource path")] private string prefabResourcePath;

            public string PrefabResourcePath => prefabResourcePath.Replace('/', Path.DirectorySeparatorChar);
        }
    }
}
