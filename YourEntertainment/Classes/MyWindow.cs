using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace YourEntertainment.Classes
{
    public class MyWindow
    {
        private double height;
        private double width;

        public MyWindow (double height, double width)
        {
            this.height = height;
            this.width = width;
        }

        public double getHeight()
        {
            return height;
        }

        public double getWidth()
        {
            return width;
        }

        public void initializeLeftBarButton(Button button, string name, double buttonW, double buttonH, double top, double left)
        {

            BitmapImage bitmap = new BitmapImage();
            Image img = new Image();
            bitmap.BeginInit();
            bitmap.UriSource = new Uri("/YourEntertainment;component/Theme/Buttons/LeftBar/" + name + ".png", UriKind.Relative);
            bitmap.EndInit();
            img.Stretch = Stretch.Fill;
            img.Source = bitmap;
            button.Content = img;
            button.Background = new ImageBrush(bitmap);
            button.Width = width * buttonW;
            button.Height = height * buttonH;
            Canvas.SetTop(button, height * top);
            Canvas.SetLeft(button, width * left);
        }

        public void initializeLeftBlueBarButton(Button button, string name, double buttonW, double buttonH, double top, double left)
        {
            changeImage(button, name);
            button.Width = width * buttonW;
            button.Height = height * buttonH;
            Canvas.SetTop(button, height * top);
            Canvas.SetLeft(button, width * left);
        }

        public void initializeTopBar(Button button, string name, double buttonW, double buttonH, double top, double left)
        {

            BitmapImage bitmap = new BitmapImage();
            Image img = new Image();
            bitmap.BeginInit();
            bitmap.UriSource = new Uri("/YourEntertainment;component/Theme/Buttons/TopBar/" + name + ".png", UriKind.Relative);
            bitmap.EndInit();
            img.Stretch = Stretch.Fill;
            img.Source = bitmap;
            button.Content = img;
            button.Background = new ImageBrush(bitmap);
            button.Width = width * buttonW;
            button.Height = height * buttonH;
            Canvas.SetTop(button, height * top);
            Canvas.SetLeft(button, width * left);
        }



        internal void changeImage(Button button, string name)
        {
            BitmapImage bitmap = new BitmapImage();
            Image img = new Image();
            bitmap.BeginInit();
            bitmap.UriSource = new Uri("/YourEntertainment;component/Theme/Buttons/LeftBlueBar/" + name + ".png", UriKind.Relative);
            bitmap.EndInit();
            img.Stretch = Stretch.Fill;
            img.Source = bitmap;
            button.Content = img;
            button.Background = new ImageBrush(bitmap);

        }

        internal void buttonVisibility(Button button, Visibility visibility, bool enabled)
        {
            button.Visibility = visibility;
            button.IsEnabled = enabled;
        }
    }
}
