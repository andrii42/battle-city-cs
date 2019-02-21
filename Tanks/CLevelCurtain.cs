using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using System.Windows.Forms;

namespace Tanks
{
    public class CLevelCurtain : IProcessable, IKeyEventReceiver
    {
        const int maxCounter = 224;

        // *********************************
        // Private and protected members:
        // *********************************

        private bool start;

        private int levelNo;

        private int drawingCounter;

        // *********************************
        // Events:
        // *********************************
        public event EventHandler OnHalfCompleted;

        public event EventHandler OnCompleted;

        // *********************************
        // Methods:
        // *********************************

        public void ProcessObject()
        {
            // Processing it:
            if (start)
            {
                drawingCounter = drawingCounter <= maxCounter ?  drawingCounter + 8 : drawingCounter;                
            }
            else
            {
                drawingCounter -= 8;

                if ((drawingCounter < 1) && (OnCompleted != null)) OnCompleted(this, new EventArgs());
            }
        }

        public void DrawElement(Graphics g)
        {

            g.DrawImage(Properties.Resources.levelCurtainFull, 0, 0, 512, drawingCounter);

            g.DrawImage(Properties.Resources.levelCurtainFull, 0, 448 - drawingCounter, 512, drawingCounter);

            if (start) g.DrawString("STAGE " + levelNo.ToString(), new Font(FontFamily.GenericMonospace, 16, FontStyle.Bold), Brushes.Black, new PointF(200, 216));
        }

        public CLevelCurtain(int ln)
        {
            this.start = true;

            this.levelNo = ln;
        }

        public void ReceiveKeyEvent(KeyMessageID m, KeyEventArgs e)
        {
            if ((drawingCounter >= maxCounter) && (start))
            {
                start = false;

                if (OnHalfCompleted != null) OnHalfCompleted(this,new EventArgs());
            }
        }
    }
}
