using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Tanks
{
    /// <summary>
    /// This interface must be implemented in every game mode
    /// </summary>
    public interface IGameMode : IKeyProcessor
    {   
        // Scene painter:
        void DrawGameMode(PaintEventArgs e);

        //
        void InitializeMode();

        // Game mode runner:
        void RunGameMode();

    }
}
