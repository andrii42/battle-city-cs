using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;


namespace Tanks
{
    public class CMovableEx : CGameObject, IMovable
    {

        // *********************************
        // Private and protected members:
        // *********************************

        // indicates if we can move:
        private bool allowedToMove = false;

        // Speed:
        private float speed;                     

        // Index of the current image:
        private int imageIndex = 0;

        // Previous coordinates:
        // Previous X:
        private float prevX;               
        
        // Previous Y:
        private float prevY;

        // Counter of moves:
        private int moveCounter;

        // Vectors of the movement:
        private int vectorX;

        private int vectorY;

        private int imageChangeInterval = 4;

        // A random number for changing vector randomly
        protected static Random rnd = new Random();

        // Images for animation:
        protected List<Image> img_up;

        protected List<Image> img_down;

        protected List<Image> img_left;

        protected List<Image> img_right;

        protected List<Image> img_current;

        protected List<Image>[] images = new List<Image>[4];

        protected Directions direction;   

        // *********************************
        // Properties:
        // *********************************

        public float PrevX
        {
            get { return prevX; }
            set { prevX = value; }
        }

        public float PrevY
        {
            get { return prevY; }

            set { prevY = value; }
        }

        // Coordinates of movable object:
        public override float Y
        {
            get { return y; }

            set {
                  
                  prevY = y;
                  
                  y = value;
                  
                }
        }

        public override float X
        {
            get { return x; }

            set {
                  
                  prevX = x; 
                  
                  x = value;
                 
                }
        }        

        // X vector:
        public int VectorX
        {
            get { return vectorX; }
            set
            {
                if (value != 0)
                {
                    img_current = value == -1 ? img_left : img_right;
                    vectorY = 0;
                }
                vectorX = value;
            }
        }

        // Y vector:
        public int VectorY
        {
            get { return vectorY; }
            set
            {
                if (value != 0)
                {
                    img_current = value == -1 ? img_up : img_down;
                    vectorX = 0;
                }
                vectorY = value;
            }
        }       

        protected int ImageIndex
        {
            get { return imageIndex; }
            set {
                imageIndex = (value < img_current.Count()) ? value : 0;
             }
        }

        // Current image for animation effect:
        public override Image Image
        {

            get { return image; }

            set { image = value; }
   
        }         
        
        // Current direction of the movement:
        public virtual Directions Direction
        {
            get { return this.direction; }
            
            set
            {
                switch (value)
                {
                    case Directions.Up: VectorY = -1; break;
                    
                    case Directions.Down: VectorY = 1; break;
                    
                    case Directions.Left: VectorX = -1; break;
                    
                    case Directions.Right: VectorX = 1; break;
                }
                
                direction = value;

                ImageIndex = 0;
            }
        }        

        public float Speed
        {
            get { return speed; }
            set { speed = value; }
        }        
       
        public int MoveCounter
        {
            get { return moveCounter; }
            set { moveCounter = (value > this.ImageChangeInterval) ? 0 : value; }
        }
                
        // Checks if we can turn and move: 
        public bool CanTurn
        {
            get
            {
                if ((Math.Truncate(this.Y % 16) == 0) && (Math.Truncate(this.X % 16) == 0)) return (true); else return (false);
            }
        }

        public bool AllowedToMove
        {
            get { return allowedToMove; }
            set { allowedToMove = value; }
        }

        public int ImageChangeInterval
        {
            get { return imageChangeInterval; }
            set { imageChangeInterval = value; }
        }

        // *********************************
        // Events:
        // *********************************

        // On move event:
        public event OnMove onMove;


        // *********************************
        // Methods:
        // *********************************

        public virtual void ChangeImage()
        {
            if (MoveCounter++ == 0) ImageIndex++;
        }

        // This method generates a random vector:
        public void RandomDirection()
        {            
            this.Direction = (Directions)rnd.Next(0, 3);
        }

        // Reverses vector:
        public void ReverceDirection()
        {
            switch (Direction)
            {
                case Directions.Up: Direction = Directions.Down; break;

                case Directions.Down: Direction = Directions.Up; break;

                case Directions.Left: Direction = Directions.Right; break;

                case Directions.Right: Direction = Directions.Left; break;
            }          
 
        }

        public override void ResetEvents()
        {
            
            this.onMove = null;

            base.ResetEvents();

        }

        // Moves this object:
        public virtual void Move()
        {
            // Incrementing or position with vectors:
            if (this.AllowedToMove)
            {
                // Changing coordinates:
                X += VectorX * this.Speed;

                Y += VectorY * this.Speed;

                // Changing image:
                //ImageIndex++;
            }
           
           // Raising onMove event:
           //this.onMove(this);

     
        }

        // Constructor:
        public CMovableEx()
        {
            
        }

    }
}
