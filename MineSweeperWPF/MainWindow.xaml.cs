using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Timers;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace MineSweeperWPF
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>

    public partial class MainWindow : Window
    {
        private int points;

        List<int> mines = new List<int>();
        private int minecount = 10;

        private int heightCanvas = 6;
        private int widthCanvas = 9;
        private int index;

        public MainWindow()
        {
            InitializeComponent();
            genMines(minecount);
            genButtons();
        }
        
        private void handelerButtonClick(object sender, RoutedEventArgs e)
        {
            Button button = sender as Button;

            foreach (int mine in mines)
            {
                string buttonMineNumber = "btn" + mine.ToString();

                if (buttonMineNumber == button.Name)
                {
                    ImageBrush imageBrush = new ImageBrush();
                    imageBrush.ImageSource = new BitmapImage(new Uri("minesweeperbomb.png", UriKind.Relative));

                    button.Background = imageBrush;

                    gameOver();
                    return;
                } else
                {
                    ImageBrush imageBrush = new ImageBrush();
                    imageBrush.ImageSource = new BitmapImage(new Uri("number1minesweeper.png", UriKind.Relative));

                    button.Background = imageBrush;
                    // Check how many bombs are around the button.
                }
            }
        }     
        private void genButtons()
        {
            genCanvas.Children.Clear();


            for (int x = 0; x < widthCanvas; x++)
            {
                for (int y = 0; y < heightCanvas; y++)
                {
                    Button button = new Button();

                    string giveName = "btn" + Convert.ToString(y * widthCanvas + x);

                    button.Width = 50;
                    button.Height = 50;
                    button.Click += handelerButtonClick;
                    button.Name = giveName;

                    Canvas.SetLeft(button, x * 50 + 20);
                    Canvas.SetTop(button, y * 50);

                    genCanvas.Children.Add(button);

                    
                }
            }
            foreach (int mine in mines)
            {
                for (int x = 0; x < widthCanvas; x++)
                {
                    for (int y = 0; y < heightCanvas; y++)
                    {
                        if (mine == y * widthCanvas + x)
                        {
                            Label label = new Label();

                            Canvas.SetLeft(label, x * 50 + 20);
                            Canvas.SetTop(label, y * 50);

                            genCanvas.Children.Add(label);

                            label.Content = mine;
                        }
                    } 
                }
            }
        }
        private void genMines(int minecount)
        {

            mines.Clear();
            
            int[] minegenned = new int[minecount];
            for (int i = 0; i < minecount; i++)
            {
                Random random = new Random();
                int randomBombVal = random.Next(0, heightCanvas * widthCanvas);
                // if button containts a mine, creates a new index for the mine.
                while (mines.Contains(randomBombVal))
                {
                    randomBombVal = random.Next(0, heightCanvas * widthCanvas);
                }
                mines.Add(randomBombVal);
                minegenned[i] = randomBombVal;
                for (int x = 0; x < minecount; x++)
                {
                    int test2 = randomBombVal - 1;
                    int test = randomBombVal + 1;
                    if (test == randomBombVal)
                    {
                        MessageBox.Show("Bomb Around you");
                    }
                }
            }   
        }

        private void gameOver()
        {
            string MessageBoxTitle = "Oh nee je blies op!";
            string MessageBoxContent = "Opnieuw proberen?";

            if (MessageBox.Show(MessageBoxTitle, MessageBoxContent, MessageBoxButton.YesNo) == MessageBoxResult.Yes)
            {
                genMines(minecount);
                genButtons();    
                MessageBox.Show("Nieuw spel begonnen");
                gameBoardText.Content = (points + " punten");
            } else
            {
                Close();
            }

        }
        private void reset_Click(object sender, RoutedEventArgs e)
        {
            genMines(minecount);
            genButtons();
            MessageBox.Show("Nieuw spel?");
            gameBoardText.Content = (points + " punten");
        }
    }
}
