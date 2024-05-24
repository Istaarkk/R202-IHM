using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Threading;


namespace MinesweeperWPF
{
    public partial class MainWindow : Window
    {
        private int gridSize;
        private int nbMine;
        private int nbFlag;
        private int nbCellOpen = 0;
        private int[,] matrix;
        private Button[,] buttons;
        private DispatcherTimer timer;
        private int elapsedTime = 0;
        private List<GameHistory> gameHistory = new List<GameHistory>();
        private bool isFirstClick = true; // Variable pour suivre le premier clic

        public MainWindow()
        {
            InitializeComponent();
            timer = new DispatcherTimer();
            timer.Interval = TimeSpan.FromSeconds(1);
            timer.Tick += Timer_Tick;
            timer.Start(); 
        }

        private void Timer_Tick(object sender, EventArgs e)
        {
            elapsedTime++;
            TimerTextBlock.Text = $"Temps ecoulé: {elapsedTime} sec";
        }

        private void StartGameButton_Click(object sender, RoutedEventArgs e)
        {
            if (int.TryParse(GridSizeTextBox.Text, out gridSize) && int.TryParse(NbMinesTextBox.Text, out nbMine))
            {
                InitGame();
            }
            else
            {
                MessageBox.Show("Veuillez entrer des valeurs valides pour la taille de la grille et le nombre de mines.");
            }
        }

        /* private void StopGame()
         {
             timer.Stop();
             for(int i =0; i < gridSize; ++i)
             {
                 for (int j = 0; j < gridSize; ++j)
                 {
                     buttons[i, j].IsEnabled = false;

                 }

             }
         }*/

        private void InitGame()
        {
            try
            {
                nbFlag = nbMine;
                matrix = new int[gridSize, gridSize];
                nbCellOpen = 0;
                elapsedTime = 0; // Réinitialiser le temps écoulé à chaque nouvelle partie
                TimerTextBlock.Text = $"Temps écoulé: {elapsedTime} sec";
                isFirstClick = true; // Réinitialiser le suivi du premier clic



                GRDGame.Children.Clear();
                GRDGame.ColumnDefinitions.Clear();
                GRDGame.RowDefinitions.Clear();

            }
            catch (Exception)
            {

            }
        
            for (int i = 0; i < gridSize; i++)
            {
                GRDGame.ColumnDefinitions.Add(new ColumnDefinition() { Width = new GridLength(1, GridUnitType.Star) });
                GRDGame.RowDefinitions.Add(new RowDefinition() { Height = new GridLength(1, GridUnitType.Star) });
            }

            CreateGrid();

            // Masquer le panneau de configuration et afficher la grille de jeu
            ConfigPanel.Visibility = Visibility.Collapsed;
            GRDGame.Visibility = Visibility.Visible;
        }
        
        private void CreateGrid()
        {
          if(gridSize <= 1)
            {
                MessageBox.Show("Entrez une valeur plus grande que 1");
                
            }
          
            try
            {
                buttons = new Button[gridSize, gridSize];
            }
            catch(Exception) { }

            for (int i = 0; i < gridSize; i++)
            {
                for (int j = 0; j < gridSize; j++)
                {
                    matrix[i, j] = 1;
                    Button button = new Button();
                    button.Content = "";
                    button.BorderBrush = new SolidColorBrush(Colors.LightBlue);
                    button.Click += Button_Click;
                    button.MouseRightButtonUp += Button_RightClick; // Gestionnaire pour le clic droit
                    button.Tag = new Tuple<int, int>(i, j);
                    Grid.SetRow(button, i);
                    Grid.SetColumn(button, j);
                    GRDGame.Children.Add(button);
                    buttons[i, j] = button;
                }
            }
        }

        private void PlaceMines(int firstClickX, int firstClickY)
        {
            Random rand = new Random();
            int minesPlaced = 0;
            while (minesPlaced < nbMine)
            {
                int x = rand.Next(gridSize);
                int y = rand.Next(gridSize);
                // Ne pas placer de mine sur la cellule cliquée en premier
                if ((x == firstClickX && y == firstClickY) || matrix[x, y] == -1)
                {
                    continue;
                }
                matrix[x, y] = -1;
                minesPlaced++;
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                Button button = sender as Button;
                var position = (Tuple<int, int>)button.Tag;
                int x = position.Item1;
                int y = position.Item2;

                if (button.Content.ToString() == "🚩")
                {
                    return;
                }

                if (isFirstClick)
                {
                    isFirstClick = false;
                    PlaceMines(x, y); // Placer les mines après le premier clic en excluant cette cellule
                }

                if (matrix[x, y] == -1)
                {
                    timer.Stop();
                    MessageBox.Show("BOOM! Vous avez perdu.", "Partie terminée");
                    //StopGame();
                    SaveGame(false);
                    ConfigPanel.Visibility = Visibility.Visible;
                    GRDGame.Visibility = Visibility.Hidden;
                    timer.Start(); // Redémarrer le timer après avoir réinitialisé le jeu
                    return;
                }

                OpenCell(x, y);
            }
            catch (Exception)
            {
                // Handle exception if needed 
            }
        }

        private void OpenCell(int x, int y)
        {
            if (x < 0 || x >= gridSize || y < 0 || y >= gridSize || buttons[x, y].Content.ToString() != "")
                return;

            int bombs = CountAdjacentBombs(x, y);
            try
            {
                if (bombs == 0)
                {
                    buttons[x, y].Content = "0";
                }
                else
                {
                    buttons[x, y].Content = bombs.ToString();
                }

                buttons[x, y].Background = Brushes.LightGray;
                nbCellOpen++;

                buttons[x, y].Tag = "opened";

                if (bombs == 0)
                {
                    OpenCell(x - 1, y);
                    OpenCell(x + 1, y);
                    OpenCell(x, y - 1);
                    OpenCell(x, y + 1);
                }

                if (nbCellOpen == gridSize * gridSize - nbMine)
                {
                    timer.Stop();
                    MessageBox.Show("Félicitations! Vous avez gagné!");
                    //StopGame();
                    SaveGame(true);
                    ConfigPanel.Visibility = Visibility.Visible;
                    GRDGame.Visibility = Visibility.Hidden;
                    timer.Start(); // Redémarrer le timer après avoir réinitialisé le jeu
                    return;

                }
            }
            catch(Exception)
            {

            }
        }

        private int CountAdjacentBombs(int x, int y)
        {
            int count = 0;
            for (int i = -1; i <= 1; i++)
            {
                for (int j = -1; j <= 1; j++)
                {
                    int newX = x + i;
                    int newY = y + j;
                    if (newX >= 0 && newX < gridSize && newY >= 0 && newY < gridSize && matrix[newX, newY] == -1)
                        count++;
                }
            }
            return count;
        }

        private void Button_RightClick(object sender, MouseButtonEventArgs e)
        {
            try
            {
                Button button = sender as Button;
                var position = (Tuple<int, int>)button.Tag;
                int x = position.Item1;
                int y = position.Item2;

                if (button.Content.ToString() == "")
                {
                    button.Content = "🚩";
                    button.Foreground = Brushes.Red;
                }
                else if (button.Content.ToString() == "🚩")
                {
                    button.Content = "";
                    button.Foreground = Brushes.Black;
                }
            }
            catch (Exception)
            {
                // Handle exception if needed 
            }

            e.Handled = true;
        }

        private void SaveGame(bool won)
        {
            gameHistory.Add(new GameHistory
            {
                Date = DateTime.Now,
                GridSize = gridSize,
                Mines = nbMine,
                Time = elapsedTime,
                Won = won
            });
        }

        private void ShowHistoryButton_Click(object sender, RoutedEventArgs e)
        {
            HistoryListBox.Items.Clear();
            foreach (var game in gameHistory)
            {
                HistoryListBox.Items.Add($"{game.Date} - {game.GridSize}x{game.GridSize} - Mines: {game.Mines} - Temps: {game.Time} sec - {(game.Won ? "Gagné" : "Perdu")}");
            }
        }
    }

    public class GameHistory
    {
        public DateTime Date { get; set; }
        public int GridSize { get; set; }
        public int Mines { get; set; }
        public int Time { get; set; }
        public bool Won { get; set; }
    }
}
