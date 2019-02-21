using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Tanks
{
    public class CGameOverMessage : CGameObject, IProcessable
    {
        // Lifetime of the "GAME OVER" caption in frames:
        private int lifeTime = 400;

        // Constructor:
        public CGameOverMessage()
        {
            this.Image = Properties.Resources.GameOver;
            this.X = 200;
            this.Y = 520;
            this.Width = 62;
            this.Height = 30;
            this.Transparent = true;
            this.DrawPriority = 11;

        }
        
        // IProcessable implementation:
        public void ProcessObject()
        {
            
            if (this.Y > 220) this.Y -= 2;

            if (this.lifeTime-- < 0) this.Destroyed = true;

        }
    }
}
