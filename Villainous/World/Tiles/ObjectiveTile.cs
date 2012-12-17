using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace Villainous
{

    enum Objectives
    {
        DRONES,
        NONE,
    }

    class ObjectiveTile : Tile
    {

        Objectives TileObjective = Objectives.NONE;

        public ObjectiveTile()
        {
            this.texture = "floortile";
        }


        public override void  OnControlToggle(Entity toggleer)
        {
 	        switch(TileObjective)
            {
                case Objectives.DRONES:
                    Station.Instance.DronesReporgramed = true;
                    UserInterface.Message("The drones will now shoot the station", Color.Yellow);
                    break;

                default:
                    break;
            }
        }

        public override void HandleMetadata(int x, int y, string metadata)
        {
            TileObjective = (Objectives)Enum.Parse(typeof(Objectives), metadata);
        }
        
    }
}
