using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Drawing;
using System.Threading;
using System.Windows;
using System.Windows.Forms;

namespace Tanks
{   

    public partial class CPlayLevel : CLevel, ILevel
    {
        // *********************************
        // Private and protected members:
        // *********************************

        int tmp = 0;
       
        // Matrix for projectiles:
        private CProjectile[,] mapProjectiles = new CProjectile[56, 56];

        // Coordinates of HQ fence:
        private int[] hqXs = { 192, 208, 224, 240, 192, 240, 192, 240 };
        private int[] hqYs = { 384, 384, 384, 384, 400, 400, 416, 416 };

        // Respawn points for enemies:
        private System.Drawing.Point[] EnemyRespawnPoints = { new System.Drawing.Point(16,16) , new System.Drawing.Point(208,16), new System.Drawing.Point(400,16) };
    
        // Respawn points for players:
        private System.Drawing.Point[] PlayerRespawnPoints = { new System.Drawing.Point(144, 400), new System.Drawing.Point(272, 400) };

        //
        Tanks[] EnemyTanks = { Tanks.Enemy1, Tanks.Enemy1, Tanks.Enemy1, Tanks.Enemy1, Tanks.Enemy1, Tanks.Enemy1, Tanks.Enemy1, Tanks.Enemy1, Tanks.Enemy1, Tanks.Enemy2, Tanks.Enemy2, Tanks.Enemy2, Tanks.Enemy2, Tanks.Enemy1, Tanks.Enemy2, Tanks.Enemy4, Tanks.Enemy4, Tanks.Enemy4, Tanks.Enemy4, Tanks.Enemy4 };                      

        private int timeoutToNextLevel = 300;

        // Enemy tanks count:
        private int enemyTanksLeft = 20;

        // indexator for respawn points:
        private int respawnPointIndex;       

        // Number of the level:
        private int levelNumber;

        private int[] constructionMap;

        // Indicates if level is started:
        private bool levelStarted;

        // Current game status:
        private GameStatuses gameStatus;               

        // Curtain that a shown when level starts:
        private CLevelCurtain levelCurtain;

        // Random numbers generator:
        private static Random random = new Random();

        // Empty area for map:
        private CTile emptyArea = new CTile();
               
        // List of objects that shoud receive key events:
        private List<IKeyEventReceiver> KeyEventReceivers = new List<IKeyEventReceiver>();

        // Statistic objects list:
        private List<IGameObject> StatisticObjects = new List<IGameObject>();

        private CTankPlayer player1;

        private CTankPlayer player2;

        // Matrix of tiles:
        protected CGameObject[,] mapMain = new CGameObject[56, 56];         

        // *********************************
        // Events:
        // *********************************

        // Occures when level completes:
        public event EventHandler<LevelEventArgs> OnLevelExit;

        // *********************************
        // Properties and public members:
        // *********************************

        // Players:
        public List<CTankPlayer> Players = new List<CTankPlayer>();   

        // Property for indexator of the respawn points::
        public int RespawnPointIndex
        {
            get { return respawnPointIndex; }
            set { respawnPointIndex = (value < EnemyRespawnPoints.Count()) ? value : 0; }
        }

        // For level number:
        public int LevelNumber
        {
            get { return levelNumber; }
            set { levelNumber = value; }
        }

        // Shows how many enemies left to beat:
        public int EnemyTanksLeft
        {
            get { return enemyTanksLeft; }
            set { enemyTanksLeft = value; }
        }

        public bool LevelStarted
        {
            get { return levelStarted; }
            set { levelStarted = value; }
        }        

        // Shows current game status:
        public GameStatuses GameStatus
        {
            get { return gameStatus; }
            set { gameStatus = value; }
        }

        // *********************************
        // Methods:
        // *********************************
        // - - - - - - - - - -
        // Managing objects :
        // - - - - - - - - - -

        // This method sets object on a map:
        protected override void AddObject(CGameObject o)
        {
            // Adding to all objects:
            AllGameObjects.Add(o);            
                        
            // if it is processable then add it to corresponding list:
            if (o is IProcessable) ProcessableObjects.Add((IProcessable)o);

            if ((o is CIconEnemy) || (o is ClevelFlag) || (o is CPlayerIcon)) StatisticObjects.Add(o);

            // Set event handlers:
            SetEventHandlers(o);

            //Add to map:
            CGameObject[,] map;

            // Choosing the right map:            
            map = (o is CProjectile) ? mapProjectiles : mapMain;

            // check if transparent:
            if (!o.Transparent)
            { 
                int x, y, width, height;

                GetDimensionsOnMap(o, out x, out y, out width, out height);

                // Put on the map:
                for (int i = 0; i < width; i++)
                    
                    for (int j = 0; j < height; j++)
                    {
                        map[x + j, y + i] = o;
                    }
            }
        }

        // This method removes object from map:
        protected override void RemoveObject(CGameObject o)
        {            
            // Removing from all objects:           
            AllGameObjects.Remove(o);
                      
            //
            if (o is IProcessable) ProcessableObjects.Remove((IProcessable)o);

            if ((o is CIconEnemy) || (o is ClevelFlag) || (o is CPlayerIcon)) StatisticObjects.Remove(o);

            // Removing from map:
            CGameObject[,] map;
            
            // Choosing the right map:            
            map = (o is CProjectile) ? mapProjectiles : mapMain;
            
            // if object is not transparent then removing it:        
            if (!o.Transparent)
            {
                int x, y, width, height;

                GetDimensionsOnMap(o, out x, out y, out width, out height);

                // Removing from map:
                for (int i = 0; i < width; i++)

                    for (int j = 0; j < height; j++)
                    {

                        map[x + j, y + i] = null;

                    }
            }
            
        }        

        // Sets object on map:
        void SetObjectOnMap(CGameObject o)
        {
            int x, y, width, height;

            GetDimensionsOnMap(o, out x, out y, out width, out height);

            // Put our tank on the map:
            for (int i = 0; i < width; i++)
                for (int j = 0; j < height; j++)
                {
                    mapMain[x + j, y + i] = o;
                }
        }

        // Removes object from map:
        void RemoveObjectFromMap(CGameObject o)
        {

            int x, y, width, height;

            GetDimensionsOnMap(o, out x, out y, out width, out height);

            // Put our tank on the map:
            for (int i = 0; i < width; i++)
                for (int j = 0; j < height; j++)
                {
                    mapMain[x + j, y + i] = null;
                }            
        }

        // Removes object from map overloaded:
        void RemoveObjectFromMap(float ox, float oy, float w, float h)
        {            
            int x = (int)(ox / cellSize);
            
            int y = (int)(oy / cellSize);
            
            int width = (int)(w / cellSize);
            
            int height = (int)(h / cellSize);
            
            // Put our tank on the map:
            for (int i = 0; i < width; i++)
                for (int j = 0; j < height; j++)
                {
                    mapMain[x + j, y + i] = null;
                }
        }

        // Resets Tile on map:
        void ResetTileOnMap(CTile o)
        {

            int x, y, width, height;

            GetDimensionsOnMap(o, out x, out y, out width, out height);
            
            int comparator = 8;
            //
            // Put our tank on the map:
            for (int i = 0; i < width; i++)
                for (int j = 0; j < height; j++)
                {
                    if ((comparator & o.Tilemap) != 0) mapMain[x + i, y + j] = o; else mapMain[x + i, y + j] = this.emptyArea;
                    //
                    comparator >>= 1;
                }
        }

        // Clears position for object:
        void ClearPositionForObject(IGameObject o)
        {

            int x, y, width, height;

            GetDimensionsOnMap(o, out x, out y, out width, out height);

            // Put our tank on the map:
            for (int i = 0; i < width; i++)
                for (int j = 0; j < height; j++)
                {
                    var go = mapMain[x + j, y + i];
                    
                    if ((go != null) && !(go is CTank)) RemoveObject(go);
                }
        }

        // Check if object can take its position on map:
        bool CheckPosition(IGameObject o)
        {

            int x, y, width, height;

            GetDimensionsOnMap(o, out x, out y, out width, out height);

            // Checking map:
            for (int i = 0; i < width; i++)
                for (int j = 0; j < height; j++)
                {
                    if (mapMain[x + j, y + i] != null) return false;
                }

            return true;
        }

        void GetDimensionsOnMap(IGameObject o ,out int x, out int y, out int width, out int height)
        {
            x = (int)(o.X / cellSize);

            y = (int)(o.Y / cellSize);

            width = (int)(o.Width / cellSize);

            height = (int)(o.Height / cellSize);
        }

        public void ClearLevel()
        {
            // Removing objects from ProcessableObjects:
            ProcessableObjects.Clear();

            // Remove objects from container:
            AllGameObjects.Clear();
        }

        // - - - - - - - - - 
        // Creators :
        // - - - - - - - - - 

        // Set HQ on map:
        void CreateHQ()
        {                 
            // Creating fortification for eagle:
            for (int i = 0; i < 8; i++)
            {
                var brick = CObjectCreator.CreateStaticObject(StaticObjects.Brick, hqXs[i], hqYs[i]);

                if ( CheckPosition(brick) ) AddObject((CGameObject)brick);
            }

            // Creating and adding Eagle:
            var eagle = new CEagle();                     
                        
            ClearPositionForObject(eagle);

            // Adding Eagle:
            this.AddObject((CGameObject)eagle);
        }
            
        // Creates tank portal:
        void CreateTankPortal(CTank tank)
        {          

            CTankPortal portal = null;

            // Creating portal:
            switch (tank.TankID)
            {

                case Tanks.Player1: portal = new CTankPortal(0, PlayerRespawnPoints[0].X, PlayerRespawnPoints[0].Y, tank); break;

                case Tanks.Player2: portal = new CTankPortal(0, PlayerRespawnPoints[1].X, PlayerRespawnPoints[1].Y, tank); break;

                default:
                    {

                        portal = new CTankPortal(100,EnemyRespawnPoints[RespawnPointIndex].X, EnemyRespawnPoints[RespawnPointIndex].Y,tank);

                        RespawnPointIndex++;

                    } break;
            }

            // Setting coordinates for tank:
            portal.Tank.X = portal.X;

            portal.Tank.Y = portal.Y;

            // Cleaning position for portal:
            ClearPositionForObject(portal);

            // Adding portal to objects:
            AddObject(portal);
        }              

        // Create explotion:
        void CreateSmallExplotion(IGameObject o) 
        {
            // Creating explotion:
            var explotionSmall = new CExplotion((CGameObject)o, 2); ;//new CExplosionSmall(o.X, o.Y);            
            
            // Adding to lists:
            AddObject(explotionSmall);

        }
        
        // Create big explotion:
        void CreateBigExplotion(IGameObject o)
        {
            // Creating explotion:
            var explotionBig = new CExplotion((CGameObject)o, 8); //new CExplosionBig((CGameObject)o);

            // Adding to lists:
            AddObject(explotionBig);

        }       

        // Create statistics block:
        void CreateStatisticsBlock()
        {
            int x = 0;// = 460;
            int y = 16;// = 0;

            //
            AddObject(new ClevelFlag(450, 370));

            AddObject(new CPlayerIcon(450, 300));

            // Setting statistics block:
            for (int i = 0; i < EnemyTanksLeft; i++)
            {
                if (i % 2 == 0)
                {
                    x = 450;
                    y += 16;
                }
                else x += 16;

                // Adding directly to container:
                AddObject(new CIconEnemy(x, y));

            }            
        }

        // NEW LoadMapFromFile:
        void CreateMap()
        {
            // Getting map from container:
            int[] map = ((this.LevelNumber) == 1 && (this.constructionMap != null)) ? this.constructionMap : CMapContainer.GetMap(this.LevelNumber);
            
            // Creating level objects:
            for (int i = 0; i < 26; i++)
            {
                for (int j = 0; j < 26; j++)
                {
                    var go = (StaticObjects)map[i*26+j];//File.ReadByte();

                    if (go != StaticObjects.Empty)
                    {
                        int x = (i + 1) * 16;

                        int y = (j + 1) * 16;

                        var new_go = CObjectCreator.CreateStaticObject(go, x, y);

                        //SetEventHandlers((CGameObject)new_go);

                        AddObject((CGameObject)new_go);
                    }
                }
            }

        }

        void CreateBonus()
        {
            // Removing previous bonus:
            var bonus = ProcessableObjects.Find(p => {
                                                        if (p is CBonus) return !(p as CBonus).Taken; 
                                                       
                                                        else return false;
                                                      } 
                                               );

            if (bonus != null) RemoveObject((CGameObject)bonus);

            // Creating random bonus at random position:
            AddObject(CObjectCreator.CreateBonus(random.Next(16,400), random.Next(16,400), (Bonuses)random.Next(0,6)));                       
        }

        void CreateLevelCurtain()
        {
            // Creating curtain:
            levelCurtain = new CLevelCurtain(this.LevelNumber);

            // Settting event handlers:
            levelCurtain.OnHalfCompleted += LoadMap;

            levelCurtain.OnCompleted += this.InitializeLevel;

            levelCurtain.OnCompleted += this.OnComplete_LevelCurtain;

            // Adding to processable objects:
            ProcessableObjects.Add(levelCurtain);

            // Adding to key receivers:
            KeyEventReceivers.Add(levelCurtain);
        }

        // - - - - - - - - - 
        // Configurators:
        // - - - - - - - - - 

        // Sets event handlers for object:
        void SetEventHandlers(CGameObject o)
        {
            // - - -
            if (o is CBrick)
            {
                
                o.onHit += OnHit_Object;

                o.onDestroy += OnDestroy_Object;

            }

            if (o is CIron) o.onDestroy += OnDestroy_Object; 

            // - - -
            if (o is CTank)
            {
                
                // Setting events:
                (o as CTank).onCheckMoveForward += OnCheck_CanMoveForward;
                
                (o as CTank).onDestroy += OnDestroy_Tank;
                
                (o as CTank).onMove += OnMove_Object;
                
                (o as CTank).onShoot += OnShoot_Tank;

                (o as CTank).onInvinsible += this.OnActivate_ForceField;

            }

            if (o is CTankEnemy) (o as CTankEnemy).OnCreateBonus += OnBonusCreate;

            // 
            if (o is CTankPlayer)
            {
                (o as CTankPlayer).onDirectionChange += OnMove_Object;

                //(o as CTankPlayer).onDestroy += OnPlayerTankDestroy;
            }

            //
            if (o is CTankPortal)
            {
                
                (o as CTankPortal).onDestroy += OnDestroy_Portal;

            }          

            if (o is CExplotion)
            {

                (o as CExplotion).onDestroy += OnDestroy_Object;

                (o as CExplotion).onDestroy += OnDestroy_Explotion;

            }


            //
            if (o is CProjectile)
            {
                // Setting event handlers:
                (o as CProjectile).onDestroy += OnDestroy_Projectile;
                
                (o as CProjectile).onMove += OnMove_Projectile;
                
                (o as CProjectile).onCheckTargetHit += OnCheck_IsTargetHit;

            }

            //
            if (o is CGameOverMessage) o.onDestroy += this.OnComplete_Level;

            //
            if (o is CEagle) o.onDestroy += this.OnHit_HQ;

            if (o is CPoints) o.onDestroy += this.OnDestroy_Object;

            if (o is CForceField) o.onDestroy += this.OnDestroy_Object;

            if (o is CBonus) 
            {
                o.onDestroy += this.OnDestroy_Object;
                
                (o as CBonus).CheckBonusTake = this.OnCheck_IsBonusTaken;
                
                (o as CBonus).OnBonusTake += this.OnBonusTake;
                
                if (o is CBonusSpade)
                {
                    (o as CBonusSpade).OnBonusTake += this.OnBonusTake_Spade;

                    (o as CBonusSpade).OnBonusExpire += this.OnBonusExpire_Spade;                                                           
                }

                if (o is CBonusGranade)
                {
                    (o as CBonusGranade).OnBonusTake += this.OnBonusTake_Granade;
                }
                
                if (o is CBonusLife)
                {
                    (o as CBonusLife).OnBonusTake += this.OnBonusTake_Life;
                }

                if (o is CBonusClock)
                {
                    (o as CBonusClock).OnBonusTake += this.OnBonusTake_Clock;

                    (o as CBonusClock).OnBonusExpire += this.OnBonusExpire_Clock;
                }

                if (o is CBonusHelmet)
                {
                    (o as CBonusHelmet).OnBonusTake += this.OnBonusTake_Helmet;
                }

                if (o is CBonusStar)
                {
                    (o as CBonusStar).OnBonusTake += this.OnBonusTake_Star;
                }
            }

        }

        // - - - - - - - - - 
        // Key processor :
        // - - - - - - - - - 
        public void ReceiveKeyEvent(KeyMessageID m, KeyEventArgs e)
        {            
            KeyEventReceivers.ForEach((receiver) => receiver.ReceiveKeyEvent(m, e));
        }
        
        // - - - - - - - - - 
        // Level processor :
        // - - - - - - - - - 

        // Processing level objects:
        public void ProcessLevel()
        {

            ProcessableObjects.ForEach( p => p.ProcessObject() );
                     
            if (levelStarted)
            {
                // Adding enemy:
                if ((AllGameObjects.EnemiesCount < 4) && (AllGameObjects.TankPortalsCount < 1) && (this.EnemyTanksLeft > 0))
                {
                    var bonus = enemyTanksLeft % 10 == 0 ? true : false;
                    
                    CreateTankPortal(CObjectCreator.CreateTank(EnemyTanks[enemyTanksLeft - 1],bonus));

                    EnemyTanksLeft--;

                }

                if ((EnemyTanksLeft == 0) && (AllGameObjects.EnemiesCount == 0))
                {
                    if (timeoutToNextLevel-- == 0) OnComplete_Level(this, new EventArgs());
                }
            }

        }
                
        // - - - - - - - - - 
        // Drawing:
        // - - - - - - - - - 
        // Level painter:
        public void DrawLevel(Graphics g)
        {
            // Drawing all game objects: 
            DrawMap(g);

            // If level started then show statistics:
            if (levelStarted) DrawPlayerStat(g);

            // else show curtain:
            else DrawLevelCurtain(g);           

        }

        public void DrawLevelCurtain(Graphics g)
        {
            if (levelCurtain != null) levelCurtain.DrawElement(g);
        }

        public void DrawPlayerStat(Graphics g)
        {
            // Drawing level number:
            g.DrawString(levelNumber.ToString(), font, Brushes.Black, pointf);

            // Drawing player stats:
            if (this.player1 != null)
            {
                g.DrawString("IP", font2, Brushes.Black, pointf_Player1_IPCaption);

                g.DrawString(player1.Life.ToString(), font2, Brushes.Black, pointf_Player1_LifeCaption);
            }

            if (this.player2 != null)
            {
                g.DrawString("IIP", font2, Brushes.Black, pointf_Player2_IIPCaption);
                
                g.DrawString(player2.Life.ToString(), font2, Brushes.Black, pointf_Player2_LifeCaption);
            }
        }

        // Saves a map to png file:
        public void SaveMapToBMP(int g)
        {
            Bitmap map = new Bitmap(56, 56);
            for (int i = 0; i < 56; i++)
                for (int j = 0; j < 56; j++)
                    if (mapMain[i, j] != null) map.SetPixel(i, j, Color.Red); else map.SetPixel(i, j, Color.Blue);
            map.Save(string.Format("d:\\map\\map{0}.png", g));
        }

        // - - - - - - - - - 
        // Constructors :
        // - - - - - - - - -
        void LoadMap(object sender, EventArgs e)
        {            
            // Loading Map:
            CreateMap();

            // Setting HeadQuaters:
            CreateHQ();

            // Adding background:
            AddObject(new CGameObject(Properties.Resources.background, 0, 0, 494, 452) { Transparent = true });
        }

        void InitializeLevel(object sender, EventArgs e)
        {   
            // Adding Players:
            Players.ForEach(player =>
            {
                if (player.TankID == Tanks.Player1) player1 = player; 
                
                else 
                    
                    if (player.TankID == Tanks.Player2) player2 = player; 
 
                player.Reset(false);

                player.ResetEnemyHits();

                CreateTankPortal(player); 
            }
            );                     

            // Adding statistics:
            CreateStatisticsBlock(); 
        }
 
        // Constructor:
        public CPlayLevel(int levelNo, List<CTankPlayer> players, int[] map = null)
        {
            // setting level number:
            this.LevelNumber = levelNo;

            // Setting construction map:
            this.constructionMap = map;

            // Setting players:
            this.Players = players;

            // Setting game status:
            this.GameStatus = GameStatuses.Playing;           

            // Dont start level until curtain is completed:
            this.LevelStarted = false;

            // Creating level curtain:
            CreateLevelCurtain();
            
        }

    }
}
