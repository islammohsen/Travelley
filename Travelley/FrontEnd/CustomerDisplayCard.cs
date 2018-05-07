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
        Label NumberOfTrips;
        Label Discount;
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
                Stroke = new SolidColorBrush(Color.FromRgb(0, 0, 0)),
                StrokeThickness = 3
            };
            Canvas.SetLeft(BackGround, 111);
            Canvas.SetTop(BackGround, BaseLoc);
            CurrentCanvas.Children.Add(BackGround);

            CustomerImage = CurrentCustomer.UserImage.GetImage();
            CustomerImage.MaxHeight = 180;
            CustomerImage.MaxWidth = 300;
            CustomerImage.Loaded += CustomerImage_Loaded;
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
            Canvas.SetTop(CustomerName, BaseLoc + 20);
            CurrentCanvas.Children.Add(CustomerName);

            CustomerEmail = new Label
            {
                Content = "Email: " + CurrentCustomer.Email,
                FontSize = 25,
                FontWeight = FontWeights.Bold,
                HorizontalAlignment = HorizontalAlignment.Left,
                VerticalAlignment = VerticalAlignment.Center
            };
            Canvas.SetTop(CustomerEmail, BaseLoc + 50);
            CurrentCanvas.Children.Add(CustomerEmail);



            NumberOfTrips = new Label
            {
                Content = "Number of trips: " + CurrentCustomer.numberOfTrips,
                FontSize = 25,
                FontWeight = FontWeights.Bold,
                HorizontalAlignment = HorizontalAlignment.Left,
                VerticalAlignment = VerticalAlignment.Center
            };
            Canvas.SetTop(NumberOfTrips, BaseLoc + 80);
            CurrentCanvas.Children.Add(NumberOfTrips);

            Discount = new Label
            {
                Content = "Discount: " + CurrentCustomer.Discount,
                FontSize = 25,
                FontWeight = FontWeights.Bold,
                HorizontalAlignment = HorizontalAlignment.Left,
                VerticalAlignment = VerticalAlignment.Center
            };
            Canvas.SetTop(Discount, BaseLoc + 110);
            CurrentCanvas.Children.Add(Discount);

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

        private void CustomerImage_Loaded(object sender, RoutedEventArgs e)
        {
            Canvas.SetLeft(CustomerName, 160 + CustomerImage.ActualWidth);
            Canvas.SetLeft(CustomerEmail, 160 + CustomerImage.ActualWidth);
            Canvas.SetLeft(NumberOfTrips, 160 + CustomerImage.ActualWidth);
            Canvas.SetLeft(Discount, 160 + CustomerImage.ActualWidth);
        }
    }
}
