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

namespace Ghetto_paint
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public Point pointToWindow;
        public Brush AppColor;
        public int SelectedObject;
        public string SelectedColor = null;

        public MainWindow()
        {
            InitializeComponent();
            AppColor = Brushes.Blue;
        }

        private void btnCircle_Click(object sender, RoutedEventArgs e)
        {
            SelectedObject = 1;
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

        private void slWidth_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            lblWidth.Content = Convert.ToInt16(slWidth.Value);
        }

        private void slHeight_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            lblHeight.Content = Convert.ToInt16(slHeight.Value);
        }

        private void btnRect_Click(object sender, RoutedEventArgs e)
        {
            SelectedObject = 2;
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
            if (SelectedObject == 1)
            {
                Ellipse();
            } else if (SelectedObject == 2)
            {
                btnRect_Click(sender, e);
            }

        }
        private void btnLine_Click(object sender, MouseButtonEventArgs e)
        {
            btnLine.IsEnabled = false;
            btnLine.Fill = Brushes.Aqua;
        }
    }
}
