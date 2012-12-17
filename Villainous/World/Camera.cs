using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace Villainous
{
    static class Camera
    {
        static Vector2 TileSize = new Vector2(Tile.TileSize, Tile.TileSize);
        static Vector2 CameraOffset = new Vector2(8, 8);
        static Vector2 Center = new Vector2(0, 0);
        static PlayerEntity track;

        static Rectangle centerBounds = new Rectangle(0, 0, 8, 8);

        public static void TrackPlayer(PlayerEntity player)
        {
            track = player;
        }



        public static void UpdateCamera()
        {
            
            Vector2 offsetPosition = track.GetPosition() - Center;
            
            if (!centerBounds.Contains((int)offsetPosition.X, (int)offsetPosition.Y))
            {
                if (offsetPosition.X <= centerBounds.Right)
                {
                    
                    Center += new Vector2(-1, 0);
                }
                if (offsetPosition.X >= centerBounds.Left)
                {
                    Center += new Vector2(1, 0);
                }
                if (offsetPosition.Y <= centerBounds.Top)
                {
                    Center += new Vector2(0, -1);
                }
                if (offsetPosition.Y >= centerBounds.Bottom)
                {
                    Center += new Vector2(0, 1);
                }
                
            }
        }

        public static Vector2 ToScreenSpace(Vector2 tileCoord)
        {
            return (tileCoord - Center + CameraOffset) * TileSize;
        }

    }
}
