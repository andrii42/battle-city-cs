using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Tanks
{
    public class CIce : CTile
    {

        public CIce(float x, float y)
            : base()
        {
            // Setting image:
            this.Image = Properties.Resources.ice;
            // Setting position:
            this.X = x;
            this.Y = y;
            // Setting drawing priority:
            this.DrawPriority = 4;

            this.Transparent = true;
        }
    }
}
