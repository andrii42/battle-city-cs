using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Tanks
{
    public class ConstructionEventArgs : EventArgs
    {
        // *********************************
        // Private and protected members:
        // *********************************

        private int[] mapBuffer;

        // *********************************
        // Properties:
        // *********************************
        public int[] MapBuffer
        {
            get { return mapBuffer; }
            set { mapBuffer = value; }
        }

        // *********************************
        // Methods:
        // *********************************

        public ConstructionEventArgs(int[] map)
        {
            this.mapBuffer = map;
        }
    }

    public partial class CModel
    {
        // *********************************
        // Methods:
        // *********************************
        void OnGameOver(object sender, EventArgs e)
        {
            // Saving map if we have exited the construction mode:
            if (e is ConstructionEventArgs) this.mapBuffer = (e as ConstructionEventArgs).MapBuffer;

            var playingMode = sender as CPlayingMode;

            if (playingMode != null)
            {

                this.playersPoints = new List<int>();

                foreach (CTankPlayer player in playingMode.Players)
                {
                    playersPoints.Add(player.Points);

                    if ((player.TankID == Tanks.Player1) && (player.Points > this.hiscorePlayer1)) this.hiscorePlayer1 = player.Points;

                    if ((player.TankID == Tanks.Player2) && (player.Points > this.hiscorePlayer2)) this.hiscorePlayer2 = player.Points;

                }

            }

            // Setting game menu mode:
            SetGameMode(GameModes.Menu);            
        }

        void OnSelectGameMode(object sender, EventArgs e)
        {
            CMenuMode mainmenu = (sender as CMenuMode);

            if (mainmenu != null) SetGameMode(mainmenu.SelectedGameMode);
        }

    }
}
