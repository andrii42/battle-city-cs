using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Tanks
{
    public class CIron : CTile
    {       
        // Constructor:
        public CIron(float x, float y)
            : base()
        {
            // Setting image:
            this.Image = Properties.Resources.iron;
            // Setting position:
            this.X = x;
            this.Y = y;
            // Setting drawing priority:
            this.DrawPriority = 2;
        }


        public override void Hit(CProjectile p)
        {
            if (p.ProjectileType == ProjectileTypes.Max) this.Destroyed = true;
        }
    }
}
