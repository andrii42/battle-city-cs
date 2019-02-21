using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Tanks
{
    public partial class ViewControl : UserControl
    {
        private CModel model;

        // Timer to run game:
        private System.Windows.Forms.Timer drawTimer = new System.Windows.Forms.Timer();

        // OnPaint handler
        protected override void OnPaint(PaintEventArgs e)
        {
            // Redirecting event to model:
            model.DrawScene(e);

        }

        // key down handler:
        protected override void OnKeyDown(KeyEventArgs e)
        {
            //e.KeyData.
            // Redirecting event to model:
            model.ProcessKeys(KeyMessageID.KeyDown,e);
        }

        // key up handler:
        protected override void OnKeyUp(KeyEventArgs e)
        {
            // Redirecting event to model:            
            model.ProcessKeys(KeyMessageID.KeyUp,e);
        }      
        
        // Constructor:
        public ViewControl(CModel model)
        {
            // Setting Model field:
            this.model = model;
                              
            // Setting timer to Invalidate view:
            drawTimer.Interval = 5;
            drawTimer.Tick += (object sender, EventArgs e) => this.Invalidate();
            drawTimer.Start();

            // Initialising:
            InitializeComponent();
        }
    }
}
