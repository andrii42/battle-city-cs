using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;

namespace Tanks
{
    public class CBonusStar : CBonus
    {

        public CBonusStar(float x, float y) : base()
        {
            this.X = x;

            this.Y = y;

            this.lifetime = 1;

            this.Image = Properties.Resources.star;

            this.bonusID = Bonuses.Star;
        }
    }
}
