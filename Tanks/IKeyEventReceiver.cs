using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Tanks
{
    public interface IKeyEventReceiver
    {
        // Receives Keys event:
        void ReceiveKeyEvent(KeyMessageID m, KeyEventArgs e);

    }
}
