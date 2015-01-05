using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestSauvegarde
{
    [Serializable]
    public class Class3
    {
        public List<int> num_case { get; set; }

        public Class3(List<int> num_case)
        {
            this.num_case = num_case;
        }

        public Class3()
        {
            this.num_case = new List<int>();
            for (int i = 0; i < 10; i++) num_case.Add(i);
        }
    }
}
