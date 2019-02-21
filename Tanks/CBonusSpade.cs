using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Tanks
{
    public class CBonusSpade : CBonus
    {
        // *********************************
        // Private:
        // *********************************
        bool blink;

        // *********************************
        // Public:
        // *********************************
        public List<IGameObject> HQobjects = new List<IGameObject>();

        // *********************************
        // Methods:
        // *********************************
        public override void ProcessObject()
        {
            base.ProcessObject();

            if ( (this.lifetime < 200) && (this.lifetime % 20 == 0) )
            {
                var img = blink ? Properties.Resources.iron : Properties.Resources.brick_full;

                HQobjects.ForEach(obj => obj.Image = img);

                blink = !blink;
            }
        }

        public CBonusSpade(float x, float y) : base()
        {
            this.X = x;

            this.Y = y;

            this.lifetime = 800;

            this.Image = Properties.Resources.spade;

            this.bonusID = Bonuses.Spade;
        }
    }
}
