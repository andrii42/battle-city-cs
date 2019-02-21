using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Tanks
{
    public class CGarden : CTile
    {
        public CGarden(float x, float y)
            : base()
        {
            // Setting drawing priority:
            this.DrawPriority = 8;

            // Setting image:
            this.Image = Properties.Resources.garden;
            
            // Setting coordinates:
            this.X = x;
            
            this.Y = y;
            
            // Set it transparent:
            this.Transparent = true;
        }
    }
}
