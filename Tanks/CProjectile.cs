using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;

namespace Tanks
{   

    public class CProjectile : CMovable, IProcessable
    {
        // ********************** //
        //  Private & protected :
        // ********************** //        

        private ProjectileTypes projectileType;

        // ********************** //
        //  Properties :
        // ********************** //

        // Owner of the projectile:
        public object Owner;

        public bool Explode = false;

        public ProjectileTypes ProjectileType
        {
            get { return projectileType; }

            set { projectileType = value; }
        }

        // ********************** //
        //  Events :
        // ********************** //

        // On check if targt is hit event:
        public event OnCheckTargetHit onCheckTargetHit;

        // ********************** //
        //  Methods :
        // ********************** //

        // Process object method:
        public void ProcessObject()
        {
            if ((this.X < 16) || (this.Y < 16) || (this.X > 424) || (this.Y > 424))
            {
                
                Explode = true;
                
                Destroyed = true;                               

            }
 
            else 
            {                
                
                if (onCheckTargetHit(this)) Destroyed = true; 
                
                else Move();                

            }
        }

        public override void ResetEvents()
        {
            this.onCheckTargetHit = null;

            base.ResetEvents();
        }
 
        // Constructor:
        public CProjectile(float x, float y, Directions d, ProjectileTypes type)
        {
            // Loading images:
            this.img_down = new List<Image> { Properties.Resources.projectile_down };

            this.img_up = new List<Image> { Properties.Resources.projectile_up };

            this.img_left = new List<Image> { Properties.Resources.projectile_left };

            this.img_right = new List<Image> { Properties.Resources.projectile_right };           
            
            // Set size in tiles:
            this.Width = 8;
            
            this.Height = 8;
            
            // set coordinates:
            this.X = x + 12;
            
            this.Y = y + 12;
            
            // Set vector:
            this.Direction = d;
            
            // Alowing to move:
            this.AllowedToMove = true;

            this.ProjectileType = type;

            // Speed:
            this.Speed = type == ProjectileTypes.Slow ? 4 : 8;

            // Setting drawing priority:
            this.DrawPriority = 6;
            
        }
        
    }
}
