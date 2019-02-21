using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Tanks
{
    public class CEagle : CGameObject
    {

        public CEagle()
        {
            this.Image = Properties.Resources.eagle;
            
            this.X = 208;
            
            this.Y = 400;
            
            this.Height = 32;
            
            this.Width = 32;
            
            // Setting drawing priority:
            this.DrawPriority = 1;
        }

        public override void Hit(CProjectile p)
        {
            this.Destroyed = true;
        }
    }
}
