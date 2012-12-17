using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace Villainous
{
    static class TileMap
    {
        static Dictionary<Color, Tile> tilemap = new Dictionary<Color, Tile> ();
        static Dictionary<Vector2, string> metadataMap = new Dictionary<Vector2,string>();

        static TileMap()
        {
            tilemap[Color.White] = new FloorTile();
            tilemap[Color.Black] = new WallTile();
            tilemap[Color.Gray] = new SpaceTile();
            tilemap[Color.Blue] = new DoorTile();
            tilemap[Color.Red] = new SpawnPoint();
            tilemap[new Color(0, 255, 0, 255)] = new OneTimeMessageTile();
            tilemap[new Color(0, 255, 255, 255)] = new MessageTile();
            tilemap[new Color(255, 255, 0, 255)] = new ChestTile();
            tilemap[new Color(178, 0, 255, 255)] = new DecalTile();
            tilemap[new Color(127, 0, 0, 255)] = new TerminalTile();
            tilemap[new Color(0, 127, 0, 255)] = new SecurityTerminal();
            tilemap[new Color(0, 0, 127, 255)] = new ItemScanner();
            tilemap[new Color(127, 127, 0, 255)] = new ObjectiveTile();
        }

        public static Tile GetTile(Color col)
        {
            if (!tilemap.ContainsKey(col)) return tilemap[Color.Gray];
            return tilemap[col];
        }

        public static void ParseMetadata(String[] metadataLines)
        {
            foreach (string s in metadataLines)
            {
                
                string[] md = s.Split(' ');

                for (int i = 0; i < md.Length; i++)
                {
                    md[i] = md[i].Trim();
                }
                int x = int.Parse(md[0]);
                int y = int.Parse(md[1]);
                string data = string.Join(" ", md, 2, md.Length - 2);
                Station.Instance.GetTile(x, y).HandleMetadata(x, y, data);

            }
        }

    }

    
}
