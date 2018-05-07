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

            double BaseLoc = (index * 230) + 40;

            //creating BackGroung
            BackGround = new Rectangle
            {
                Width = 800,
                Height = 220,
                Fill = new SolidColorBrush(Color.FromRgb(255, 255, 255)),
                Stroke = new SolidColorBrush(Color.FromRgb(255, 255, 255)),
                StrokeThickness = 3
            };
            Canvas.SetLeft(BackGround, 111);
            Canvas.SetTop(BackGround, BaseLoc);
            CurrentCanvas.Children.Add(BackGround);

            CustomerImage = CurrentCustomer.UserImage.GetImage();
            CustomerImage.MaxHeight = 180;
            CustomerImage.MaxWidth = 300;
            Canvas.SetLeft(CustomerImage, 140);
            Canvas.SetTop(CustomerImage, BaseLoc + 20);
            CurrentCanvas.Children.Add(CustomerImage);

            CustomerName = new Label
            {
                Content = "Name: " + CurrentCustomer.Name,
                FontSize = 25,
                FontWeight = FontWeights.Bold,
                HorizontalAlignment = HorizontalAlignment.Left,
                VerticalAlignment = VerticalAlignment.Center
            };
            Canvas.SetLeft(this.CustomerName, 450);
            Canvas.SetTop(this.CustomerName, BaseLoc + 20);
            CurrentCanvas.Children.Add(this.CustomerName);

            CustomerEmail = new Label
            {
                Content = "Email: " + CurrentCustomer.Email,
                FontSize = 25,
                FontWeight = FontWeights.Bold,
                HorizontalAlignment = HorizontalAlignment.Left,
                VerticalAlignment = VerticalAlignment.Center
            };
            Canvas.SetLeft(this.CustomerEmail, 450);
            Canvas.SetTop(this.CustomerEmail, BaseLoc + 50);
            CurrentCanvas.Children.Add(this.CustomerEmail);

            View_More = new Button
            {
                Content = "View More",
                Width = 152,
                Height = 48,
                Background = new SolidColorBrush(Color.FromRgb(232, 126, 49)),
                Foreground = new SolidColorBrush(Color.FromRgb(255, 255, 255)),
                FontSize = 20,
                Cursor = Cursors.Hand
            };
            View_More.Click += View_More_Click;
            Canvas.SetLeft(View_More, 750);
            Canvas.SetTop(View_More, BaseLoc + 150);
            CurrentCanvas.Children.Add(View_More);

            CurrentCanvas.Height = BaseLoc + 250;
        }

        private void View_More_Click(object sender, RoutedEventArgs e)
        {
            CurrentWindow.ShowCustomerFullData(CurrentCustomer);
        }
    }
}
