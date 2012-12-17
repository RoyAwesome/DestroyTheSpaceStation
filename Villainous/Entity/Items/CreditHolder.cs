using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Villainous
{
    class CreditHolder : ItemEntity
    {
        int credits = 0;
        public override void UseItem(int x, int y, Entity user)
        {
            if (user is PlayerEntity)
            {
                PlayerEntity p = user as PlayerEntity;
                p.AddCredits(credits);
                UserInterface.Message(credits + "$ has been deposited into your account");
            }
        }

        public override void HandleMetadata(string metadata)
        {
            credits = int.Parse(metadata);
            Name = "Credit Holder: " + credits + " $";
        }
    }
}
