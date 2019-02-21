using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Tanks
{
    class CIconEnemy : CGameObject
    {
        public CIconEnemy(int x, int y)
        {
            // Position:
            this.X = x;
            this.Y = y;
            //Dimentions:
            this.Width = 14;
            this.Height = 14;
            // Image:
            this.Image = Properties.Resources.enemy_icon;
            //
            this.Transparent = true;
        }
    }
}
