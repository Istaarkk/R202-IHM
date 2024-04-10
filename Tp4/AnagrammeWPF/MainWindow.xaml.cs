using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Runtime.InteropServices;
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

namespace AnagrammeWPF
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private string motATrouver;
        private const int ESSAIS_MAX = 5;
        private int essais = ESSAIS_MAX;
        private int cptessaie = 0;
        private int jeux = 0;
        private string res = " ";
        private string reponse = "";

        private string[] mots;
        //ajouter d'autres propriétés ici si besoin
        //
        //
        //
        private String[] tabMots;

        public MainWindow()
        {
            
            InitializeComponent();
            initialisation();


        }
        private void initialisation()
        {
           lst_partiejouer.Items.Clear();
            nbessai.Content = ESSAIS_MAX;
            tabMots = new string[] { "ORDINATEUR", "PROGRAMMATION", "DEVELOPPEUR", "INTERFACE", "APPLICATION" };
            Random rand = new Random();
            int index = rand.Next(tabMots.Length);
            motATrouver = tabMots[index];
            string motMelange = melanger(motATrouver);
            lblAnagramme.Content = motMelange;
            nbessai.Content = ESSAIS_MAX;

        }

        private string melanger(string chaine)
        {
            Random rand = new Random();
            char[] chars = chaine.ToArray();
            for (int i = 0; i < chars.Length; i++)
            {
                int j = rand.Next(chars.Length);
                char temp = chars[i];
                chars[i] = chars[j];
                chars[j] = temp;
            }
            return new string(chars);
        }
     
        
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            initialisation();
        }

        private void TextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            
        }

        private void nouvellePartie()
        {
            nbessai.Content = ESSAIS_MAX;
            lst_partiejouer.Items.Add("¨Partie : " + jeux + " " + motATrouver + "  Essai restant" + essais + " Résultat :"+res );
            essais = ESSAIS_MAX;
            motHistorique.Items.Clear();
            ++jeux;
            
            Random rand = new Random();
            int index = rand.Next(tabMots.Length);
            motATrouver = tabMots[index];
            string motMelange = melanger(motATrouver);
            lblAnagramme.Content = motMelange;
            cptessaie = 0;



        }


        private void Button_Valider(object sender, RoutedEventArgs e)
        {
            reponse = txtReponse.Text.ToUpper();
            txtReponse.Text = "";
            if (reponse == motATrouver)
            {
                res = " Gagnée ";
                motCorrect(); 
            }
            else
            {
                res = " Perdu ";
                motIncorrect();
               
            }
        }

        private void motCorrect()
        {
            MessageBox.Show("Félicitation le mot a trouver été bien : " + motATrouver);
            if (MessageBox.Show("Voulez-vous rejouer ", "nouvelle partie", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
            {
                nouvellePartie();
            }
            else
            {
                this.Close();
            }
        }

        private void motIncorrect()
        {
            
            essais--;
            nbessai.Content = essais;
            cptessaie++;
            motHistorique.Items.Add("Essaie : " + cptessaie +" "+ reponse);
            if (essais== 0)
            {
                MessageBox.Show("Désoler, vous avez épuisé tous vos essais. Le mot à trouver était " + motATrouver + " Partie perdue");
                if (MessageBox.Show("Voulez-vous rejouer", "Nouvelle Partie", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
                {
                    nouvellePartie();
                }
                else
                {
                    this.Close();
                }
            }
        }

        private void Button_Quit_Click(object sender, RoutedEventArgs e)
        {
            if (MessageBox.Show("Voulez-vous Fermer la fenetre ", "", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
            {
                this.Close();
            }
           
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if (MessageBox.Show("Voulez-vous Rejouez ", "", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
            {
                nouvellePartie();
            }
           
        }




        //ajouter vos autres méthodes ici
        //
        //
        //

    }
}
