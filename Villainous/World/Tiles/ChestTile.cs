using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;

namespace Villainous
{
    class ChestTile : Tile
    {
        static Random rng = new Random();
        bool opened = false;

        SecurityLevel level;

        const string OpenedDecal = "chesttile_decal";
        const string SecurityDecal = "chesttile_securitydecal";

        ItemEntity itemInside;

        public ChestTile()
        {
            this.texture = "chesttile_bg";
            this.level = SecurityLevel.PURPLE;
            this.Collidable = true;
        }


        public override void Draw(SpriteBatch batch, float dt, Vector2 position)
        {
            base.Draw(batch, dt, position);
            batch.Draw(ArtManager.GetTexture(OpenedDecal), position, opened?Color.OrangeRed:Color.GreenYellow);
            batch.Draw(ArtManager.GetTexture(SecurityDecal), position, level.securityColor);


        }

        public override void OnCollideWith(Entity e)
        {
            if (e is PlayerEntity) UserInterface.Message("The chest is " + (opened?"opened":"closed. (Press E to interact with it)"), Color.Red);
        }

        public override void OnInteract(Entity e)
        {
            
            if (e is PlayerEntity)
            {
                if (!opened)
                {
                    PlayerEntity p = e as PlayerEntity;
                    if (this.level <= p.GetSecurityLevel())
                    {
                        opened = true;
                        UserInterface.Message("Inside is a " + itemInside.Name + " (Interact again to take)", Color.SkyBlue);
                    }
                    else
                    {
                        UserInterface.Message("You need to be atleast " + level.name + " to use this chest", Color.Red);
                    }
                }
                else
                {
                    if (itemInside != null)
                    {
                        PlayerEntity p = e as PlayerEntity;
                        bool success = p.PickUpItem(itemInside);
                        if (success)
                        {
                            UserInterface.Message("Took " + itemInside.Name, Color.SkyBlue);
                            itemInside = null;
                        }
                    }
                    else UserInterface.Message("The chest is Empty!", Color.Red);
                }
            }
        }

        public override void OnHack(Entity Hacker)
        {
            if (!opened)
            {
                opened = true;
                UserInterface.Message("Inside is a " + itemInside.Name + " (Interact again to take)", Color.SkyBlue);
            }
            else
            {
                UserInterface.Message("Nothing Happens", Color.Red);
            }
        }

        public override void HandleMetadata(int x, int y, string metadata)
        {
            string[] data = metadata.Split(' ');
            level = SecurityLevel.GetSecurityLevelByName(data[0]);
            string[] types = data[1].Substring(1, data[1].IndexOf('}')-1).Split(',');
            //Select a type in this chest
            int itemInChest = rng.Next(types.Length);
            
            string type = types[itemInChest];
            Console.WriteLine("Selected Item " + itemInChest + " it's " + type + " " + types.Length);


            itemInside = ItemEntity.CreateItem(type);
        }
    }
}
