using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;

namespace Tanks
{
    public interface IGameObject 
    {
        // Image:
        Image Image
        {
            get;
            set;
        }
        // Coordinates:
        float Y
        {
            get;
            set;
        }
        //  
        float X
        {
            get;
            set;
        }
        // Dimensions:
        float Width
        {
            get;
            set;
        }

        float Height
        {
            get;
            set;
        }
        
        // Transparency:
        bool Transparent
        {
            get;
            set;
        }

        bool Visible
        {
            get;
            set;
        }

        // Drawing priority:
        int DrawPriority
        {
            get;
            set;
        }

        // Indicates if object is fortified (can be only hit with ProjectileTypes.Max projectile):
        bool Fortified
        {
            get;
            set;
        }

        // Desproyed property:
        bool Destroyed
        {
            get;
            set;
        }
        // Hit method:
        void Hit(CProjectile p);  

        // Reseting events for current object:
        void ResetEvents();
    }
}
