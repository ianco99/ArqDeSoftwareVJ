using ianco99.ToolBox.Blueprints;

namespace ZooArchitect.Architecture.GameLogic
{
    public struct TileData
    {
        [BlueprintParameter("Is Walkable")] public bool isWalkable;
        [BlueprintParameter("Is Structure")] public bool isStructure;
        [BlueprintParameter("Is Animal Habitat")] public bool isAnimalContainer;
        [BlueprintParameter("Can Spawn Humans")] public bool canSpawnHumans;
        [BlueprintParameter("Can Dispawn Humans")] public bool canDispawnHumans;
        [BlueprintParameter("Is Default")] public bool isDefault;
    }

}