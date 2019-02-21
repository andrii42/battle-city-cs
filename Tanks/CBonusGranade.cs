using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Tanks
{
    public class CBonusGranade : CBonus
    {

        public CBonusGranade(float x, float y) : base()
        {
            this.X = x;

            this.Y = y;

            this.lifetime = 1;

            this.Image = Properties.Resources.granade;

            this.bonusID = Bonuses.Granade;
        }
    }
}
