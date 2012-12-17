using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Villainous
{
    class SecurityClearanceCard : ItemEntity
    {
        SecurityLevel level;

        public SecurityClearanceCard()
        {
            SetLevel(SecurityLevel.PURPLE);
        }

        public void SetLevel(SecurityLevel level)
        {
            this.level = level;

            this.Name = level.name + " SCard";
        }

        public override void UseItem(int x, int y, Entity user)
        {
            if (user is PlayerEntity)
            {
                PlayerEntity p = user as PlayerEntity;
                p.ChangeSecurityLevel(level);                
            }
        }

        public override void HandleMetadata(string metadata)
        {
            SetLevel(SecurityLevel.GetSecurityLevelByName(metadata));
        }
    }
}
