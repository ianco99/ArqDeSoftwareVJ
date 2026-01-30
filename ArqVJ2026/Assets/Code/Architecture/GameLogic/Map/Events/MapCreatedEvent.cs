using ianco99.ToolBox.Events;
using ianco99.ToolBox.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZooArchitect.Architecture.GameLogic.Events
{
    public struct MapCreatedEvent : IEvent
    {

        public void Assign(params object[] parameters)
        {

        }

        public void Reset()
        {

        }
    }

    public struct TileCreatedEvent : IEvent
    {
        public int tileId;
        public int xCoord;
        public int yCoord;
        public void Assign(params object[] parameters)
        {
            tileId = (int)parameters[0];
            xCoord = (int)parameters[1];
            yCoord = (int)parameters[2];
        }

        public void Reset()
        {
            tileId = 0;
            xCoord = -1;
            yCoord = -1;
        }
    }
}
