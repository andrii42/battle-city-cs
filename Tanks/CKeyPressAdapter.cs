using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Tanks
{

    /// <summary>
    /// Adapter for key press processing:
    /// </summary>
    public class CKeyPressAdapter : IKeyProcessor
    {
        // *********************************
        // Private and protected members:
        // *********************************        
        private CTankPlayer Receiver;

        private Keys[] Keys;

        // Buffer for Player1 move keys:
        private int keysBuffer;

        // *********************************
        // Properties:
        // *********************************

        public int KeysBuffer
        {
            get { return keysBuffer; }

            set
            {

                Receiver.AllowedToMove = (value == 0) ? false : true;

                keysBuffer = value;

            }
        }

        // *********************************
        // Methods:
        // *********************************

        public void ProcessKeys(KeyMessageID m, KeyEventArgs e)
        {            
            KeyIndex keyindex = (KeyIndex)GetKeyIndex(e.KeyCode);

            switch (m)
            {
                case KeyMessageID.KeyDown:
                    // If KeyDown event occured:
                    if (!Receiver.Destroyed && Receiver.Enabled)
                    {
                        switch ((KeyIndex)keyindex)
                        {
                            case KeyIndex.Up: Receiver.Direction = Directions.Up; KeysBuffer |= 0x01; break;

                            case KeyIndex.Down: Receiver.Direction = Directions.Down; KeysBuffer |= 0x02; break;

                            case KeyIndex.Left: Receiver.Direction = Directions.Left; KeysBuffer |= 0x04; break;

                            case KeyIndex.Right: Receiver.Direction = Directions.Right; KeysBuffer |= 0x08; break;

                            case KeyIndex.Shoot: Receiver.Shoot(); break;

                            default: break;
                        }
                    } break;

                case KeyMessageID.KeyUp :
                    // If KeyUp event occured:
                    if (!Receiver.Destroyed)
                    {
                        switch ((KeyIndex)keyindex)
                        {
                            case KeyIndex.Up: KeysBuffer &= 0x0e; break;

                            case KeyIndex.Down: KeysBuffer &= 0x0d; break;

                            case KeyIndex.Left: KeysBuffer &= 0x0b; break;

                            case KeyIndex.Right: KeysBuffer &= 0x07; break;

                            //case KeyIndex.Shoot: KeysBuffer &= 0x0e; break;

                            default: break;
                        }

                    }
                    break;
            }
        }

        private int GetKeyIndex(Keys key)
        {

            for (int i = 0; i < Keys.Count(); i++) if (key == Keys[i]) return i;

            return -1;

        }

        public CKeyPressAdapter(Keys[] keys, CTankPlayer receiver)
        {

            Keys = keys;

            Receiver = receiver;

        }
    }
}
