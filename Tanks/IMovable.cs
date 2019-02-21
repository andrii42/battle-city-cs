using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Tanks
{
    public interface IMovable
    {
        // Direction of the object:
        Directions Direction  
        { get; set; }

        // Speed:
        float Speed 
        { get; set; }

        // Ability to move:
        bool AllowedToMove
        { get; set; }

        // Previous X:
        float PrevX
        { get; set; }

        // Previous Y:
        float PrevY
        { get; set; }

        // X direction:
        int VectorX
        { get; set; }

        // Y direction:
        int VectorY
        { get; set; }

        // Checks if we can turn and move: 
        bool CanTurn
        { get; }

        // Move method:
        void Move();

        // Changing image:
        void ChangeImage();
    }
}
