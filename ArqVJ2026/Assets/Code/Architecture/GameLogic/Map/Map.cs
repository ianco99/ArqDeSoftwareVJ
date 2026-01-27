using ianco99.ToolBox.Blueprints;
using ianco99.ToolBox.Services;
using System;
using System.Collections.Generic;
using ZooArchitect.Architecture.Data;
using ZooArchitect.Architecture.Exceptions;

namespace ZooArchitect.Architecture.GameLogic
{
    public sealed class Map
    {
        BlueprintRegistry BlueprintRegistry => ServiceProvider.Instance.GetService<BlueprintRegistry>();
        BlueprintBinder BlueprintBinder => ServiceProvider.Instance.GetService<BlueprintBinder>();

        private uint sizeX;
        private uint sizeY;

        private Tile[,] grid;

        private Dictionary<int, TileData> tileDatas;
        private Dictionary<int, string> tileHashToName;

        public Map(uint sizeX, uint sizeY)
        {
            this.sizeX = sizeX;
            this.sizeY = sizeY;
            tileDatas = new Dictionary<int, TileData>();
            tileHashToName = new Dictionary<int, string>();

            foreach (string tileTypeID in BlueprintRegistry.BlueprintsOf(TableNames.TILE_TYPES_TABLE_NAME))
            {
                object tileData = new TileData();

                try
                {
                    BlueprintBinder.Apply(ref tileData, TableNames.TILE_TYPES_TABLE_NAME, tileTypeID);
                }
                catch (DataMisalignedException exception )
                {
                    throw new DataEntryException($"Falied to read {TableNames.TILE_TYPES_TABLE_NAME} - {tileTypeID} ({exception.Message})");
                }

                tileDatas.Add(tileTypeID.GetHashCode(), (TileData)tileData);
                tileHashToName.Add(tileTypeID.GetHashCode(), tileTypeID);
            }

            grid = new Tile[sizeX, sizeY];
            int defaultDataHash = 0;

            TileData defaultData;
            foreach (TileData tileData in tileDatas.Values)
            {
                if (tileData.isDefault)
                {
                    defaultDataHash = tileData.GetHashCode();
                    break;
                }
            }

            if (defaultDataHash == 0)
            {
                throw new BrokenGameRuleException("Missing default tile definition in Blueprints.xlsx");
            }

            for (int x = 0; x < sizeX; x++)
            {
                for (int y = 0; y < sizeY; y++)
                {
                    grid[x, y] = new Tile(defaultDataHash);
                }
            }
        }
    }

}