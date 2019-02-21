using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Tanks
{
    class CPlayerIcon : CGameObject
    {
        public CPlayerIcon(int x, int y)
        {
            // Position:
            this.X = x;
            this.Y = y;
            //Dimentions:
            this.Width = 14;
            this.Height = 16;
            // Image:
            this.Image = Properties.Resources.PlayerIcon3;
            //
            this.Transparent = true;
        }
    }
}
