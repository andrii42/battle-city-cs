using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;

namespace Tanks
{
    public class StatElement : IProcessable
    {
        // ********************** //
        //  Private & protected :
        // ********************** //
        int Y = 0;

        int pointsMultiplier;

        int[] enemiesHit = {0,0};

        int[] displayedHit = {0,0};

        int[] Xs = { 200, 300, 60, 350 };

        public bool Done = false;

        bool total;

        int pause = 50;

        // ********************** //
        //  Properties:
        // ********************** //

        int Pause
        {
            get { return pause; }

            set { 
                 pause = value;
                 if (pause < 0) pause = 25;
                }
        }

        // ********************** //
        //  Methods:
        // ********************** //
        public StatElement(int y, bool total, int pointsmultiplier, params int[] enemieshit)
        {            
            Y = y;

            pointsMultiplier = pointsmultiplier;

            enemiesHit = enemieshit;

            this.total = total;

            if (this.total) displayedHit = enemieshit;

        }

        public void ProcessObject()
        {
            int i;

            if (Pause-- == 0)
            {
                bool done = true;

                for (i = 0; i < enemiesHit.Count(); i++)
                {

                    if (displayedHit[i] < enemiesHit[i])  
                    {
                        displayedHit[i]++;
                        
                        done = false;
                    }

                }

                Done = done; 
            }
        }

        public void DrawStatElement(Graphics g)
        {
            
            for (int i = 0; i < enemiesHit.Count(); i++)
            {
                // Drawing hits:
                g.DrawString(displayedHit[i].ToString(), new Font(FontFamily.GenericMonospace,18,FontStyle.Bold) , Brushes.White, new PointF(Xs[i],Y));
                
                // Drawing Points:
                if (!this.total) g.DrawString((displayedHit[i]*100*pointsMultiplier).ToString(), new Font(FontFamily.GenericMonospace, 18, FontStyle.Bold), Brushes.White, new PointF(Xs[i+2], Y));
            }
        }

    }

    public class CStatisticsViewer : CGameObject, IProcessable
    {
        // ********************** //
        //  Private & protected :
        // ********************** //
        private int[] Ys = { 170, 218, 267, 315 , 350 };

        private int[][] playersHits = { new int[4], new int[4] };

        private int[][] playersPoints = { new int[4], new int[4] };

        private CTankPlayer[] players;

        private Image[] images = { Properties.Resources.Statistics_1p, Properties.Resources.statistics_2p };                

        private List<StatElement> statElements = new List<StatElement>();
                
        private int lifeTime = 100;

        private int hiScore;

        private int levelNo;

        private int currentStatElementIndex = 0;

        private StatElement currentStatElement;

        private GameStatuses gameStatus;

        // ********************** //
        //  Events :
        // ********************** //

        // On statistics done event:
        public event EventHandler<LevelEventArgs> OnEnd;

        // ********************** //
        //  Methods :
        // ********************** //

        // Draws statistics:
        public void DrawStatistics(Graphics g)
        {            
            // Drawing background image:
            g.DrawImage(this.Image, this.X, this.Y, this.Width, this.Height);
            
            // Drawing players points:
            g.DrawString(players[0].Points.ToString(), new Font(FontFamily.GenericMonospace, 18, FontStyle.Bold),Brushes.Orange, new PointF(90,130));

            // Drawing hiscore:
            g.DrawString(this.hiScore.ToString(), new Font(FontFamily.GenericMonospace, 18, FontStyle.Bold), Brushes.Orange, new PointF(300, 26));

            // Drawing level number:
            g.DrawString(this.levelNo.ToString(), new Font(FontFamily.GenericMonospace, 18, FontStyle.Bold), Brushes.White, new PointF(300, 58));

            // Drawing statistics:
            for (int i = 0; i <= currentStatElementIndex; i++) statElements[i].DrawStatElement(g);
        }

        // Processes statistics:
        public void ProcessObject()
        {
            // Processing current statisctics row:
            currentStatElement.ProcessObject();

            // If it is done, moving to next:
            if ((currentStatElement.Done) && (currentStatElementIndex < (statElements.Count - 1)))  currentStatElement = statElements[++currentStatElementIndex];

            // If all done, then counting down lifetime
            if (statElements.Last().Done)
            {               
                // if lifetime ended, calling OnEnd event:
                if ((--this.lifeTime < 1) && (OnEnd != null))  OnEnd(this, new LevelEventArgs(this.gameStatus));
            }

        }

        private void CreateStaisticsElements()
        {
            // Reading players statistics:
            for (int i = 0; i < this.players.Count(); i++) this.playersHits[i] = this.players[i].GetEnemiesHit();

            // Adding statistics elements:
            switch (players.Count())
            {
                case 1:
                    {
                        for (int i = 0; i < 4; i++) statElements.Add(new StatElement(Ys[i], false, i + 1, playersHits[0][i]));

                        statElements.Add(new StatElement(Ys[4], true, 0, playersHits[0].Sum()));

                    } break;

                case 2:
                    {
                        for (int i = 0; i < 4; i++) statElements.Add(new StatElement(Ys[i], false, i + 1, playersHits[0][i], playersHits[1][i]));

                        statElements.Add(new StatElement(Ys[4], true, 0, playersHits[0].Sum(), playersHits[1].Sum()));

                    } break;

                default: break;
            }
           
            // Setting current statistics element:
            currentStatElement = statElements[currentStatElementIndex];

        }

        public CStatisticsViewer(GameStatuses gs, int levelno, int hiscore, CTankPlayer[] p)
        {
            this.players = p;

            this.Image = images[p.Count() - 1];
            
            this.X = 0;
            
            this.Y = 0;
            
            this.Width = 512;
            
            this.Height = 448;
            
            this.gameStatus = gs;

            this.levelNo = levelno;

            this.hiScore = hiscore;

            CreateStaisticsElements();
            
        }
    }
}
