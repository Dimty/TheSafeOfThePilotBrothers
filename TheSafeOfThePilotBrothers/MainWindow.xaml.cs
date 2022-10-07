using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
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
//TODO: если успею - поиграться с MVC
namespace TheSafeOfThePilotBrothers
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        Lever[,] list;

        public MainWindow()
        {
            InitializeComponent();
            InitializeHint();
        }

        private void InitializeHint()
        {
            Hint.ToolTip =
                "Введите число, которое будет описывать сторону квадрата, и нажмите \"Enter\". \nДопустимый диапозон [2,9].";
        }

        private void TextBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                if (int.TryParse(NField.Text, out int value))
                {
                    if (value > 2 && value < 10)
                    {
                        NField.IsEnabled = false;
                        NField.Visibility = Visibility.Hidden;

                        Hint.IsEnabled = false;
                        Hint.Visibility = Visibility.Hidden;

                        InitializeLeversList(value);
                        InitializeField(value);
                        Mixing();
                        return;
                    }
                }

                Hint.Content = "Попробуйте снова";
                NField.Text = "";
            }
        }
        private void Mixing()
        {
            var rnd = new Random();
            int steps = rnd.Next(2, list.Length / 2 + list.Length / 4);
            int dimension = list.GetLength(0);
            for (int i = 0; i < steps; i++)
            {
                int x = rnd.Next(0, dimension);
                int y = rnd.Next(0, dimension);
                ChangeVerticalAndHorizontalLevers(x, y);
            }
        }
        private void InitializeLeversList(int value)
        {
            list = new Lever[value, value];
        }

        private void InitializeField(int value)
        {
            for (int i = 0; i < value; i++)
            {
                MainGrid.ColumnDefinitions.Add(new ColumnDefinition());
                MainGrid.RowDefinitions.Add(new RowDefinition());
            }

            for (int i = 0; i < value; i++)
            {
                for (int j = 0; j < value; j++)
                {
                    var bt = new Lever()
                    {
                        X = i,
                        Y = j
                    };

                    bt.Height = 50;
                    bt.Width = 50;

                    bt.Content = bt.condition.GetCondition();
                    bt.Click += LeverChangeState;

                    Grid.SetColumn(bt, i);
                    Grid.SetRow(bt, j);

                    MainGrid.Children.Add(bt);
                    list[i, j] = bt;
                }
            }
        }

        private void LeverChangeState(object sender, RoutedEventArgs e)
        {
            var lever = (Lever) sender;
            ChangeVerticalAndHorizontalLevers(lever.X, lever.Y);
            CheckAllPos();
        }

        private void CheckAllPos()
        {
            Lever fItem = null;
            for (int i = 0; i < list.GetLength(0); i++)
            {
                for (int j = 0; j < list.GetLength(0); j++)
                {
                    if (fItem is null)
                    {
                        fItem = list[i, j];
                        continue;
                    }

                    if (fItem.condition.GetCondition() != list[i, j].condition.GetCondition())
                    {
                        return;
                    }
                }
            }

            MessageBox.Show("Грац!");
            Close();
        }


        private void ChangeVerticalAndHorizontalLevers(int x, int y)
        {
            for (int i = 0; i < list.GetLength(0); i++)
            {
                for (int j = 0; j < list.GetLength(0); j++)
                {
                    if (x == i || j == y) list[i, j].Flip();
                }
            }
        }
    }
}
