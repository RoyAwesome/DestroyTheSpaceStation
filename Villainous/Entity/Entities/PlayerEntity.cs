using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace Villainous
{
    class PlayerEntity : MovingEntity
    {

        bool interacting = false;
        bool usingItem = false;
        int ItemUsing = -1;
        int credits = 0;

        public PlayerEntity(Vector2 spawnPoint)
            : base(spawnPoint)
        {
            this.securityLevel = SecurityLevel.YELLOW;
          
            this.HeadTexture = "human_face";
            this.BodyTexture = "human_body";
            this.HeadColor = Color.White;
            UserInterface.TrackPlayer(this);
            Camera.TrackPlayer(this);
            for (int i = 0; i < 10; i++)
            {
                PickUpItem(null);
            }
        }

        public override bool DoTurn()
        {
            bool movementActionTaken = false;

            KeyboardState state = Keyboard.GetState();

            for (int i = 0; i < 10; i++)
            {
                
                if (usingItem && state.IsKeyDown((Keys)Enum.Parse(typeof(Keys), "D"+(i))))
                {
                    usingItem = false;

                    int itemSlot = (i == 0) ? 1 : i-1;
                    ItemEntity item = GetItem(itemSlot);
                    if (item == null) 
                    {
                        UserInterface.Message("There is no item in that slot", Color.Blue);
                        continue;
                    }
                    else if (!item.NeedsDirection)
                    {

                        UseItem(itemSlot);
                    }
                    else
                    {
                        ItemUsing = itemSlot;
                        UserInterface.Message("Which Direction?", Color.Aqua);
                    }
                    break;
                }
            }

               

            if (state.IsKeyDown(Keys.W) || state.IsKeyDown(Keys.Up))
            {
                if (ItemUsing != -1)
                {
                    UseItem(0, -1, ItemUsing);
                    ItemUsing = -1;
                }
                else if (interacting)
                {
                    InteractDirection(0, -1);
                    interacting = false;
                }
                else
                {
                    MoveDirection(0, -1);
                }
                movementActionTaken = true;
            }
            if (state.IsKeyDown(Keys.S) || state.IsKeyDown(Keys.Down))
            {
                if (ItemUsing != -1)
                {
                    UseItem(0, 1, ItemUsing);
                    ItemUsing = -1;
                }
                else if (interacting)
                {
                    InteractDirection(0, 1);
                    interacting = false;
                }
                else
                {
                    MoveDirection(0, 1);
                }
                movementActionTaken = true;
            }
            if (state.IsKeyDown(Keys.D) || state.IsKeyDown(Keys.Right))
            {
                if (ItemUsing != -1)
                {
                    UseItem(1, 0, ItemUsing);
                    ItemUsing = -1;
                }
                else if (interacting)
                {
                    InteractDirection(1, 0);
                    interacting = false;
                }
                else
                {
                    MoveDirection(1, 0);
                }
                movementActionTaken = true;
            }
            if (state.IsKeyDown(Keys.A) || state.IsKeyDown(Keys.Left))
            {
                if (ItemUsing != -1)
                {
                    UseItem(-1, 0, ItemUsing);
                    ItemUsing = -1;
                }
                else if (interacting)
                {
                    InteractDirection(-1, 0);
                    interacting = false;
                }
                else
                {
                    MoveDirection(-1, 0);
                }
                movementActionTaken = true;
            }
            if (state.IsKeyDown(Keys.E) || state.IsKeyDown(Keys.Enter))
            {
                UserInterface.Message("Interact in which direction?", Color.Aqua);
                interacting = true;
                movementActionTaken = true;

            }

            if (state.IsKeyDown(Keys.Q) || state.IsKeyDown(Keys.RightShift))
            {
                UserInterface.Message("Use which item? (1 - 0)", Color.Aqua);
                movementActionTaken = true;
                usingItem = true;
            }

           

            return movementActionTaken;

        }

        public void ChangeSecurityLevel(SecurityLevel level)
        {
            this.securityLevel = level;
        }

       

        protected override void MoveDirection(int x, int y)
        {
            base.MoveDirection(x, y);
            Camera.UpdateCamera();
        }

        public void AddCredits(int ammount)
        {
            credits += ammount;
        }

        public int GetCredits()
        {
            return credits;
        }

        

            
    }
}
