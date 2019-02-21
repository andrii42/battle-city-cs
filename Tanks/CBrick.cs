using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;

namespace Tanks
{  
       
    public class CBrick : CTile, IResetable
    {
        // *********************************
        // Private and protected members:
        // *********************************
        private Image[] images = { 
                            /*0*/  Properties.Resources.brick_null,
                            /*1*/  Properties.Resources.brick_quarter_downright, 
                            /*2*/  Properties.Resources.brick_quarter_upright,
                            /*3*/  Properties.Resources.brick_half_right,  
                            /*4*/  Properties.Resources.brick_quarter_downleft,
                            /*5*/  Properties.Resources.brick_half_down,
                            /*6*/  null,
                            /*7*/  null,
                            /*8*/  Properties.Resources.brick_quarter_upleft, 
                            /*9*/  null,
                            /*10*/ Properties.Resources.brick_half_up,
                            /*11*/ null,
                            /*12*/ Properties.Resources.brick_half_left,
                            /*13*/ null,
                            /*14*/ null,
                            /*15*/ Properties.Resources.brick_full
                         };
 
       
        private int[] hits = { /*Up*/0, /*Down*/0, /*Left*/0, /*Right*/0 };

        private int[] comparators = { /*Up*/0x0A, /*Down*/0x05, /*Left*/0x0C, /*Right*/0x03 };

        // Current state of the brick:
        private int state;// = 0x0F;

        // *********************************
        // Properties:
        // *********************************

        // Property used to set the image:
        public int State
        {
            get { return state; }
            set { 
                   // Setting state and Tilemap:
                   state = Tilemap = value;
                   
                   // Setting image corresponding to state:
                   this.Image = images[state];
                   
                   // Setting Destroyed field:
                   if (state == (int)BrickState.Destroyed) this.Destroyed = true;
                   
                }
        }

        // *********************************
        // Methods:
        // *********************************
        
        // If brick is hit by the bomb:
        public override void Hit(CProjectile p) 
        {
            // If projectile type is Max then destroying it:
            if (p.ProjectileType == ProjectileTypes.Max) this.State = (int)BrickState.Destroyed;//this.Destroyed = true;

            else if (!this.Fortified)
            {
                // Getting side index:
                int index = (int)p.Direction;

                // Check if brick has already been hit from this side:
                if (++hits[index] >= 2) State = (int)BrickState.Destroyed;

                // If not then change it's state:
                else
                {
                    State &= comparators[index];

                    base.Hit(p);
                }
            }
        }
        
        // Constructor:
        public CBrick(float x, float y)
            : base()
        {
            // Setting drawing priority:
            this.DrawPriority = 3;

            // Setting position:
            this.X = x;
            
            this.Y = y;
            
            // Setting state:
            this.State = (int)BrickState.Full;
        }

        // Resets brick state:
        public void Reset()
        {
            this.State = (int)BrickState.Full;

            this.hits = new int[4];
        }
    }
}
