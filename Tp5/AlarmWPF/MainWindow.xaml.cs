using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media;
using System.Windows.Shapes;
using System.Windows.Threading;
using System.Windows.Controls;
using System.Runtime.CompilerServices;
using System.Media;
using System.Threading;
using System.Text.RegularExpressions;
using System.Windows.Input;

namespace AlarmWPF
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        System.Media.SoundPlayer song = new System.Media.SoundPlayer(@"Alarme.wav");
        private DispatcherTimer timer;  
        private Ellipse ellipse;
        private Line seconds;
        private Line minutes;
        private Line hours;

        private void NumberValidationTextBox(object sender, TextCompositionEventArgs e)
        {
            Regex regex = new Regex("[^0-9]+");
            e.Handled = regex.IsMatch(e.Text);
        }
        public MainWindow()
        {
            InitializeComponent();
            InitializeClock();
        }

        private void InitializeClock()
        {
            timer = new DispatcherTimer();
            timer.Interval = TimeSpan.FromSeconds(1);
            timer.Tick += Timer_Tick;
            timer.Start();

            // Create and initialize the ellipse (clock face)
            ellipse = new Ellipse();
            CNVClock.Children.Add(ellipse);
            ellipse.Width = 300;
            ellipse.Height = 300;
            ellipse.Stroke = Brushes.Gray;
            ellipse.StrokeThickness = 1;

            // Create and initialize the seconds hand
            seconds = new Line();
            CNVClock.Children.Add(seconds);
            seconds.Stroke = Brushes.Red;
            seconds.StrokeThickness = 1;
            seconds.X1 = ellipse.Width / 2;
            seconds.Y1 = ellipse.Height / 2;
            // Create and initialize the minutes hand
            minutes = new Line();
            CNVClock.Children.Add(minutes);
            minutes.Stroke = Brushes.Blue;
            minutes.StrokeThickness = 1;
            minutes.X1 = ellipse.Width / 2;
            minutes.Y1 = ellipse.Height / 2;
            // Create and initialize the hours hand
            hours = new Line();
            CNVClock.Children.Add(hours);
            hours.Stroke = Brushes.Black;
            hours.StrokeThickness = 1;
            hours.X1 = ellipse.Width / 2;
            hours.Y1 = ellipse.Height / 2;
            UpdateClockHands();
        }

        private void Timer_Tick(object sender, EventArgs e)
        {
            // Update clock hands positions
            UpdateClockHands();
            CheckAlarms();
        }

        private void UpdateClockHands()
        {
            double secondsLength = ellipse.Width / 2;
            double secondAngle = (DateTime.Now.Second * Math.PI / 30) - (Math.PI / 2);
            seconds.X2 = ellipse.Width / 2 + Math.Cos(secondAngle) * secondsLength;
            seconds.Y2 = ellipse.Height / 2 + Math.Sin(secondAngle) * secondsLength;

            double minutesLength = ellipse.Width / 3; 
            double minuteAngle = (DateTime.Now.Minute * Math.PI / 30) - (Math.PI / 2);
            minutes.X2 = ellipse.Width / 2 + Math.Cos(minuteAngle) * minutesLength;
            minutes.Y2 = ellipse.Height / 2 + Math.Sin(minuteAngle) * minutesLength;

            double hoursLength = ellipse.Width / 4; 
            double hourAngle = ((DateTime.Now.Hour % 12 + DateTime.Now.Minute / 60.0) * Math.PI / 6) - (Math.PI / 2);
            hours.X2 = ellipse.Width / 2 + Math.Cos(hourAngle) * hoursLength;
            hours.Y2 = ellipse.Height / 2 + Math.Sin(hourAngle) * hoursLength;
        }

        private void media ()
        { 
            song.Play();
        }

        private void CheckAlarms()
        {
            for (int i = 0; i < ListAlarm.Items.Count; ++i)
            {
                if ((string)ListAlarm.Items[i] == DateTime.Now.Hour + " heures et " + DateTime.Now.Minute + "minutes")
                {
                    media();
                    MessageBoxResult result = MessageBox.Show("Il est temps ! Souhaitez-vous arrêter l'horloge ?", "Alarme", MessageBoxButton.YesNo);
                    ellipse.Stroke = Brushes.Red;

                    if (result == MessageBoxResult.Yes)
                    {
                        ListAlarm.Items.RemoveAt(i);
                        ellipse.Stroke = Brushes.Black;
                        song.Stop();
                    }
                }
            }
        }

        private void BTNAjouter_Click(object sender, RoutedEventArgs e)
        {
            ListAlarm.Items.Add(HourTextBox.Text + " heures et " +MinuteTextBox.Text + "minutes");
        }

        private void BTNDelete_Click(object sender, RoutedEventArgs e)
        {
                ListAlarm.Items.Remove(ListAlarm.SelectedItem);
            
        }

        private void BTN_ACTIVER_Click(object sender, RoutedEventArgs e)
        {
             for (int i = 0; i < ListAlarm.Items.Count; ++i)
            {
                        ListAlarm.Items.RemoveAt(i);
                        song.Stop();
                        ellipse.Stroke = Brushes.Black;
            }
            }


    }

}
