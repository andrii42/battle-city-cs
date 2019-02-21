using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Tanks
{
    public class CTankEnemy : CTank, IProcessable
    {

        // ********************** //
        //  Private & protected :
        // ********************** //

        // Indicates if this enemy tank has bonus:
        bool isBonus;

        // ********************** //
        //  Events:
        // ********************** //

        // If IsBonus set:
        public event EventHandler OnCreateBonus;

        // ********************** //
        //  Properties:
        // ********************** //

        public bool IsBonus
        {
            get { return isBonus; }
            set { isBonus = value; }
        }


        // ********************** //
        //  Methods:
        // ********************** //
        // Turns a tank if posible:
        public void Turn()
        {
            // If tank can turn then turn with probability of 10% :
            if (this.CanTurn && (rnd.Next(100) > 90)) this.RandomDirection();

        }

        public override void Hit(CProjectile p)
        {
            if (IsBonus)
            {
                OnCreateBonus(this, new EventArgs());

                IsBonus = false;
            }

            base.Hit(p);
        }

        // Process element method:
        public void ProcessObject()
        {
            if (this.Enabled)
            {
                // Try to shoot:
                if ((rnd.Next(1, 100) > 95)) Shoot();

                // If tank is blocked by another tank ahead, and it cant turn, then try to revers movement vector with probability of 5% :
                if ((!canMoveForward) && (!CanTurn) && (rnd.Next(100) > 95)) ReverceDirection();

                // Try to turn:
                Turn();
            }
            // Try to move:
            Move();

            // Changing image:
            ChangeImage();
        }

        // Constructor:
        public CTankEnemy(bool bonus) : base()
        {
            this.IsBonus = bonus;
            // Alowing to move:
            this.AllowedToMove = true;

            this.ProjectileLimit = 1;            
        }
    }
}
