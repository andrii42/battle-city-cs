using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;

namespace Tanks
{
    public class CWater : CAnimatable//CTile, IProcessable
    {
        
        
        public CWater(float x, float y)
            : base()
        {
            // Setting image:
            //this.Image = Properties.Resources.water_1;
            
            // Setting position:
            this.X = x;
            this.Y = y;

            this.Width = 16;
            this.Height = 16;

            // Setting drawing priority:
            this.DrawPriority = 5;

            this.destroyAfterApear = false;

            this.imageChangeInterval = 30;

            this.images = new List<Image> { Properties.Resources.water_1, Properties.Resources.water_2 };

            // Setting it transparent:
            this.Transparent = false;
        }
    }
}
