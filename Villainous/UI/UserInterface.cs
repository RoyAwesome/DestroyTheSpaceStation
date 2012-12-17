using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;

namespace Villainous
{
    class UIMessage
    {
        public string Message;
        public float ttl;
        public Color color = Color.White;

    }

    class UserInterface
    {
        const int UIWidth = 200;

        public static int height;
        public static int width;
        const float MessageLife = 5.0f;

        static PlayerEntity player;

        static List<UIMessage> messages = new List<UIMessage>();

        public static void TrackPlayer(PlayerEntity entity)
        {
            player = entity;
        }

        public static void Message(string message)
        {
            Message(message, Color.White);
        }

        public static void Message(string message, Color color)
        {
            UIMessage m = new UIMessage();
            m.Message = message;
            m.ttl = MessageLife;
            m.color = color;
            messages.Add(m);
        }
        static float firstMessageTime = 1.0f;
        static bool firstMessageDisplayed = false;
        
        public static void Update(float dt)
        {
            if (firstMessageTime <= 0 && !firstMessageDisplayed )
            {
                UserInterface.Message("Welcome! WASD to move. TODO: Arrow Keys");
                firstMessageDisplayed = true;
            }
            else firstMessageTime -= dt;

            messages.ForEach((m) => { m.ttl -= dt; });            
            messages.RemoveAll((match) => { return match.ttl <= 0; });
        }


        public static void Draw(SpriteBatch batch, float dt)
        {

            //Draw UI
            float startUI = width - UIWidth;
            batch.Draw(ArtManager.GetTexture("transparentblack"), new Rectangle((int)startUI, 0, UIWidth, height), Color.White);
            batch.DrawString(ArtManager.UIFont, "Space Station Status", new Vector2(startUI + 35, 20), Color.White);
            batch.DrawString(ArtManager.UIFont, "All Systems Normal", new Vector2(startUI + 40, 35), Color.Green);

            batch.DrawString(ArtManager.UIFont, "Current Clearence Level", new Vector2(startUI + 20, 60), Color.White);
            batch.DrawString(ArtManager.UIFont, player.GetSecurityLevel().name, new Vector2(startUI + 80, 75), player.GetSecurityLevel().securityColor);

            batch.DrawString(ArtManager.UIFont, "Current Inventory", new Vector2(startUI + 40, 120), Color.White);
            int invItem = 0;
            for (int i = 0; i < 10; i++)
            {
                ItemEntity item = player.GetInventory()[i];
                string itemName = "No Item";
                bool restricted = false;
                if (item != null)
                {
                    itemName = item.Name;
                    restricted = item.RestrictedItem;
                }
                invItem++;
                if (invItem == 10) invItem = 0;
                batch.DrawString(ArtManager.UIFont, invItem + ") " + itemName, new Vector2(startUI + 20, 135 + (i * 15)), restricted ? Color.Red : Color.White);
                
            }

            batch.DrawString(ArtManager.UIFont, "Current Account Balance", new Vector2(startUI + 20, 300), Color.White);
            batch.DrawString(ArtManager.UIFont, player.GetCredits() + "$", new Vector2(startUI + 95, 315), Color.White);

            //Draw Messages
            Vector2 position = new Vector2(0, height - 20);
            Vector2 offset = new Vector2(0, -ArtManager.UIFont.MeasureString("l").Y - 5);
            int num = messages.Count ;
            foreach (UIMessage msg in messages)
            {
                num--;
                Vector2 stringSize = ArtManager.UIFont.MeasureString(msg.Message);
                Vector2 place = position + num * offset;
                float trans = MathHelper.Clamp(msg.ttl, 0.0f, 1.0f);
                Color transcolor = new Color(msg.color.R, msg.color.G, msg.color.B, trans);
                batch.Draw(ArtManager.GetTexture("transparentblack"), new Rectangle((int)place.X, (int)place.Y, (int)stringSize.X + 5, (int)stringSize.Y + 5), transcolor);
                batch.DrawString(ArtManager.UIFont, msg.Message, place, transcolor);
                
            }
        }

    }
}
