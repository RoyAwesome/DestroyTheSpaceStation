using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;
using System.IO;
namespace Villainous
{
    class ArtManager
    {
        static Dictionary<string, Texture2D> textures = new Dictionary<string, Texture2D>();

        public static SpriteFont UIFont;


        public static void LoadAllTexture(ContentManager manager)
        {
            String[] files = Directory.GetFiles("content");
            foreach (String f in files)
            {
                string c = stripPath(f);
                if (!c.EndsWith(".png")) continue;
                textures[stripExt(c)] = manager.Load<Texture2D>(c);

                Console.WriteLine("Loaded " + c + " as " + stripExt(c));
            }

            UIFont = manager.Load<SpriteFont>("uifont");
        }

        public static Texture2D GetTexture(string name)
        {
            if (!textures.ContainsKey(name)) return textures["error"];
            return textures[name];
        }


        private static string stripPath(string path)
        {
            return path.Substring(path.IndexOf('\\')+1);
        }

        private static string stripExt(string file)
        {
            return file.Substring(0, file.IndexOf('.'));
        }
    }
}
