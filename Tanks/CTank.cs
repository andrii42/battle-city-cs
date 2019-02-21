using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Tanks
{   

    public class CTank : CMovable, ITank
    {
        // ********************** //
        //  Private & protected :
        // ********************** //
        // Health of the tank (number of projectiles to hit):
        private int armour;
        
        // Maximum projectiles at the moment:
        private int projectileLimit;

        private bool invincible;

        private bool enabled = true;
               
        private Tanks tankId = Tanks.Unknown;

        private object destroyer;

        private int projectilesInFlight;

        // Type of the projectile:
        private ProjectileTypes projectileType = ProjectileTypes.Slow;
        
        // 
        protected bool canMoveForward;

        // ********************** //
        //  Events:
        // ********************** //

        // OnShoot event:
        public event OnTankShoot onShoot;
        
        // On check move forward event:
        public event OnCheckMoveForward onCheckMoveForward;

        // On set this object invinsible:
        public event OnSetInvinsible onInvinsible;                                           

        // ********************** //
        //  Properties:
        // ********************** //

        public object Destroyer
        {
            get { return destroyer; }
            set { destroyer = value; }
        }

        public bool Enabled
        {
            get { return enabled; }
            set { 
                   enabled = value;

                   this.AllowedToMove = value;
                }
        }

        // ID of the tank:
        public Tanks TankID
        {
            get { return tankId;  }
            set { tankId = value; }
        }        

        public ProjectileTypes ProjectileType
        {
            get { return projectileType; }
            set { projectileType = value; }
        }  

        public bool Invincible
        {
            get { return invincible; }
            set { invincible = value; }
        }                            

        public int Armour
        {
            get { return armour; }
            set { armour = value;}
        }

        public int ProjectileLimit
        {
            get { return projectileLimit; }
            set { projectileLimit = value; }
        }        

        public int ProjectilesInFlight
        {
            get { return projectilesInFlight; }
            set { projectilesInFlight = value; }
        }

        // ********************** //
        //  Methods:
        // ********************** //

        // Shoot method:
        public void Shoot()
        {
            if (this.onShoot != null) this.onShoot(this);

        }

        // Move method:
        public override void Move()
        {

            // Move forward if posible:
            canMoveForward = onCheckMoveForward(this);

            if (canMoveForward) base.Move();

        }
                
        // Hit method:
        public override void Hit(CProjectile p)
        {
            if (!this.Invincible)
            {
                if (--this.Armour <= 0)
                {
                    this.Destroyer = p.Owner;

                    this.Destroyed = true;
                }
            }

        }

        public virtual void Upgrade()
        {

        }

        // Set tank invincible:
        public void SetInvinsible(int time)
        {
            onInvinsible(this, time);
        }

        // Reset events of the tank:
        public override void ResetEvents()
        {
            this.onCheckMoveForward = null;

            this.onShoot = null;
            
            this.onInvinsible = null;
            
            base.ResetEvents();
        }              

        // Sets images for a tank:
        public virtual void SetImages()
        {

        }

        // Constructor for CTank:
        public CTank()
        {
            // Setting drawing priority:
            this.DrawPriority = 7;

            // Set size:
            this.Width = 32;

            this.Height = 32;                       
        }


    }
}
