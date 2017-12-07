using System;
using System.Collections.Generic;
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
using System.Threading;

namespace Ghetto_paint
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        //public Thread thread;
        public Point pointToWindow;
        public Brush AppColor;
        public int SelectedObject;
        public string SelectedColor = null;
        public bool MouseDraw;

        public MainWindow()
        {
            InitializeComponent();
            AppColor = Brushes.Blue;
            Thread thread = new Thread(EraserThread);
            thread.SetApartmentState(ApartmentState.STA);
            thread.Start();
        }

        private void slWidth_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            lblWidth.Content = Convert.ToInt16(slWidth.Value);
        }

        private void slHeight_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            lblHeight.Content = Convert.ToInt16(slHeight.Value);
        }
        private void slThickness_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            lblThickness.Content = Convert.ToInt16(slThickness.Value);
        }

        private void Ellipse()
        {
            Ellipse ellipse = new Ellipse();
            ellipse.Width = slWidth.Value;
            ellipse.Height = slHeight.Value;
            ellipse.Stroke = AppColor;
            ellipse.Margin = new Thickness(pointToWindow.X, pointToWindow.Y, 0, 0);
            Dock.Children.Add(ellipse);
        }

        private void Rectangle()
        {
            Rectangle rectangle = new Rectangle();
            rectangle.Width = slWidth.Value;
            rectangle.Height = slHeight.Value;
            rectangle.Stroke = AppColor;
            rectangle.Margin = new Thickness(pointToWindow.X, pointToWindow.Y, 0, 0);
            Dock.Children.Add(rectangle);
        }

        private void Pencil()
        {
            Ellipse ellipse = new Ellipse();
            ellipse.Width = slThickness.Value;
            ellipse.Height = slThickness.Value;
            ellipse.Fill = AppColor;
            ellipse.Stroke = AppColor;
            ellipse.Margin = new Thickness(pointToWindow.X - slThickness.Value/2, pointToWindow.Y - slThickness.Value/2, 0, 0);
            Dock.Children.Add(ellipse);
        }

        private void EraserThread()
        {
            while (true)
            {

                Thread.Sleep(10);
                

                this.Dispatcher.BeginInvoke(new Action(() =>
                {
                    if (SelectedObject == 3 || SelectedObject == 4)
                    {
                        Aids();
                        MouseUI.Children.Clear();                       
                        Ellipse ellipse = new Ellipse();
                        ellipse.Width = slThickness.Value;
                        ellipse.Height = slThickness.Value;
                        ellipse.Stroke = Brushes.DimGray;
                        ellipse.Margin = new Thickness(pointToWindow.X - slThickness.Value / 2, pointToWindow.Y - slThickness.Value / 2, 0, 0);
                        MouseUI.Children.Add(ellipse);
                    }
                }));
            }
        }

        private void Eraser()
        {
            Ellipse ellipse = new Ellipse();
            ellipse.Width = slThickness.Value;
            ellipse.Height = slThickness.Value;
            ellipse.Fill = Brushes.White;
            ellipse.Stroke = Brushes.White;
            ellipse.Margin = new Thickness(pointToWindow.X - slThickness.Value / 2, pointToWindow.Y - slThickness.Value / 2, 0, 0);
            Dock.Children.Add(ellipse);
        }

        private void cmbBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ComboBox MyComboBoxItem = (ComboBox)sender;
            ComboBoxItem Item = (ComboBoxItem)MyComboBoxItem.SelectedItem;
            SelectedColor = Item.Content.ToString();
            Converter();
        }

        private void Converter()
        {
            switch (SelectedColor)
            {
                case "Blue":
                    AppColor = Brushes.Blue;
                    break;
                case "Red":
                    AppColor = Brushes.Red;
                    break;
                case "Green":
                    AppColor = Brushes.Green;
                    break;
                case "Black":
                    AppColor = Brushes.Black;
                    break;
            }
        }

        public Point Aids()
        {
            Point pointToScreen = Mouse.GetPosition(this);
            pointToWindow = pointToScreen;
            return pointToScreen;
        }

        private void MousDown(object sender, MouseButtonEventArgs e)
        {
            Aids();
            MouseDraw = true;
            if (SelectedObject == 1)
            {
                Ellipse();
            }
            else if (SelectedObject == 2)
            {
                Rectangle();
            }
        }

        private void btnCircle_Click(object sender, RoutedEventArgs e)
        {
            SelectedObject = 1;
        }
        private void btnRect_Click(object sender, RoutedEventArgs e)
        {
            SelectedObject = 2;
        }
        private void btnPencil_Click(object sender, RoutedEventArgs e)
        {
            SelectedObject = 3;
        }
        private void btnEraser_Click(object sender, RoutedEventArgs e)
        {
            SelectedObject = 4;
        }
        private void MainGrid_MouseMove(object sender, MouseEventArgs e)
        {
            Aids();
            if (e.LeftButton == MouseButtonState.Pressed && SelectedObject == 3)
            {
                Pencil();
            } else if (e.LeftButton == MouseButtonState.Pressed && SelectedObject == 4)
            {
                Eraser();
            }
        }
        private void btnClear_Click(object sender, RoutedEventArgs e)
        {
            Dock.Children.Clear();
        }
    }
}
