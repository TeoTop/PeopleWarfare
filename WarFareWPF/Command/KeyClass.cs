using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace WarFareWPF.Command
{
    public class KeyClass
    {
        public Key key { get; set; }
        public bool handle { get; set; }

        public KeyClass(Key key, bool handle = false)
        {
            this.key = key;
            this.handle = handle;
        }
    }
}
