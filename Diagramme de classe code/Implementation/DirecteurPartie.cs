﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using Wrapper;
namespace PeopleWar
{
    public class DirecteurPartie
    {
        /**
         * DirecteurPartie Constructor 
         */
        public DirecteurPartie() { }

        /**
         * Game's builder
         * @var MonteurPartie MonteurPartie
         */
        public MonteurPartie MonteurPartie { get; set; }
        /**
         * Create a game
         * @param EnumCarte carte
         * @param EnumPeuple p1
         * @param EnumPeuple p2
         * @return Partie
        */
        public PartieImp creerPartie(String nom1, String nom2, EnumCarte carte, EnumPeuple p1, EnumPeuple p2)
        {
            StrategieCarte c = MonteurPartie.creerCarte(carte);
            int nbUnite = 0;
            int nbTourMax = 0;


            switch (carte)
            {
                case EnumCarte.DEMO:
                    nbUnite = 4;
                    nbTourMax = 5;
                    break;
                case EnumCarte.PETITE:
                    nbUnite = 6;
                    nbTourMax = 20;
                    break;
                case EnumCarte.NORMALE:
                    nbUnite = 8;
                    nbTourMax = 30;
                    break;
            }

            // give position to units
            WrapperAlgos w = new WrapperAlgos();
            int[] pos = w.placer_joueurs(c.nbCase);
            JoueurImp j1 = MonteurPartie.creerJoueur(nom1, p1, nbUnite, pos[0]);
            JoueurImp j2 = MonteurPartie.creerJoueur(nom2, p2, nbUnite, pos[1]);
            return new PartieImp(c, j1, j2, nbTourMax);
        }


        /**
         * Load a game from binary file
         * @param String file
         * @return Partie
         */
        public PartieImp chargerPartie(String file)
        {
            PartieImp partie = new PartieImp();

            try
            {
                IFormatter formatter = new BinaryFormatter();
                Stream stream = new FileStream(file, FileMode.Open, FileAccess.Read);
                partie = (PartieImp)formatter.Deserialize(stream);
                stream.Close();

            }
            catch (Exception)
            {

            }

            return partie;
        }

        /**
         * Define a builder for the game
         * @param MonteurPartie monteurPartie
         * @return void
         */
        public void definirMonteur(MonteurPartie monteurPartie)
        {
            MonteurPartie = monteurPartie;
        }
    }

}

