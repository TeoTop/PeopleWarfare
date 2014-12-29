using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using PeopleWar;
using System.Windows.Controls.Primitives;

namespace WarFareWPF
{
    /// <summary>
    /// Logique d'interaction pour GameWindow.xaml
    /// </summary>
    public partial class GameWindow : Window
    {
        public PartieImp partie { get; set; }
        public MapView map { get; set; }
        public PlayerView J1 { get; set; }

        public PlayerView J2 { get; set; }

        public ListActionView actions { get; set; }

        public bool isUnitSelected { get; set; }

        public int CurrentPlayer { get; set; }

        public int nbUnitesj1 { 
            get { return partie.j1.peuple.getNbUnite(); }
        }

        public int nbUnitesj2 { 
            get { return partie.j2.peuple.getNbUnite(); } 
        }

        public SelectUnit su { get; set; }

        public GameWindow(PartieImp partie)
        {
            this.partie = partie;
            this.map = new MapView(partie.carte);
            J1 = new PlayerView(partie.j1, new Point(0, 0));
            J2 = new PlayerView(partie.j2, new Point(Math.Sqrt(partie.carte.cases.Count()) - 1, Math.Sqrt(partie.carte.cases.Count()) - 1));
            actions = new ListActionView(this.partie.tours, "Chemin/Vers/Dossier/AutoSave");
            CurrentPlayer = 0;
            // definir les cases où les unités seront mises depuis le wrapper
            map.cases[partie.carte.getKey((int)J1.InitialPosition.X, (int)J1.InitialPosition.Y)].unitsJ1 = J1.peuple.units;
            map.cases[partie.carte.getKey((int)J2.InitialPosition.X, (int)J2.InitialPosition.Y)].unitsJ2 = J2.peuple.units;
            isUnitSelected = false;
            InitializeComponent();
        }

        public PlayerView getCurrentPlayer()
        {
            return CurrentPlayer == 0 ? J1 : J2;
        }

        public PlayerView getOtherPlayer()
        {
            return CurrentPlayer == 0 ? J2 : J1;
        }

        public void switchPlayer()
        {
            CurrentPlayer = CurrentPlayer == 0 ? 1 : 0;
        }

        private void Grid_MouseWheel(object sender, MouseWheelEventArgs e)
        {
            //ctrl
            bool handle = (Keyboard.Modifiers & ModifierKeys.Control) > 0;
            if (handle)
            {
                // zoom / dezoom without scroll if ctrl is handled
                if (e.Delta > 0)
                {
                    this.map.Zoom += 5;
                }
                else
                {
                    this.map.Zoom -= 5;
                }
                e.Handled = true;
            }
        }

        private void SelectUnit(object sender, MouseButtonEventArgs e)
        {
            Grid g = sender as Grid;
            int uid = System.Convert.ToInt32(g.Tag);
            BoxView SelectedBoxForUnit = map.SelectedBoxForUnit;
            BoxView box = map.cases[uid];
            if (isUnitSelected && SelectedBoxForUnit != box)
            {
                // deplacer unité
                if (SelectedBoxForUnit != null)
                {
                    UnitView unit = SelectedBoxForUnit.SelectedUnit;
                    if (unit != null)
                    {
                        if (unit.unit.verifierDeplacement(uid, SelectedBoxForUnit.Uid, (int)Math.Sqrt(map.carte.cases.Count), getCurrentPlayer().peuple.peuple.getType(), map.carte, getOtherPlayer().peuple.peuple))
                        {
                            unit.unit.seDeplacer(uid);
                            box.addUnit(unit, CurrentPlayer);
                            SelectedBoxForUnit.removeUnit(unit, CurrentPlayer);
                            isUnitSelected = false;
                        }
                    }
                }
            }
            else
            {
                // selectionner unité
                List<UnitView> units = box.getUnits(CurrentPlayer);
                if (su != null) su.Close();
                su = new SelectUnit(box, units, this);
                su.Show();
                su.Activate();
            }
        }

        private void btnSauvgarder(object sender, RoutedEventArgs e)
        {
            try
            {
                SaveFileDialog saveFileDialog = new SaveFileDialog();
                saveFileDialog.FileName = "peopleWarfare_save";     //nom par default du fichier
                saveFileDialog.Filter = "PeopleWarfare (*.ppw)|*.ppw|Tous les fichiers (*.*)|*.*";  //liste des extension
                saveFileDialog.FilterIndex = 1;     // met ppw comme extension par default, 2 pour l'autre
                saveFileDialog.Title = "Sauvegarde de partie de PeopleWarfare";  //titre de la fenetre
                saveFileDialog.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.Desktop); // trouver le dossier bureau

                if (saveFileDialog.ShowDialog() == true)
                {
                    IFormatter formatter = new BinaryFormatter();
                    Stream stream = new FileStream(saveFileDialog.FileName, FileMode.OpenOrCreate, FileAccess.Write);
                    formatter.Serialize(stream, partie);
                    stream.Close();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erreur lors de la sauvegarde: " + ex.Message);
            }
        }

        private void btnQuitter(object sender, RoutedEventArgs e)
        {
            if (partie.sauvegarde)
            {
                //quitter
            }
            else
            {
                //sauvegarder avant
            }
        }
    }
}
