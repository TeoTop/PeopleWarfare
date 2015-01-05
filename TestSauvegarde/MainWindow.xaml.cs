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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace TestSauvegarde
{
    /// <summary>
    /// Logique d'interaction pour MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public Class1 c1 { get; set; }

        public MainWindow()
        {
            c1 = new Class1(5);
            InitializeComponent();
        }

        private void btnSaveFile_Clic(object sender, RoutedEventArgs e)
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
                    formatter.Serialize(stream, c1);
                    stream.Close();                   
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erreur lors de la sauvegarde: " + ex.Message);
            }
        }

        private void btnAjouter(object sender, RoutedEventArgs e)
        {
            c1.ajouterTour("Tour " + c1.tours.Count + "\n");
            txtEditor.AppendText(c1.tours.Last().tour);
        }

        private void btnLoad(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openFileDialog1 = new OpenFileDialog();

            openFileDialog1.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
            openFileDialog1.Filter = "PeopleWarfare (*.ppw)|*.ppw|Tous les fichiers (*.*)|*.*";
            openFileDialog1.FilterIndex = 1;
            openFileDialog1.RestoreDirectory = true;
            openFileDialog1.Title = "Chargement d'une sauvegarde PeopleWarfare";
            
            if (openFileDialog1.ShowDialog() == true)
            {
                try
                {
                    IFormatter formatter = new BinaryFormatter();
                    Stream stream = new FileStream(openFileDialog1.FileName, FileMode.Open, FileAccess.Read);
                    c1 = (Class1)formatter.Deserialize(stream);
                    stream.Close();

                    chargerDock();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error: Could not read file from disk. Original error: " + ex.Message);
                }
            }
        }

        public void chargerDock()
        {
            txtEditor.Clear();
            foreach (Class2 t in c1.tours)
            {  
                txtEditor.AppendText(t.tour);
            }
        }


    }
}
