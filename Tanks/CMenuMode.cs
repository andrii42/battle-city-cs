using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Tanks
{
    public class CMenuMode : CGameMode
    {
        // *********************************
        // Private and protected members:
        // *********************************

        private CGameObject mainMenu;
        
        private CMainMenuCursor menuCursor;

        private List<IProcessable> ProcessableObjects = new List<IProcessable>();

        private GameModes selectedGameMode;

        private int hiscore;

        private int hiscoreP1;

        private int hiscoreP2;

        private int playersCount;
        // *********************************
        // Properties:
        // *********************************

        public GameModes SelectedGameMode
        {
            get { return selectedGameMode; }
            set { selectedGameMode = value; }
        }

        // *********************************
        // Events:
        // *********************************
        public event EventHandler OnSelectGameMode;

        // *********************************
        // Methods:
        // *********************************

        public override void RunGameMode()
        {
            if (mainMenu.Y > 0)
            {
                mainMenu.Y -= 2;

                menuCursor.Y -= 2;
            }

            ProcessableObjects.ForEach((p) => p.ProcessObject()); 
        }

        public override void DrawGameMode(PaintEventArgs e)
        {
            // Drawing image of the menu:
            e.Graphics.DrawImage(mainMenu.Image, mainMenu.X, mainMenu.Y, mainMenu.Width, mainMenu.Height);

            // Drawing cursor:
            e.Graphics.DrawImage(menuCursor.Image, menuCursor.X, menuCursor.Y, menuCursor.Width, menuCursor.Height);

            // Drawing points:
            CGameTextPainter.WriteCaption(e.Graphics, (int)mainMenu.X + 250, (int)mainMenu.Y + 30, string.Format("{0,5:00}",this.hiscore));

            CGameTextPainter.WriteCaption(e.Graphics, (int)mainMenu.X + 68, (int)mainMenu.Y + 30, string.Format("{0,5:00}", this.hiscoreP1));

            if (this.playersCount > 1) CGameTextPainter.WriteCaption(e.Graphics, (int)mainMenu.X + 380, (int)mainMenu.Y + 30, string.Format("{0,5:00}", this.hiscoreP2));
        }

        public override void ProcessKeys(KeyMessageID m, KeyEventArgs e)
        {            
            if (m == KeyMessageID.KeyDown)
            {
                if (mainMenu.Y > 0)
                {
                    
                    mainMenu.Y = 0;

                    menuCursor.Y = 247;

                }
                else
                {
                    switch (e.KeyCode)
                    {
                        case Keys.Home: menuCursor.MoveUp(); 
                            break; 

                        case Keys.End: menuCursor.MoveDown(); 
                            break;

                        case Keys.Enter:

                            SelectedGameMode = menuCursor.SelectedGameMode;

                            if (OnSelectGameMode != null) OnSelectGameMode(this, new EventArgs());

                            break;

                        default: break;
                    }
                }
            }
        }

        public override void InitializeMode()
        {
            
        }

        public CMenuMode(int playersCount, int hiscore, int hiscoreP1, int hiscoreP2)
        {
            var backgroudImage = playersCount > 1 ? Properties.Resources.Menu_2p : Properties.Resources.Menu_1p;

            this.hiscore = hiscore;

            this.hiscoreP1 = hiscoreP1;

            this.hiscoreP2 = hiscoreP2;

            this.playersCount = playersCount;

            mainMenu = new CGameObject(backgroudImage, 0, 448, 512, 448);
            
            menuCursor = new CMainMenuCursor(135, 695, Properties.Resources.Player1tank_right1, Properties.Resources.Player1tank_right2);
            
            ProcessableObjects.Add(menuCursor);
        }
    }
}
