using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Drawing;

namespace Tanks
{
    public interface ILevel : IKeyEventReceiver
    {
        // Draws a level:
        void DrawLevel(Graphics g);

        // Processes a level:
        void ProcessLevel();                
    }
}
