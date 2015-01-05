using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestSauvegarde
{
    [Serializable]
    public class Class2
    {
        public String tour { get; set; }

        public Class2(String tour)
        {
            this.tour = tour;
        }
    }
}
