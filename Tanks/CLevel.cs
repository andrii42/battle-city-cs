using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;

namespace Tanks
{
    public abstract class CLevel
    {
        // *********************************
        // Private and protected members:
        // *********************************
        // size of the tile:
        protected const int tileSize = 16;
        // size of the cell on map:
        protected const int cellSize = 8;

        protected readonly int minimumXY = 16;
        //
        protected readonly int maximumXY = 400;
        //
        #warning Font provider shoud be made!

        protected Font font = new Font(FontFamily.GenericMonospace, 14, FontStyle.Bold);

        protected Font font2 = new Font(FontFamily.GenericMonospace, 16, FontStyle.Bold);

        protected Font font3 = new Font(FontFamily.GenericMonospace, 18, FontStyle.Bold);

        protected PointF pointf = new PointF(460, 400);

        protected PointF pointf_Player1_IPCaption = new PointF(447, 280);

        protected PointF pointf_Player1_LifeCaption = new PointF(462, 297);

        protected PointF pointf_Player2_IIPCaption = new PointF(447, 320);

        protected PointF pointf_Player2_LifeCaption = new PointF(462, 337);

        // All objects container:
        protected CGameObjectContainer<IGameObject> AllGameObjects = new CGameObjectContainer<IGameObject>();

        // List of processable objects:
        protected List<IProcessable> ProcessableObjects = new List<IProcessable>();

        // *********************************
        // Methods:
        // *********************************

        public void DrawMap(Graphics g)
        {
            // Drawing all objects:
            foreach (IGameObject go in AllGameObjects.GameObjectList) if (go.Visible) g.DrawImage(go.Image, (int)go.X, (int)go.Y, (int)go.Width, (int)go.Height);
        }

        protected abstract void AddObject(CGameObject o);

        protected abstract void RemoveObject(CGameObject o);
        
    }
}
