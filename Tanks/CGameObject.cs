using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;

namespace Tanks
{
    public class CGameObject : IGameObject, IComparable
    {
        // *********************************
        // Private and protected members:
        // *********************************
        // Coordinates of current object
        protected float x;

        protected float y;

        // Current image of the object:
        protected Image image;

        // Dimantions of current object:
        private float width;

        private float height;       

        // Indicates if object is transparent:
        private bool transparent;

        private bool visible = true;

        // Indicates if object is fortified (can be only hit with ProjectileTypes.Max projectile):
        private bool fortified = false;

        // Indicates if object destroyed:
        private bool destroyed;

        private int drawPriority = 0;

        // *********************************
        // Properties:
        // *********************************

        public virtual Image Image
        {

            get { return image; }

            set { image = value; }

        }

        // Incapsulated coordinates:
        public virtual float Y
        {

            get { return y; }

            set { y = value; }

        }

        //  
        public virtual float X
        {

            get { return x; }

            set { x = value; }

        }               

        public float Width
        {
            
            get { return width; }

            set { width = value; }

        }

        public float Height
        {
            
            get { return height; }

            set { height = value; }

        }       
               
        public bool Transparent
        {
            
            get { return transparent; }
            
            set { transparent = value; }

        }
        
        public bool Visible
        {
            get { return visible; }
            set { visible = value; }
        }       

        public bool Fortified
        {
            get { return fortified; }
            set { fortified = value; }
        }       

        // Destroyed property:
        public bool Destroyed
        {
            get { return destroyed; }

            set
            {

                if ((value && !destroyed) && (onDestroy != null))
                {
                    
                    destroyed = value;
                    
                    onDestroy(this,new EventArgs());

                }

                else destroyed = value;

            }
        }       

        // Drawing priority:
        public int DrawPriority
        {
            get { return drawPriority;}
            set { drawPriority = value; }
        }       

        // *********************************
        // Events:
        // *********************************

        // On destroy:
        public event EventHandler onDestroy;

        // On Hit:
        public event OnHit onHit;

        // *********************************
        // Methods:
        // *********************************

        // Hit method:
        public virtual void Hit(CProjectile p)
        {
            if (onHit != null) onHit(this);
        }

        // IComparable implementation:
        public int CompareTo(object obj)
        {
            IGameObject gO = obj as IGameObject;

            int result = 0;

            if (gO == null) throw new FormatException();

            else
            {                
                if (gO.DrawPriority != this.DrawPriority) result = (gO.DrawPriority > this.DrawPriority) ? 1 : -1;
            }

            return result; 
        }

        // Reset events for current object:
        public virtual void ResetEvents()
        {
            this.onDestroy = null;

            this.onHit = null;
        }

        // Default constructor:
        public CGameObject() 
        {

        }
        
        public CGameObject(Image image, float x, float y, float width, float height)
        {
            this.Image = image;

            this.X = x;

            this.Y = y;

            this.Width = width;

            this.Height = height;
        }

    }
}
