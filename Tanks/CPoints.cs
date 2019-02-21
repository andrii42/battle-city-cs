using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;

namespace Tanks
{
    public class CPoints : CAnimatable
    {

        public CPoints(float x, float y, int points) : base()
        {
            this.X = x;
            
            this.Y = y;
            
            this.Width = 32;
            
            this.Height = 32;
            
            this.DrawPriority = 9;
            
            this.imageChangeInterval = 25;

            this.destroyAfterApear = true;

            switch (points)
            {
                case 100: images = new List<Image> { Properties.Resources._100 }; break;
                case 200: images = new List<Image> { Properties.Resources._200 }; break;
                case 300: images = new List<Image> { Properties.Resources._300 }; break;
                case 400: images = new List<Image> { Properties.Resources._400 }; break;
                case 500: images = new List<Image> { Properties.Resources._500 }; break;
            }
        }
    }
}
