using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using System.Windows.Forms;
using System.Threading;
using System.Runtime.CompilerServices;

namespace Tanks
{

    public class CPlayingMode : CGameMode, IKeyProcessor
    {
        // *********************************
        // Private and protected members:
        // *********************************

        private Keys[] ControlKeysP1 = { 
                                        /*Move Up:   */ Keys.Home, 
                                        /*Move Down: */ Keys.End,
                                        /*Move Left: */ Keys.Delete,
                                        /*Move Right:*/ Keys.PageDown,
                                        /*Shoot:     */ Keys.Space
                                      };

        private Keys[] ControlKeysP2 = { 
                                        /*Move Up:   */ Keys.W, 
                                        /*Move Down: */ Keys.S,
                                        /*Move Left: */ Keys.A,
                                        /*Move Right:*/ Keys.D,
                                        /*Shoot:     */ Keys.M
                                      };

        private List<Keys[]> ControlKeys = new List<Keys[]>();

        // Construction map:
        private int[] constructionMap;

        // Game over timeout:
        private int gameOverTimeout = 200;

        // Number of the level:
        private int levelNumber = 0;

        // Hiscore:
        private int hiScore;

        // Current level:
        private CPlayLevel level;

        // Current scene painter:
        private GamePainter gamePainter;

        // GameStatus:
        private GameStatuses currentGameStatus;

        // List of players:
        private List<CTankPlayer> players = new List<CTankPlayer>();

        private CStatisticsViewer levelStatistics;

        // Key adapters:
        private List<CKeyPressAdapter> keyPressAdapters = new List<CKeyPressAdapter>();

        // *********************************
        // Properties:
        // *********************************

        public List<CTankPlayer> Players
        {
            get { return players; }

            private set { players = value; }
        }

        // Public members:
        public int LevelNumber
        {
            get { return levelNumber; }
            set
            {
                if (value > 36) levelNumber = 1; else levelNumber = value;
            }
        }

        public GameStatuses CurrentGameStatus
        {
            get { return currentGameStatus; }
            private set { currentGameStatus = value; }
        }

        // *********************************
        // Events:
        // *********************************

        // On Game over event:
        public event EventHandler OnGameOver;

        // *********************************
        // Methods:
        // *********************************

        // OnPaint handler:
        public override void DrawGameMode(PaintEventArgs e)
        {
            // Drawing all objects:
            if (gamePainter != null) gamePainter.Invoke(e.Graphics);

        }

        // Processes key press:
        public override void ProcessKeys(KeyMessageID m, KeyEventArgs e)
        {
            // When key pressed, processing key adapters:
            foreach (CKeyPressAdapter keyadapter in keyPressAdapters) keyadapter.ProcessKeys(m, e);

            if (level != null) level.ReceiveKeyEvent(m, e);

        }

        // Shows statistics:
        void OnShowStatistics(object sender, EventArgs e)
        {
            try
            {
                // Setting status to show statistics:
                this.CurrentGameStatus = GameStatuses.ShowStatistics;

                // Getting players:
                CTankPlayer[] players = (sender as CPlayLevel).Players.ToArray();

                // Modify hiscore if any player hits it:
                foreach (CTankPlayer p in players) if (p.Points > hiScore) hiScore = p.Points;

                // Creating statistics, taking game status and player from level:
                this.levelStatistics = new CStatisticsViewer((e as LevelEventArgs).GameStatus, this.levelNumber, this.hiScore, players);

                // Setting event that shoud occur when statistics shown
                this.levelStatistics.OnEnd += OnStatisticsEnd;

                // Setting game painter to statistics:
                this.gamePainter = levelStatistics.DrawStatistics;

            }
            catch (Exception)
            {
                MessageBox.Show("Error on statistics show! Going to menu...");

                this.gamePainter = null;
            }

        }

        // Loads new level:
        void LoadLevel()
        {
            // Clearing key press adapter:
            this.keyPressAdapters.Clear();

            try
            {
                // Creating a level:
                this.level = new CPlayLevel(++LevelNumber, Players, constructionMap);

                // Setting statistics to show whel level completed:
                this.level.OnLevelExit += OnShowStatistics;

                // Setting key press adapter for player 1:
                for (int i = 0; i < Players.Count; i++) this.keyPressAdapters.Add(new CKeyPressAdapter(ControlKeys[i], Players[i]));

                // Setting scene painter:
                this.gamePainter = level.DrawLevel;

                // Setting current status to Playing:
                this.CurrentGameStatus = GameStatuses.Playing;
            }

            catch (Exception)
            {
                MessageBox.Show("Error loading level!");

                this.gamePainter = null;
            }

        }

        public void OnStatisticsEnd(object sender, LevelEventArgs e)
        {
            this.CurrentGameStatus = (e as LevelEventArgs).GameStatus;
        }

        // Runs game mode:
        public override void RunGameMode()
        {
            switch (CurrentGameStatus)
            {
                case GameStatuses.Playing:

                    // Processing level:
                    level.ProcessLevel();

                    break;

                case GameStatuses.PlayerWin:

                    // Loading next level:
                    LoadLevel();

                    break;

                case GameStatuses.ShowStatistics:

                    // If statistics created, process it:
                    if (levelStatistics != null) levelStatistics.ProcessObject();

                    break;

                case GameStatuses.GameOver:

                    // GAME OVER screen:
                    if (gameOverTimeout == 200) gamePainter = new CGameOver().DrawElement;

                    // After timeout initialize OnGameOver event:
                    if ((OnGameOver != null) && (gameOverTimeout-- < 0)) OnGameOver(this, new EventArgs());

                    break;
            }

        }

        public override void InitializeMode()
        {

        }


        // Constructor:
        public CPlayingMode(int hiscore,int playersCount, int[] map)
        {
            this.hiScore = hiscore;

            Players.Add(new CTankPlayer1());

            this.constructionMap = map;

            if (playersCount == 2) Players.Add(new CTankPlayer2());

            ControlKeys.Add(ControlKeysP1);

            ControlKeys.Add(ControlKeysP2);

            // Loading level:
            LoadLevel();

        }

    }
}
