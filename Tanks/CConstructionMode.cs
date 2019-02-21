using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Tanks
{
    public class CConstructionMode : CGameMode
    {
        // *********************************
        // Private and protected members:
        // *********************************
        CConstructionLevel constructionLevel;

        // *********************************
        // Events:
        // *********************************
        public event EventHandler OnConstructionModeExit;

        // *********************************
        // Methods:
        // *********************************
        public override void RunGameMode()
        {
            constructionLevel.ProcessLevel();
        }

        public override void DrawGameMode(PaintEventArgs e)
        {
            constructionLevel.DrawLevel(e.Graphics);
        }

        public override void ProcessKeys(KeyMessageID m, KeyEventArgs e)
        {
            constructionLevel.ReceiveKeyEvent(m, e);
        }

        public override void InitializeMode()
        {
            constructionLevel = new CConstructionLevel();

            constructionLevel.OnLevelExit += this.OnConstructionModeExit;
        }

        public CConstructionMode()
        {
            
        }

    }
}
