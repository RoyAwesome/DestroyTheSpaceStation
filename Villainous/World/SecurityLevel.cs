using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace Villainous
{
    struct SecurityLevel
    {
        public static SecurityLevel RED = new SecurityLevel(Color.Red, 5, "RED");
        public static SecurityLevel GREEN = new SecurityLevel(Color.Green, 4, "GREEN");
        public static SecurityLevel BLUE = new SecurityLevel(Color.Blue, 3, "BLUE");
        public static SecurityLevel YELLOW = new SecurityLevel(Color.Yellow, 2, "YELLOW");
        public static SecurityLevel PURPLE = new SecurityLevel(Color.Purple, 1, "PURPLE");
        public static SecurityLevel NONE = new SecurityLevel(Color.White, 0, "NONE");

        public static SecurityLevel GetSecurityLevelByName(string name)
        {
            
            if (name == "RED") return RED;
            if (name == "GREEN") return GREEN;
            if (name == "BLUE") return BLUE;
            if (name == "YELLOW") return YELLOW;
            if (name == "PURPLE") return PURPLE;
            return NONE;
        }

        public Color securityColor;
        int level;
        public string name;

        public SecurityLevel(Color color, int level, string name)
        {
            this.securityColor = color;
            this.level = level;
            this.name = name;
        }

        public static bool operator ==(SecurityLevel t, SecurityLevel o){
            return t.level == o.level;
        }
        public static bool operator !=(SecurityLevel t, SecurityLevel o)
        {
            return t.level != o.level;
        }

        public static bool operator >(SecurityLevel t, SecurityLevel o)
        {
            return t.level > o.level;
        }

        public static bool operator <(SecurityLevel t, SecurityLevel o)
        {
            return t.level < o.level;
        }

        public static bool operator >=(SecurityLevel t, SecurityLevel o)
        {
            return t.level >= o.level;
        }
        public static bool operator <=(SecurityLevel t, SecurityLevel o)
        {
            return t.level <= o.level;
        }

    }
}
