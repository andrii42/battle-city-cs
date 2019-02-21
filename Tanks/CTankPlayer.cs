using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using System.Drawing.Imaging;

namespace Tanks
{    

    public class CTankPlayer : CTank , ITankPlayer, IProcessable
    {
        // ********************** //
        //  Private & protected :
        // ********************** //

        private int life = 99;

        private int tankLevel = 0;

        int points;

        int[] enemiesHit = { 0, 0, 0, 0 };

        int[] pointsForTanks = { 0, 0, 0, 0 };
 
        // ********************** //
        //  Properties :
        // ********************** //

        public int Life
        {
            get { return life; }
            set { life = value; }
        }                 

        public int Points
        {
            get { return points; }
            set  { points = value;}
        }
        
        public override Directions Direction
        {
            get { return direction; }

            set
            {
                if (value != this.direction)
                {
                    switch (value)
                    {
                        
                        case Directions.Up:
                            {
                                //VectorY = -1;
                                
                                if (this.X % 16 != 0) this.X = (this.X % 16 > 8) ? this.X + 16 - (this.X % 16) : this.X - (this.X % 16);

                            } break;
                        
                        case Directions.Down:
                            {
                                //VectorY = 1;
                                
                                if (this.X % 16 != 0) this.X = (this.X % 16 > 8) ? this.X + 16 - (this.X % 16) : this.X - (this.X % 16);

                            }
                            break;
                        
                        case Directions.Left:
                            {
                                //VectorX = -1;
                                
                                if (this.Y % 16 != 0) this.Y = (this.Y % 16 > 8) ? this.Y + 16 - (this.Y % 16) : this.Y - (this.Y % 16);

                            }
                            break;
                        
                        case Directions.Right:
                            {
                                //VectorX = 1;

                                if (this.Y % 16 != 0) this.Y = (this.Y % 16 > 8) ? this.Y + 16 - (this.Y % 16) : this.Y - (this.Y % 16);
                                
                            }
                            break;
                    }
                    
                    //base.Direction = value;

                    if (onDirectionChange != null) onDirectionChange(this);

                }

                base.Direction = value;
            }

        }

        public virtual int TankLevel
        {
            get
            {
                return tankLevel;
            }
            set
            {
                if (value < 4) tankLevel = value;

                switch (tankLevel)
                {
                    case 0:
                        {
                            this.ProjectileLimit = 1;

                            this.ProjectileType = ProjectileTypes.Slow;

                        } break;
                    case 1:
                        {
                            this.ProjectileLimit = 1;

                            this.ProjectileType = ProjectileTypes.Fast;
                        } break;
                    case 2:
                        {
                            this.ProjectileLimit = 2;

                            this.ProjectileType = ProjectileTypes.Fast;
                        } break;
                    case 3:
                        {
                            this.ProjectileLimit = 2;

                            this.ProjectileType = ProjectileTypes.Max;
                        } break;
                }

            }
        }

        // ********************** //
        //  Events :
        // ********************** //
        // On direction change event:
        public event OnDirectionChange onDirectionChange;

        // ********************** //
        //  Methods :
        // ********************** //

        // IProcessable interface:
        public void ProcessObject()
        {            
            this.Move();

            if (AllowedToMove) ChangeImage();//this.ImageIndex++;
        }        

        public int[] GetEnemiesHit()
        {
            return enemiesHit;
        }

        public int[] GetPointsForTanks()
        {
            return pointsForTanks;
        }

        public void ResetEnemyHits()
        {
            this.enemiesHit = new int[4];
        }

        public void AddHit(Tanks tank)
        {
            var tankIndex = (int)tank;

            var points = ((int)tank + 1) * 100;

            this.enemiesHit[tankIndex]++;

            this.pointsForTanks[tankIndex] += points;

            this.Points += points;
        }

        public virtual void Reset(bool full)
        {
            // reseting events:
            this.ResetEvents();                                            

            // Reseting health:
            this.Armour = 1;

            // Resurecting:
            this.Destroyed = false;

            // stop it:
            this.AllowedToMove = false;

            this.ProjectilesInFlight = 0;

            //
            if (full) 
            {
                this.TankLevel = 0;
                // removing events:
                //this.ResetEvents();
                
                // projectile limit reset:
                //this.ProjectileLimit = 1;
            }

            // vector:            
            this.Direction = Directions.Up; 

        }
        
        public override void ResetEvents()
        {            
            this.onDirectionChange = null;

            base.ResetEvents();
        }

        public override void Upgrade()
        {
            this.TankLevel++;
        }
        
        // Constructor:
        public CTankPlayer() : base()
        {   
            // Projectiles to shoot at the same time:
            this.ProjectileLimit = 1;
            
            // Speed in pixels per frame:
            this.Speed = 1.50F;

            // Health:
            this.Armour = 1;            

        }       
    }
}
