using ianco99.ToolBox.Blueprints;
using System.Collections.Generic;

namespace ZooArchitect.Architecture.GameLogic
{
    public sealed class Map
    {
        private uint sizeX;
        private uint sizeY;

        private Tile[,] tiles;

        private Dictionary<int, TileData> tileDatas;

        public Map()
        {
            tileDatas = new Dictionary<int, TileData>();

        }
    }

    

    public struct Tile
    {
         public int tileType;
    }

    public struct TileData
    {
        [BlueprintParameter("TileType")] public bool walkable;
        [BlueprintParameter("TileType")] public bool isStructure;
        [BlueprintParameter("TileType")] public bool isAnimalContainer;
        [BlueprintParameter("TileType")] public bool canSpawnHumans;
        [BlueprintParameter("TileType")] public bool canDispawnHumans;
    }

}