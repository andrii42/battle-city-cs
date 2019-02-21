using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Tanks
{
    public interface IKeyProcessor
    {
        void ProcessKeys(KeyMessageID m, KeyEventArgs e);
    }
}
