using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;

namespace Tanks
{
    public delegate object BonusTakeChecker(CBonus bonus);

    public class BonusEventArgs : EventArgs
    {        
        private CBonus bonus;

        public CBonus Bonus
        {
            get { return bonus; }
            set { bonus = value; }
        }               

        public BonusEventArgs(CBonus b)
        {
            bonus = b;
        }
    }

    public class CBonus : CGameObject, IProcessable
    {
        
        const int showtime = 15;

        // *********************************
        // Private and protected members:
        // *********************************

        private int frame = 0;

        private bool taken;
               
        protected int lifetime = 1;

        protected Bonuses bonusID;

        // *********************************
        // Properties:
        // *********************************

        public bool Taken
        {
            get { return taken; }
            set { taken = value; }
        }        


        // *********************************
        // Events and Delegates:
        // *********************************

        public event EventHandler OnBonusTake;

        public event EventHandler OnBonusExpire;

        public BonusTakeChecker CheckBonusTake;

        // *********************************
        // Methods:
        // *********************************

        public virtual void ProcessObject()
        {
            if (!Taken)
            {

                if (frame++ > showtime)
                {
                    this.Visible = !this.Visible;

                    frame = 0;
                }

                var bonusReceiver = (CTank)CheckBonusTake(this);

                if ((bonusReceiver != null) && (OnBonusTake != null))
                {
                    this.Taken = true;

                    OnBonusTake(bonusReceiver, new BonusEventArgs(this));

                    this.Visible = false;
                }
            }

            else
            {
                if (lifetime-- < 0)
                {
                    if (OnBonusExpire != null) this.OnBonusExpire(this, new EventArgs());

                    this.Destroyed = true;

                    return;
                }

            }
        }

        public CBonus()
        {
            this.Transparent = true;
                        
            this.Width = 32;

            this.Height = 32;

            this.DrawPriority = 9;                 

        }

    }
}
