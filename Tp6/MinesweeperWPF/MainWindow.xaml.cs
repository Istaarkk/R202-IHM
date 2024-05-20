using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;

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

        public MainWindow()
        {
            InitializeComponent();
        }

        private void StartGameButton_Click(object sender, RoutedEventArgs e)
        {
            if (int.TryParse(GridSizeTextBox.Text, out gridSize) && int.TryParse(NbMinesTextBox.Text, out nbMine))
            {
                nbFlag = nbMine;
                matrix = new int[gridSize, gridSize];
                nbCellOpen = 0;
                GRDGame.Children.Clear();
                GRDGame.ColumnDefinitions.Clear();
                GRDGame.RowDefinitions.Clear();

                for (int i = 0; i < gridSize; i++)
                {
                    GRDGame.ColumnDefinitions.Add(new ColumnDefinition() { Width = new GridLength(1, GridUnitType.Star) });
                    GRDGame.RowDefinitions.Add(new RowDefinition() { Height = new GridLength(1, GridUnitType.Star) });
                }

                InitGame();

                // Masquer le panneau de configuration et afficher la grille de jeu
                ConfigPanel.Visibility = Visibility.Collapsed;
                GRDGame.Visibility = Visibility.Visible;
            }
            else
            {
                MessageBox.Show("Veuillez entrer des valeurs valides pour la taille de la grille et le nombre de mines.");
            }
        }

        private void InitGame()
        {
            nbCellOpen = 0;
            CreateGrid();
            PlaceMines();
        }

        private void CreateGrid()
        {
            buttons = new Button[gridSize, gridSize];
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

        private void PlaceMines()
        {
            Random rand = new Random();
            for (int i = 0; i < nbMine; ++i)
            {
                int x = rand.Next(gridSize);
                int y = rand.Next(gridSize);
                if (matrix[x, y] == -1)
                {
                    i--;
                    continue;
                }
                matrix[x, y] = -1;
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

                if (matrix[x, y] == -1)
                {
                    MessageBox.Show("BOOM! Vous avez perdu.");
                    InitGame();
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
                MessageBox.Show("Félicitations! Vous avez gagné!");
                InitGame();
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
    }
}
