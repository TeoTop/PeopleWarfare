using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WarFareWPF.Command
{
    public class KeyEquality : IEqualityComparer<KeyClass>
    {
        public bool Equals(KeyClass x, KeyClass y)
        {
            return (x.handle == y.handle && x.key == y.key);
        }

        public int GetHashCode(KeyClass obj)
        {
            return obj.key.GetHashCode();
        }
    }
}
