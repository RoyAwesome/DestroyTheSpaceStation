using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;

namespace Villainous
{
    class SecurityRobot : MovingEntity
    {

        bool alerted = false;
        public SecurityRobot(Vector2 position) : base(position)
        {
            this.HeadTexture = "robot_head";
            this.BodyTexture = "human_body";
            this.HeadColor = Color.White;
            this.BodyColor = Color.Green;
        }
        public override bool DoTurn()
        {
            return true;
        }
        public void Alert()
        {
            this.alerted = true;
            this.HeadColor = Color.Orange;
        }

        public override void HandleMetadata(string metadata)
        {
            this.securityLevel = SecurityLevel.GetSecurityLevelByName(metadata);
            this.BodyColor = securityLevel.securityColor;
        }
      
    }
}
