using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestSauvegarde
{
    [Serializable]
    public class Class1
    {
        public int nbTourMax { get; set; }

        public List<Class2> tours { get; set; }

        public Class3 carte { get; set; }

        public Class1(int nbTourMax, List<Class2> tours, Class3 carte)
        {
            this.nbTourMax = nbTourMax;
            this.tours = tours;
            this.carte = carte;
        }

        public Class1(int nbTourMax)
        {
            this.nbTourMax = nbTourMax;
            this.tours = new List<Class2>();
            this.carte = new Class3();
        }

        public void ajouterTour(String txt)
        {
            tours.Add(new Class2(txt));
        }
    }
}
