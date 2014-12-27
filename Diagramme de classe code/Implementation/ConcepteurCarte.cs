using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PeopleWar
{
    public class ConcepteurCarte
    {
        public static ConcepteurCarte INSTANCE = new ConcepteurCarte();

        public StrategieCarte carte { get; set; }

        private ConcepteurCarte() { }
        /**
         * Define the type of the builder
         * @param Carte carte
         * @return void
         */
        public void definirCarte(StrategieCarte carte)
        {
            this.carte = carte;
        }

        /**
         * Create the boxes of the map
         * @return void
         */
        public void creerCarte()
        {
            carte.creerCarte();
        }
    }
}
