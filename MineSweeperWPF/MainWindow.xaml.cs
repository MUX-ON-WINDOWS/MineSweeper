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
        private int points = 0;

        List<int> mines = new List<int>();
        private int minecount = 10;

        private int heightCanvas = 6;
        private int widthCanvas = 9;

        public MainWindow()
        {
            gameBoard(points);
            InitializeComponent();
            genMines(minecount);
            genButtons();
        }
        private void rightMouse(object sender, MouseButtonEventArgs e)
        {
            
            Button button = sender as Button;

            if (button.Background == Brushes.LightGray)
            {
                ImageBrush flagButton = new ImageBrush();
                flagButton.ImageSource = new BitmapImage(new Uri("flag.png", UriKind.Relative));
                button.Background = flagButton;
            } else
            {
                button.Background = Brushes.LightGray;
            }
        }

        private void handelerButtonClick(object sender, RoutedEventArgs e)
        {
            Button button = sender as Button;

            foreach (int mine in mines)
            {
                string buttonMineNumber = "btn" + mine.ToString();
                int x = 0, y = 0;

                int counter = 0;
                int tempMine = Convert.ToInt32(button.Name.Substring(3));
                while (tempMine >= 0)
                {

                    tempMine -= widthCanvas;

                    if (tempMine < 0)
                    {
                        y = counter;
                        x = tempMine + widthCanvas;
                    }

                    counter++;
                }

                //checkButtons(x, y);

                if (buttonMineNumber == button.Name)
                {
                    
                    ImageBrush imageBrush = new ImageBrush();
                    imageBrush.ImageSource = new BitmapImage(new Uri("bomb.png", UriKind.Relative));

                    button.Background = imageBrush;

                    gameOver();
                    return;
                } else
                {
                    int count = 0;
                    // RIGHT TOP
                    if (mines.Contains((y - 1) * widthCanvas + (x + 1)))
                        count++;

                    // TOP TOP
                    if (mines.Contains((y - 1) * widthCanvas + x))
                        count++;

                    // LEFT TOP
                    if (mines.Contains((y - 1) * widthCanvas + (x - 1)))
                        count++;

                    // MIDDLE LEFT
                    if (mines.Contains(y * widthCanvas + (x - 1)))
                        count++;

                    // MIDDLE RIGHT
                    if (mines.Contains(y * widthCanvas + (x + 1)))
                        count++;

                    // BOTTOM LEFT 
                    if (mines.Contains((y + 1) * widthCanvas + (x -1)))
                        count++;

                    // BOTTOM BOTTOM
                    if (mines.Contains((y + 1) * widthCanvas + x))
                        count++;

                    // BOTTOM RIGHT
                    if (mines.Contains((y + 1) * widthCanvas + (x + 1)))
                        count++;

                  
                    switch (count)
                    {
                        case 0:
                        {
                            button.IsEnabled = false;
                            break;
                        } 
                        case 1:
                        {
                            ImageBrush imageBrush = new ImageBrush();
                            imageBrush.ImageSource = new BitmapImage(new Uri("1.png", UriKind.Relative));

                            button.Background = imageBrush;
                            break;
                        }
                        case 2:
                        {
                            ImageBrush imageBrush = new ImageBrush();
                            imageBrush.ImageSource = new BitmapImage(new Uri("2.png", UriKind.Relative));

                            button.Background = imageBrush;
                            break;
                        }
                        case 3:
                        {
                            ImageBrush imageBrush = new ImageBrush();
                            imageBrush.ImageSource = new BitmapImage(new Uri("3.png", UriKind.Relative));

                            button.Background = imageBrush;
                            break;
                        }
                        case 4:
                            {
                                ImageBrush imageBrush = new ImageBrush();
                                imageBrush.ImageSource = new BitmapImage(new Uri("4.png", UriKind.Relative));

                                button.Background = imageBrush;
                                break;
                            }
                        case 5:
                            {
                                ImageBrush imageBrush = new ImageBrush();
                                imageBrush.ImageSource = new BitmapImage(new Uri("5.png", UriKind.Relative));

                                button.Background = imageBrush;
                                break;
                            }
                        case 6:
                            {
                                ImageBrush imageBrush = new ImageBrush();
                                imageBrush.ImageSource = new BitmapImage(new Uri("6.png", UriKind.Relative));

                                button.Background = imageBrush;
                                break;
                            }
                        case 7:
                            {
                                ImageBrush imageBrush = new ImageBrush();
                                imageBrush.ImageSource = new BitmapImage(new Uri("7.png", UriKind.Relative));

                                button.Background = imageBrush;
                                break;
                            }
                        case 8:
                            {
                                ImageBrush imageBrush = new ImageBrush();
                                imageBrush.ImageSource = new BitmapImage(new Uri("8.png", UriKind.Relative));

                                button.Background = imageBrush;
                                break;
                            }
                        default:
                            {
                                button.IsEnabled = false;
                                break;
                            }
                    }


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
                    button.MouseRightButtonDown += rightMouse;
                    button.Name = giveName;
                    button.Background = Brushes.LightGray;

                    Canvas.SetLeft(button, x * 50 + 20);
                    Canvas.SetTop(button, y * 50);

                    genCanvas.Children.Add(button);
                }
            }
            //foreach (int mine in mines)
            //{
            //    for (int x = 0; x < widthCanvas; x++)
            //    {
            //        for (int y = 0; y < heightCanvas; y++)
            //        {
            //            if (mine == y * widthCanvas + x)
            //            {
            //                Label label = new Label();

            //                Canvas.SetLeft(label, x * 50 + 20);
            //                Canvas.SetTop(label, y * 50);

            //                genCanvas.Children.Add(label);

            //                label.Content = mine;
            //            }
            //        } 
            //    }
            //}
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
            string MessageBoxTitle = "Opnieuw proberen?";
            string MessageBoxContent = "Oh nee je blies op!";

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

        private void gameBoard(int points)
        {
            if (points == 0) 
            { 
                return; 
            } 
            else 
            { 
                gameBoardText.Content = (points + " punten");       
            }
        }
        private void reset_Click(object sender, RoutedEventArgs e)
        {
            genMines(minecount);
            genButtons();
            gameBoard(points);
            MessageBox.Show("Nieuw spel begonnen");
            gameBoardText.Content = (points + " punten");

        }
    }
}
