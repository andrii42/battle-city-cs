using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using System.Windows.Forms;

namespace Tanks
{
   
    public partial class CModel : IKeyProcessor
    {
        // *********************************
        // Private and protected members:
        // *********************************

        // Timer to run game:
        private Timer MoveTimer = new Timer();

        // Current mode:
        private IGameMode gameMode;

        private GameModes lastGameMode;

        private int[] mapBuffer;

        List<int> playersPoints = new List<int>();

        private int hiscore = 20000;

        private int hiscorePlayer1 = 0;

        private int hiscorePlayer2 = 0;
        // *********************************
        // Methods:
        // *********************************

        // Receives a key:
        public void ProcessKeys(KeyMessageID m, KeyEventArgs e)
        {
            gameMode.ProcessKeys(m, e);
        }

        // Draws scene:
        public void DrawScene(PaintEventArgs e)
        {
            gameMode.DrawGameMode(e);
        }

        // This method sets game mode:
        public void SetGameMode(GameModes m)
        {
            MoveTimer.Stop();

            try
            {
                switch (m)
                {
                    case GameModes.Player1:
                        {

                            this.gameMode = new CPlayingMode(this.hiscore,1,mapBuffer);

                            this.mapBuffer = null;

                            (gameMode as CPlayingMode).OnGameOver += OnGameOver;

                            lastGameMode = GameModes.Player1;

                        } break;

                    case GameModes.Player2:
                        {

                            this.gameMode = new CPlayingMode(this.hiscore,2, mapBuffer);

                            this.mapBuffer = null;

                            (gameMode as CPlayingMode).OnGameOver += OnGameOver;

                            lastGameMode = GameModes.Player2;

                        } break;

                    case GameModes.Menu:
                        {

                            this.gameMode = new CMenuMode(playersPoints.Count,hiscore,hiscorePlayer1,hiscorePlayer2);

                            (gameMode as CMenuMode).OnSelectGameMode += OnSelectGameMode;


                        } break;

                    case GameModes.Construct:
                        {

                            this.gameMode = new CConstructionMode();

                            (gameMode as CConstructionMode).OnConstructionModeExit += this.OnGameOver;

                            this.gameMode.InitializeMode();

                        } break;

                    default: break;
                }
            }
            catch (Exception)
            {
                MessageBox.Show("Error loading game mode! Restart application please!");
            }

            MoveTimer.Start();
        }

        // This method moves tank bots:
        public void Run(object sender, EventArgs e)
        {            
            // Running game process:
            gameMode.RunGameMode();           

        }

        // Constructor
        public CModel()
        {          
            // Creating timer for game process:
            MoveTimer.Interval = 5;
            
            MoveTimer.Tick += this.Run;

            // Setting game mode:
            SetGameMode(GameModes.Menu);     
                        
        }

    }
}
