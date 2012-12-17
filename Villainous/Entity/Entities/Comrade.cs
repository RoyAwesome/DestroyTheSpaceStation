using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace Villainous
{
    class Comrade : MovingEntity
    {

        public Comrade(Vector2 position)
            : base(position)
        {
            this.HeadTexture = "human_face";
            this.BodyTexture = "human_body";
            this.HeadColor = Color.White;
            this.BodyColor = Color.Red;
            this.securityLevel = SecurityLevel.RED;
        }


        public override bool DoTurn()
        {
            return true;
        }
    }
}
