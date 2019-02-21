using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using System.Windows.Forms;
using System.Xml;
using System.Xml.Linq;

namespace Tanks
{
    /// <summary>
    /// This class is for game mode:
    /// </summary>
    public abstract class CGameMode : IGameMode  
    {
        // Random coordinates generator:
        protected static Random random = new Random();
        
        public abstract void RunGameMode();

        public abstract void InitializeMode();

        public abstract void DrawGameMode(PaintEventArgs e);

        public abstract void ProcessKeys(KeyMessageID m, KeyEventArgs e);

        // Constructor:
        public CGameMode()
        {

        }
    }
}
