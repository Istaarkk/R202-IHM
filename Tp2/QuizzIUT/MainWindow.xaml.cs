using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.Xml;
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
using System.Media;

namespace QuizzIUT
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {

        private int count = -1;
        private int good = 0;
        private int bad = 0;
        private string[] questions = { "Quelle est le nombre au quelle je pense", "Quelle est la capitale de la France", "Quel est l'élément le plus abondant dans l'atmosphère?", "que veut dire SCADA", "Quelle est la planète la plus proche du soleil?", "Quel protocole est utilisé pour le transfert de fichiers", "Quel terme décrit l'identification unique d'un périphérique sur un réseau?", "Quel est le symbole chimique de l'or" };
        private string[] reponses = { "42", "Paris", "Azote", "Supervisory Control And Data Acquisition", "Mercure", "FTP", "Adresse MAC", "Au" };

        public object MessageBoxButtons { get; private set; }

        public MainWindow()
        {
            InitializeComponent();
        }


        private void NextQuestion()
        {
            count++;
            if (count == questions.Length) { count = 0; }
            LBLQuestion.Content = questions[count];
        }


        private void BTNValider_Click(object sender, RoutedEventArgs e)
        {
            if (TBXReponse.Text == reponses[count])
            {
                MessageBox.Show("Good Job");
                ++good;
                sonvrai();
            }

            else
            {
                MessageBox.Show("Task failed Successfully answer was : " + reponses[count]);
                bad++;
                sonfaux();

            }
            LBLBonnesReponsesValeur.Content = good;
            LBLMauvaisesReponsesValeur.Content = bad;
            ++count;
            NextQuestion();
        }

        private void WNDFenetrePrincipale1_Loaded(object sender, RoutedEventArgs e)
        {
            NextQuestion();
        }

        private void SDRModeNuit_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            if (SDRModeNuit.Value == 1)
            {
                var bc = new BrushConverter();
                WNDFenetrePrincipale.Background = (Brush)bc.ConvertFrom("#000000");
                LBLBonnesReponses.Foreground = (Brush)bc.ConvertFrom("#FFFFFF");
                LBLBonnesReponsesValeur.Foreground = (Brush)bc.ConvertFrom("#FFFFFF");
                LBLMauvaisesReponses.Foreground = (Brush)bc.ConvertFrom("#FFFFFF");
                LBLMauvaisesReponsesValeur.Foreground = (Brush)bc.ConvertFrom("#FFFFFF");
                LBLQuestion.Foreground = (Brush)bc.ConvertFrom("#FFFFFF");
                LBLTitre.Foreground = (Brush)bc.ConvertFrom("#FFFFFF");
                SDRModeNuit.Foreground = (Brush)bc.ConvertFrom("#FFFFFF");
                TBXReponse.Foreground = (Brush)bc.ConvertFrom("#FFFFFF");
                TBXReponse.Background = (Brush)bc.ConvertFrom("#000000");

            }

            else if (SDRModeNuit.Value == 0)
            {
                var bc = new BrushConverter();
                WNDFenetrePrincipale.Background = (Brush)bc.ConvertFrom("#FFFFFF");
                LBLBonnesReponses.Foreground = (Brush)bc.ConvertFrom("#000000");
                LBLBonnesReponsesValeur.Foreground = (Brush)bc.ConvertFrom("#000000");
                LBLMauvaisesReponses.Foreground = (Brush)bc.ConvertFrom("#000000");
                LBLMauvaisesReponsesValeur.Foreground = (Brush)bc.ConvertFrom("#000000");
                LBLQuestion.Foreground = (Brush)bc.ConvertFrom("#000000");
                LBLTitre.Foreground = (Brush)bc.ConvertFrom("#000000");
                SDRModeNuit.Foreground = (Brush)bc.ConvertFrom("#000000");
                TBXReponse.Foreground = (Brush)bc.ConvertFrom("#000000");
                TBXReponse.Background = (Brush)bc.ConvertFrom("#FFFFFF");
            }
        }
        private void sonfaux()
        {
            SoundPlayer Faux = new SoundPlayer(@"..\..\..\..\Faux.wav");
            Faux.Play();
        }
        private void sonvrai()
        {
            SoundPlayer Vrai = new SoundPlayer(@"..\..\..\..\Vrai.wav");
            Vrai.Play();
        }

        private void TBXReponse_GotFocus(object sender, RoutedEventArgs e)
        {
            TBXReponse.Text = string.Empty;        }

        private void BTNQuitter_Click(object sender, RoutedEventArgs e)
        {
            MessageBoxResult m = MessageBox.Show("Do u  rly want to leave ?", "Quit", MessageBoxButton.YesNo);
            
            if (m == MessageBoxResult.Yes) { 
                Close();
            }
        }

    }
    
}
