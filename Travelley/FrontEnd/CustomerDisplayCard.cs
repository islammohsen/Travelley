using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Shapes;

namespace Travelley.FrontEnd
{
    public class CustomerDisplayCard
    {
        Rectangle BackGround;
        Label CustomerName;
        Label CustomerEmail;
        Image CustomerImage;
        Button View_More;
        Customer CurrentCustomer;
        MainWindow CurrentWindow;

        public CustomerDisplayCard(int index, Canvas CurrentCanvas, Customer CurrentCustomer, MainWindow CurrentWindow)
        {
            this.CurrentCustomer = CurrentCustomer;
            this.CurrentWindow = CurrentWindow;

            double BaseLoc = (index * 230) + 140;

            //creating BackGroung
            BackGround = new Rectangle();
            BackGround.Width = 800;
            BackGround.Height = 220;
            Canvas.SetLeft(BackGround, 111);
            Canvas.SetTop(BackGround, BaseLoc);
            BackGround.Fill = new SolidColorBrush(Color.FromRgb(255, 255, 255));
            BackGround.Stroke = new SolidColorBrush(Color.FromRgb(0, 0, 0));
            BackGround.StrokeThickness = 3;
            CurrentCanvas.Children.Add(BackGround);

            this.CustomerImage = CurrentCustomer.UserImage.GetImage();
            this.CustomerImage.MaxHeight = 220;
            this.CustomerImage.MaxWidth = 300;
            Canvas.SetLeft(this.CustomerImage, 140);
            Canvas.SetTop(this.CustomerImage, BaseLoc + 20);
            CurrentCanvas.Children.Add(this.CustomerImage);

            this.CustomerName = new Label();
            this.CustomerName.Content = "Name: " + CurrentCustomer.Name;
            this.CustomerName.FontSize = 25;
            this.CustomerName.FontWeight = FontWeights.Bold;
            this.CustomerName.HorizontalAlignment = HorizontalAlignment.Left;
            this.CustomerName.VerticalAlignment = VerticalAlignment.Center;
            Canvas.SetLeft(this.CustomerName, 450);
            Canvas.SetTop(this.CustomerName, BaseLoc + 20);
            CurrentCanvas.Children.Add(this.CustomerName);

            this.CustomerEmail = new Label();
            this.CustomerEmail.Content = "Email: " + CurrentCustomer.Email;
            this.CustomerEmail.FontSize = 25;
            this.CustomerEmail.FontWeight = FontWeights.Bold;
            this.CustomerEmail.HorizontalAlignment = HorizontalAlignment.Left;
            this.CustomerEmail.VerticalAlignment = VerticalAlignment.Center;
            Canvas.SetLeft(this.CustomerEmail, 450);
            Canvas.SetTop(this.CustomerEmail, BaseLoc + 50);
            CurrentCanvas.Children.Add(this.CustomerEmail);

            this.View_More = new Button();
            this.View_More.Content = "View More";
            Canvas.SetLeft(this.View_More, 750);
            Canvas.SetTop(this.View_More, BaseLoc + 150);
            View_More.Width = 152;
            View_More.Height = 48;
            View_More.Background = new SolidColorBrush(Color.FromRgb(232, 126, 49));
            View_More.Foreground = new SolidColorBrush(Color.FromRgb(255, 255, 255));
            View_More.FontSize = 20;
            View_More.Click += View_More_Click;
            View_More.Cursor = Cursors.Hand;
            CurrentCanvas.Children.Add(this.View_More);

            CurrentCanvas.Height = BaseLoc + 250;
        }

        private void View_More_Click(object sender, RoutedEventArgs e)
        {
            CurrentWindow.ShowCustomerFullData(CurrentCustomer);
        }
    }
}
