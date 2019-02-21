using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;

namespace Tanks
{
    public class CForceField : CAnimatable
    {
        // *********************************
        // Private and protected members:
        // *********************************
        private CTank owner;

        private int lifeTime;

        // *********************************
        // Properties:
        // *********************************
        public CTank Owner
        {
            get { return owner; }
            set { owner = value; }
        }

        // *********************************
        // Methods:
        // *********************************
       
        public override void ProcessObject()
        {
            base.ProcessObject();

            this.X = owner.X;

            this.Y = owner.Y;

            if (--this.lifeTime < 0)
            {
                owner.Invincible = false;

                this.Destroyed = true;
            }
        }

        public CForceField(CTank owner, int lifetime) : base()
        {
            this.images = new List<Image> { Properties.Resources.forcefield1, Properties.Resources.forcefield2 }; 

            this.owner = owner;

            this.owner.Invincible = true;

            this.lifeTime = lifetime;

            this.Width = 32;

            this.Height = 32;

            this.X = owner.X;

            this.Y = owner.Y;

            this.Transparent = true;

            this.imageChangeInterval = 2;

            this.destroyAfterApear = false;

            // Setting drawing priority:
            this.DrawPriority = 10;
        }
    }
}
