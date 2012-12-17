using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;

namespace Villainous
{
    class Station
    {

        public static Station Instance
        {
            private set;
            get;
        }

        private int width;
        private int height;
        Tile[,] tiles;


        public bool DronesReporgramed
        {
            get;
            set;
        }

        public bool GunsEnabled
        {
            get;
            set;
        }

        public bool DistressBeaconEnabled
        {
            get;
            set;
        }

        public Station(Texture2D map)
        {
            Station.Instance = this;

            this.width = map.Width;
            this.height = map.Height;
            Color[] textureColors = new Color[map.Width * map.Height];
            map.GetData<Color>(textureColors);

            tiles = new Tile[map.Width, map.Height];

            for (int x = 0; x < map.Width; x++)
            {
                for (int y = 0; y < map.Height; y++)
                {
                    Color texColor = textureColors[y * map.Height + x];
                    tiles[x, y] = TileMap.GetTile(texColor).Copy();

                }
            }

            DronesReporgramed = false;
            GunsEnabled = true;
        }


        public void Draw(SpriteBatch batch, float dt)
        {
            for (int x = 0; x < width; x++)
            {
                for (int y = 0; y < height; y++)
                {
                    tiles[x, y].Draw(batch, dt, Camera.ToScreenSpace(new Vector2(x, y)));
                    
                }
            }

        }


        public Tile GetTile(int x, int y)
        {
            return tiles[x, y];
        }

    }
}
