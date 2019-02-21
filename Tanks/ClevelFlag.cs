using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Tanks
{
    public class ClevelFlag : CGameObject
    {
        public ClevelFlag(int x, int y)
        {
            this.X = x;
            this.Y = y;
            this.Width = 32;
            this.Height = 30;
            this.Image = Properties.Resources.LevelFlagBig;
            this.Transparent = true;
        }
    }
}
