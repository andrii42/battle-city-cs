using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;

namespace Tanks
{
    public class CTile : CGameObject
    {
        // Map of the Tile: (0x0F) : 0b1111 : | Left High | Left Low | Right High | Right Low |
        public int Tilemap = 0x0f;

        // Constructor:
        public CTile()
        {
            // Setting height and width:
            this.Width = 16;
            this.Height = 16;
        }
    }
}
