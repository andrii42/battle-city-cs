using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using System.Windows.Forms;

namespace Tanks
{
    public class CConstructionCursor : CGameObject, IProcessable
    {
        // *********************************
        // Private and protected members:
        // *********************************
        private int blinktime = 15;

        private float minXY;

        private float maxXY;

        // *********************************
        // Methods:
        // *********************************

        public void ProcessObject()
        {
            if (blinktime-- == 0)
            {
                this.Visible = !this.Visible;

                blinktime = 15;
            }
        }

        public void Move(Directions d)
        {
            switch (d)
            {
                case Directions.Up: this.Y = this.Y > minXY ? this.Y - 32 : this.Y; break;

                case Directions.Down: this.Y = this.Y < maxXY ? this.Y + 32 : this.Y; break;
                
                case Directions.Left: this.X = this.X > minXY ? this.X - 32 : this.X; break;
                
                case Directions.Right: this.X = this.X < maxXY ? this.X + 32 : this.X; break;
            }
        }

        public CConstructionCursor(float x, float y,  float minimumXY, float maximumXY)
        {
            this.X = x;

            this.Y = y;

            this.minXY = minimumXY;

            this.maxXY = maximumXY;

            this.Width = 32;

            this.Height = 32;

            this.Image = Properties.Resources.Player1tank_up1;

            this.DrawPriority = 10;
        }
    }


    public class CConstructionLevel : CLevel, ILevel
    {
        // *********************************
        // Private and protected members:
        // *********************************

        private int[] mapBuffer = new int[676];

        private bool changeBlock = true;

        private int currentBlockIndex = 0;
        
        private CGameObject[,] tileMap = new CGameObject[26, 26];

        private CConstructionCursor cursor;             

        private Blocks[] blocks = {Blocks.WallFull, Blocks.WallUp, Blocks.WallDown, Blocks.WallLeft,Blocks.WallRight, 
                           Blocks.IronFull, Blocks.IronUp, Blocks.IronDown, Blocks.IronLeft, Blocks.IronRight,Blocks.Garden, 
                           Blocks.Water, Blocks.Ice, Blocks.Empty};

        // *********************************
        // Properties:
        // *********************************

        int CurrentBlockIndex
        {
            get { return currentBlockIndex; }
            set {
                 if (value < blocks.Count()) currentBlockIndex = value;  else currentBlockIndex = 0;
                }
        }

        Blocks CurrentBlock
        {
            get { return blocks[CurrentBlockIndex]; }
        }      

        // *********************************
        // Events:
        // *********************************
        public event EventHandler OnLevelExit;

        // *********************************
        // Methods:
        // *********************************

        private void SetBlock(Blocks block, float x, float y)
        {
            int blockmap = 0;

            StaticObjects object_id = StaticObjects.Brick;

            int mapX = (int)(x / 16) - 1;

            int mapY = (int)(y / 16) - 1; ;

            switch (block)
            {
                case Blocks.WallFull: blockmap = 0x0F; object_id = StaticObjects.Brick; break;

                case Blocks.WallUp: blockmap = 0x0C; object_id = StaticObjects.Brick; break;

                case Blocks.WallDown: blockmap = 0x03; object_id = StaticObjects.Brick; break;

                case Blocks.WallLeft: blockmap = 0x0A; object_id = StaticObjects.Brick; break;

                case Blocks.WallRight: blockmap = 0x05; object_id = StaticObjects.Brick; break;

                case Blocks.IronFull: blockmap = 0x0F; object_id = StaticObjects.Iron; break;

                case Blocks.IronUp: blockmap = 0x0C; object_id = StaticObjects.Iron; break;

                case Blocks.IronDown: blockmap = 0x03; object_id = StaticObjects.Iron; break;

                case Blocks.IronLeft: blockmap = 0x0A; object_id = StaticObjects.Iron; break;

                case Blocks.IronRight: blockmap = 0x05; object_id = StaticObjects.Iron; break;

                case Blocks.Garden: blockmap = 0x0F; object_id = StaticObjects.Garden; break;

                case Blocks.Ice: blockmap = 0x0F; object_id = StaticObjects.Ice; break;

                case Blocks.Water: blockmap = 0x0F; object_id = StaticObjects.Water; break;

                default: blockmap = 0x00; object_id = StaticObjects.Empty; break;
            }

            int comparator = 0x08;

            for (int i = 0; i < 2; i++)
                for (int j = 0; j < 2; j++)
                {
                    if (tileMap[mapX + j, mapY + i] != null) RemoveObject(tileMap[mapX + j, mapY + i]);

                    if ((comparator & blockmap) != 0)
                    {
                        CGameObject new_object = (CGameObject)CObjectCreator.CreateStaticObject(object_id, (float)((x + (j * 16))), (float)((y + (i * 16))));

                        AddObject(new_object);

                        tileMap[mapX + j, mapY + i] = new_object;

                        mapBuffer[mapY + j + (mapX + i) * 26] = (int)object_id;
                    }

                    else tileMap[mapX + j, mapY + i] = null;

                    comparator >>= 1;
                }
        }

        private void InitializeLevel()
        {
            AddObject(new CGameObject(Properties.Resources.background, 0, 0, 494, 452) { Transparent = true });

            cursor = new CConstructionCursor(16, 16, this.minimumXY, this.maximumXY);

            AddObject(cursor);

            ProcessableObjects.Add(cursor);
        }

        protected override void AddObject(CGameObject o)
        {
            AllGameObjects.Add(o);

            //if (o is IProcessable) ProcessableObjects.Add((IProcessable)o);
        }

        protected override void RemoveObject(CGameObject o)
        {
            AllGameObjects.Remove(o);

            //if (o is IProcessable) ProcessableObjects.Remove((IProcessable)o);
        }

        public void DrawLevel(Graphics g)
        {
            DrawMap(g);
        }

        public void ProcessLevel()
        {
            ProcessableObjects.ForEach(p => p.ProcessObject());
        }                          

        public void ReceiveKeyEvent(KeyMessageID m, KeyEventArgs e)
        {
            if (m == KeyMessageID.KeyDown)
            {
                switch (e.KeyCode)
                {
                    case Keys.Home: cursor.Move(Directions.Up); this.changeBlock = false; break;

                    case Keys.End: cursor.Move(Directions.Down); this.changeBlock = false; break;

                    case Keys.Delete: cursor.Move(Directions.Left); this.changeBlock = false; break;

                    case Keys.PageDown: cursor.Move(Directions.Right); this.changeBlock = false; break;

                    case Keys.Space:

                        if (this.changeBlock) CurrentBlockIndex++;

                        SetBlock(CurrentBlock, cursor.X, cursor.Y);

                        this.changeBlock = true;

                        break;

                    case Keys.Escape:
                        {
                            SaveMapToBMP(1);

                            OnLevelExit(this, new ConstructionEventArgs(this.mapBuffer)); break;
                        }
                }

                //
            }
        }

        // Saves a map to png file:
        public void SaveMapToBMP(int g)
        {
            Bitmap map = new Bitmap(26, 26);
            for (int i = 0; i < 26; i++)
                for (int j = 0; j < 26; j++)
                    if (mapBuffer[i*26+j] != 0) map.SetPixel(i, j, Color.Red); else map.SetPixel(i, j, Color.Blue);
            map.Save(string.Format("d:\\map\\map{0}.png", g));
        }      

        public CConstructionLevel()
        {            
            InitializeLevel();
        }
    }
}
