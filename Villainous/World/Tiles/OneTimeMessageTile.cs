using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Villainous
{
    class OneTimeMessageTile : MessageTile
    {
        bool walkedInto = false;

        public override void OnWalkInto(Entity e)
        {
            if (!walkedInto)
            {
                base.OnWalkInto(e);
            }
            walkedInto = true;
        }

    }
}
