using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;

namespace Tanks
{
    public class CExplotion : CAnimatable
    {
        // *********************************
        // Private and protected members:
        // *********************************
        private int explotionLevel;

        // *********************************
        // Properties and public members:
        // *********************************
        public int ExplotionLevel
        {
            get { return explotionLevel; }
            set { explotionLevel = value; }
        }

        // Object that coused explotion:
        public CGameObject ExplodedObject;

        // *********************************
        // Methods:
        // *********************************
        public override void ProcessObject()
        {
            base.ProcessObject();

            if (ImageIndex > this.ExplotionLevel) this.Destroyed = true;
        }

        public CExplotion(CGameObject exploded, int explotionLevel) : base()
        {
            this.ExplodedObject = exploded;

            this.ExplotionLevel = explotionLevel;

            this.Width = 64;

            this.Height = 64;

            this.X = exploded.X - (int)(this.Width / 2) + (int)(exploded.Width / 2);

            this.Y = exploded.Y - (int)(this.Height / 2) + (int)(exploded.Height / 2);

            this.images = new List<Image> { Properties.Resources.explotion_1, Properties.Resources.explotion_2, Properties.Resources.explotion_3, Properties.Resources.explotion_4, Properties.Resources.explotion_5, Properties.Resources.explotion_3, Properties.Resources.explotion_2 };

            this.DrawPriority = 10;

             // image changing interval:
            imageChangeInterval = explotionLevel > 2 ? 6 : 3;

         }
    }
}
