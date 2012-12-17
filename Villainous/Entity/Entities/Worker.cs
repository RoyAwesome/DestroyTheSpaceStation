using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace Villainous
{
    class Worker : MovingEntity
    {

        public Worker(Vector2 position) : base(position)
        {
            this.HeadTexture = "human_face";
            this.BodyTexture = "human_body";
            this.HeadColor = Color.White;
            this.BodyColor = Color.Green;
        }

        public override bool DoTurn()
        {
            return true;
        }

        public override void HandleMetadata(string metadata)
        {
            this.securityLevel = SecurityLevel.GetSecurityLevelByName(metadata);
            this.BodyColor = securityLevel.securityColor;
        }

        public override void OnInteract(Entity e)
        {
            if (e is PlayerEntity)
            {
                PlayerEntity p = e as PlayerEntity;
                if (p.GetSecurityLevel() > this.GetSecurityLevel())
                {
                    UserInterface.Message("[Worker] Sir?", Color.Green);
                }
                else
                {
                    UserInterface.Message("[Worker] Huh?", Color.Green);
                }
            }
        }
    }
}
