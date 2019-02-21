using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;

namespace Tanks
{
    public abstract class CAnimatable : CGameObject, IProcessable
    {
        // *********************************
        // Private and protected members:
        // *********************************

        // Frame counter:
        private int frame_counter = 1;

        // Pause before oject apear:
        private int pauseBeforeApear;

        // Index of the current image:
        private int image_index = 0;

        // Images of the object:
        protected List<Image> images;

        // Interval to change images:
        protected int imageChangeInterval = 1;

        // Indicates if object shoud be destroyed after apearens:
        protected bool destroyAfterApear = true;


        // *********************************
        // Properties and public members :
        // *********************************

        protected int ImageIndex
        {
            get { return image_index; }

            set
            {

                if (value < images.Count()) image_index = value;

                else
                {
                    if (destroyAfterApear) Destroyed = true; else image_index = 0;
                }

            }
        }

        public int PauseBeforeApear
        {
            get { return pauseBeforeApear; }
            set
            {
                pauseBeforeApear = value;
            }
        }

        // new Image property:
        public override Image Image
        {
            get
            {
                return images[ImageIndex];
            }

            set { image = value; }
        }


        // *********************************
        // Methods:
        // *********************************      
                
        // Constructor:
        public CAnimatable()
        {            
            this.Transparent = true;
        }

        public virtual void ProcessObject()
        {

            if (PauseBeforeApear > 0) PauseBeforeApear--;

            // Changing image index:
            else
            {
                if (!Visible) Visible = true;

                if (frame_counter++ % this.imageChangeInterval == 0) ImageIndex++;
            }
        }
    }
}
