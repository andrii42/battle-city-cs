using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Tanks
{
    public class CBonusClock : CBonus
    {

        public CBonusClock(float x, float y) : base()
        {
            this.X = x;

            this.Y = y;

            this.lifetime = 800;

            this.Image = Properties.Resources.clock;

            this.bonusID = Bonuses.Clock;
        }
    }
}
