using System.Windows.Controls;
using System.Windows.Shapes;
using System.Windows;
using System.Windows.Media;
using System.Windows.Input;

namespace Travelley.FrontEnd
{
    public class TicketsTypesCard
    {
        Label TypeLabel;
        Label NumberLabel;
        Label PriceLabel;
        TextBox TicketType;
        TextBox NumberOfSeats;
        TextBox Price;
        Button EditButton;
        Button SaveButton;
        Rectangle BackGround;
        string Prev;
        Trip CurrentTrip;

        public TicketsTypesCard(int index, Canvas CurrentCanvas, Trip CurrentTrip, string TicketType, int NumberOfSeats, double Price)
        {
            this.CurrentTrip = CurrentTrip;

            double BaseLoc = (index * 175) + 120;

            //creating BackGroung
            BackGround = new Rectangle
            {
                Width = 700,
                Height = 150,
                Fill = new SolidColorBrush(Color.FromRgb(255, 255, 255)),
                Stroke = new SolidColorBrush(Color.FromRgb(0, 0, 0)),
                StrokeThickness = 3
            };
            CurrentCanvas.Children.Add(BackGround);
            Canvas.SetLeft(BackGround, 111);
            Canvas.SetTop(BackGround, BaseLoc);

            //creating Type of ticket
            TypeLabel = new Label
            {
                Content = "Ticket Type: ",
                FontSize = 25,
                FontWeight = FontWeights.Bold,
                HorizontalAlignment = HorizontalAlignment.Left,
                VerticalAlignment = VerticalAlignment.Center
            };
            Canvas.SetLeft(this.TypeLabel, 111);
            Canvas.SetTop(this.TypeLabel, BaseLoc);
            CurrentCanvas.Children.Add(this.TypeLabel);

            this.TicketType = new TextBox
            {
                BorderThickness = new Thickness(0),
                IsReadOnly = true,
                Text = TicketType,
                FontSize = 25,
                FontWeight = FontWeights.Bold,
                HorizontalAlignment = HorizontalAlignment.Left,
                VerticalAlignment = VerticalAlignment.Center
            };
            Canvas.SetLeft(this.TicketType, 260);
            Canvas.SetTop(this.TicketType, BaseLoc + 5);
            CurrentCanvas.Children.Add(this.TicketType);

            //creating number of seats
            NumberLabel = new Label
            {
                Content = "Number of seats: ",
                FontSize = 25,
                FontWeight = FontWeights.Bold,
                HorizontalAlignment = HorizontalAlignment.Left,
                VerticalAlignment = VerticalAlignment.Center
            };
            Canvas.SetLeft(this.NumberLabel, 111);
            Canvas.SetTop(this.NumberLabel, BaseLoc + 45);
            CurrentCanvas.Children.Add(this.NumberLabel);

            this.NumberOfSeats = new TextBox
            {
                BorderThickness = new Thickness(0),
                IsReadOnly = true,
                Text = NumberOfSeats.ToString(),
                FontSize = 25,
                FontWeight = FontWeights.Bold,
                HorizontalAlignment = HorizontalAlignment.Left,
                VerticalAlignment = VerticalAlignment.Center
            };
            Canvas.SetLeft(this.NumberOfSeats, 320);
            Canvas.SetTop(this.NumberOfSeats, BaseLoc + 50);
            CurrentCanvas.Children.Add(this.NumberOfSeats);

            //creating price
            PriceLabel = new Label
            {
                Content = "Price: ",
                FontSize = 25,
                FontWeight = FontWeights.Bold,
                HorizontalAlignment = HorizontalAlignment.Left,
                VerticalAlignment = VerticalAlignment.Center
            };
            Canvas.SetLeft(this.PriceLabel, 111);
            Canvas.SetTop(this.PriceLabel, BaseLoc + 90);
            CurrentCanvas.Children.Add(this.PriceLabel);

            this.Price = new TextBox
            {
                BorderThickness = new Thickness(0),
                IsReadOnly = true,
                Text = MainWindow.CurrentCurrency.GetValue(Price).ToString(),
                FontSize = 25,
                FontWeight = FontWeights.Bold,
                HorizontalAlignment = HorizontalAlignment.Left,
                VerticalAlignment = VerticalAlignment.Center
            };
            Canvas.SetLeft(this.Price, 190);
            Canvas.SetTop(this.Price, BaseLoc + 95);
            CurrentCanvas.Children.Add(this.Price);

            //creating edit button
            EditButton = new Button
            {
                Content = "Edit",
                Width = 152,
                Height = 48,
                Background = new SolidColorBrush(Color.FromRgb(232, 126, 49)),
                Foreground = new SolidColorBrush(Color.FromRgb(255, 255, 255)),
                FontSize = 20,
                Cursor = Cursors.Hand
            };
            EditButton.Click += EditButton_Click;
            Canvas.SetLeft(EditButton, 650);
            Canvas.SetTop(EditButton, BaseLoc + 90);
            CurrentCanvas.Children.Add(EditButton);

            //creating save button
            SaveButton = new Button
            {
                Content = "Save",
                Visibility = Visibility.Hidden,
                Width = 152,
                Height = 48,
                Background = new SolidColorBrush(Color.FromRgb(232, 126, 49)),
                Foreground = new SolidColorBrush(Color.FromRgb(255, 255, 255)),
                FontSize = 20,
                Cursor = Cursors.Hand
            };
            SaveButton.Click += SaveButton_Click;
            Canvas.SetLeft(SaveButton, 650);
            Canvas.SetTop(SaveButton, BaseLoc + 90);
            CurrentCanvas.Children.Add(SaveButton);

            CurrentCanvas.Height = BaseLoc + 175;
        }

        private void EditButton_Click(object sender, RoutedEventArgs e)
        {
            TicketType.IsReadOnly = false;
            TicketType.BorderThickness = new Thickness(3);
            TicketType.BorderBrush = new SolidColorBrush(Colors.Black);

            NumberOfSeats.IsReadOnly = false;
            NumberOfSeats.BorderThickness = new Thickness(3);
            NumberOfSeats.BorderBrush = new SolidColorBrush(Colors.Black);

            Price.IsReadOnly = false;
            Price.BorderThickness = new Thickness(3);
            Price.BorderBrush = new SolidColorBrush(Colors.Black);

            Prev = TicketType.Text.ToString();
            EditButton.Visibility = Visibility.Hidden;
            SaveButton.Visibility = Visibility.Visible;
        }


        private void SaveButton_Click(object sender, RoutedEventArgs e)
        {
            TicketType.IsReadOnly = true;
            TicketType.BorderThickness = new Thickness(0);

            NumberOfSeats.IsReadOnly = true;
            NumberOfSeats.BorderThickness = new Thickness(0);

            Price.IsReadOnly = true;
            Price.BorderThickness = new Thickness(0);

            EditButton.Visibility = Visibility.Visible;
            SaveButton.Visibility = Visibility.Hidden;

            if (Prev == TicketType.Text || CurrentTrip.NumberOfSeats.ContainsKey(TicketType.Text) == false)
            {
                DataBase.UpdateTripsTickets(CurrentTrip, Prev, TicketType.Text, int.Parse(NumberOfSeats.Text), MainWindow.CurrentCurrency.ToEGP(double.Parse(Price.Text)));
            }
            else
            {
                TicketType.Text = Prev;
                NumberOfSeats.Text = CurrentTrip.NumberOfSeats[Prev].ToString();
                Price.Text = CurrentTrip.PriceOfSeat[Prev].ToString();
                MessageBox.Show("Ticket Type already exists");
            }
        }
    }
}
