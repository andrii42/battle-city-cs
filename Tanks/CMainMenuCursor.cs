using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;

namespace Tanks
{
    public class CMainMenuCursor : CAnimatable
    {
        // *********************************
        // Private and protected members:
        // *********************************
        private int[] verticalPosition = { 248, 280, 312 };

        private GameModes[] gameModes = { GameModes.Player1, GameModes.Player2, GameModes.Construct };

        private int positionIndex;

        // *********************************
        // Properties:
        // *********************************
        public GameModes SelectedGameMode
        {
            get
            {
                return gameModes[positionIndex];
            }
        }

        // *********************************
        // Methods:
        // *********************************

        public void MoveUp() 
        {
            if (positionIndex > 0)
            {
                this.Y = verticalPosition[--positionIndex]; 
            }
        }

        public void MoveDown()
        {
            if (positionIndex < 2)
            {
                this.Y = verticalPosition[++positionIndex];
            }
        }

        public CMainMenuCursor(int x, int y, params Image[] img)
        {
            this.X = x;
            this.Y = y;
            this.Width = 32;
            this.Height = 32;
            this.images = img.ToList();
            this.imageChangeInterval = 3;
            this.destroyAfterApear = false;
        }
    }
}
