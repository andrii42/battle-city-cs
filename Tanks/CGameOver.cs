using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;

namespace Tanks
{
    public class CGameOver : CGameObject
    {
        public void DrawElement(Graphics g)
        {
            g.DrawImage(Image, X, Y, Width, Height);
        }

        public CGameOver()
        {
            this.X = 0;
            this.Y = 0;
            this.Transparent = true;
            this.Image = Properties.Resources.GameOverEnd;
            this.Width = 512;
            this.Height = 448;
        }
    }
}
