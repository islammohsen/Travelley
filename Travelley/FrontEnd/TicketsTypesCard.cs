using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Shapes;
using Travelley;
using Travelley.Back_End;
using System.Windows;
using System.Media;
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
            BackGround = new Rectangle();
            BackGround.Width = 700;
            BackGround.Height = 150;
            Canvas.SetLeft(BackGround, 111);
            Canvas.SetTop(BackGround, BaseLoc);
            BackGround.Fill = new SolidColorBrush(Color.FromRgb(255, 255, 255));
            BackGround.Stroke = new SolidColorBrush(Color.FromRgb(0, 0, 0));
            BackGround.StrokeThickness = 3;
            CurrentCanvas.Children.Add(BackGround);

            //creating Type of ticket
            this.TypeLabel = new Label();
            this.TypeLabel.Content = "Ticket Type: ";
            this.TypeLabel.FontSize = 25;
            this.TypeLabel.FontWeight = FontWeights.Bold;
            this.TypeLabel.HorizontalAlignment = HorizontalAlignment.Left;
            this.TypeLabel.VerticalAlignment = VerticalAlignment.Center;
            Canvas.SetLeft(this.TypeLabel, 111);
            Canvas.SetTop(this.TypeLabel, BaseLoc);
            CurrentCanvas.Children.Add(this.TypeLabel);

            this.TicketType = new TextBox();
            this.TicketType.BorderThickness = new Thickness(0);
            this.TicketType.IsReadOnly = true;
            this.TicketType.Text = TicketType;
            this.TicketType.FontSize = 25;
            this.TicketType.FontWeight = FontWeights.Bold;
            this.TicketType.HorizontalAlignment = HorizontalAlignment.Left;
            this.TicketType.VerticalAlignment = VerticalAlignment.Center;
            Canvas.SetLeft(this.TicketType, 260);
            Canvas.SetTop(this.TicketType, BaseLoc + 5);
            CurrentCanvas.Children.Add(this.TicketType);

            //creating number of seats
            this.NumberLabel = new Label();
            this.NumberLabel.Content = "Number of seats: ";
            this.NumberLabel.FontSize = 25;
            this.NumberLabel.FontWeight = FontWeights.Bold;
            this.NumberLabel.HorizontalAlignment = HorizontalAlignment.Left;
            this.NumberLabel.VerticalAlignment = VerticalAlignment.Center;
            Canvas.SetLeft(this.NumberLabel, 111);
            Canvas.SetTop(this.NumberLabel, BaseLoc + 45);
            CurrentCanvas.Children.Add(this.NumberLabel);

            this.NumberOfSeats = new TextBox();
            this.NumberOfSeats.BorderThickness = new Thickness(0);
            this.NumberOfSeats.IsReadOnly = true;
            this.NumberOfSeats.Text = NumberOfSeats.ToString();
            this.NumberOfSeats.FontSize = 25;
            this.NumberOfSeats.FontWeight = FontWeights.Bold;
            this.NumberOfSeats.HorizontalAlignment = HorizontalAlignment.Left;
            this.NumberOfSeats.VerticalAlignment = VerticalAlignment.Center;
            Canvas.SetLeft(this.NumberOfSeats, 320);
            Canvas.SetTop(this.NumberOfSeats, BaseLoc + 50);
            CurrentCanvas.Children.Add(this.NumberOfSeats);

            //creating price
            this.PriceLabel = new Label();
            this.PriceLabel.Content = "Price: ";
            this.PriceLabel.FontSize = 25;
            this.PriceLabel.FontWeight = FontWeights.Bold;
            this.PriceLabel.HorizontalAlignment = HorizontalAlignment.Left;
            this.PriceLabel.VerticalAlignment = VerticalAlignment.Center;
            Canvas.SetLeft(this.PriceLabel, 111);
            Canvas.SetTop(this.PriceLabel, BaseLoc + 90);
            CurrentCanvas.Children.Add(this.PriceLabel);

            this.Price = new TextBox();
            this.Price.BorderThickness = new Thickness(0);
            this.Price.IsReadOnly = true;
            this.Price.Text = MainWindow.CurrentCurrency.GetValue(Price).ToString();
            this.Price.FontSize = 25;
            this.Price.FontWeight = FontWeights.Bold;
            this.Price.HorizontalAlignment = HorizontalAlignment.Left;
            this.Price.VerticalAlignment = VerticalAlignment.Center;
            Canvas.SetLeft(this.Price, 190);
            Canvas.SetTop(this.Price, BaseLoc + 95);
            CurrentCanvas.Children.Add(this.Price);

            //creating edit button
            EditButton = new Button();
            EditButton.Content = "Edit";
            Canvas.SetLeft(EditButton, 650);
            Canvas.SetTop(EditButton, BaseLoc + 90);
            EditButton.Width = 152;
            EditButton.Height = 48;
            EditButton.Background = new SolidColorBrush(Color.FromRgb(232, 126, 49));
            EditButton.Foreground = new SolidColorBrush(Color.FromRgb(255, 255, 255));
            EditButton.FontSize = 20;
            EditButton.Click += EditButton_Click;
            EditButton.Cursor = Cursors.Hand;
            CurrentCanvas.Children.Add(EditButton);

            //creating save button
            SaveButton = new Button();
            SaveButton.Content = "Save";
            SaveButton.Visibility = Visibility.Hidden;
            Canvas.SetLeft(SaveButton, 650);
            Canvas.SetTop(SaveButton, BaseLoc + 90);
            SaveButton.Width = 152;
            SaveButton.Height = 48;
            SaveButton.Background = new SolidColorBrush(Color.FromRgb(232, 126, 49));
            SaveButton.Foreground = new SolidColorBrush(Color.FromRgb(255, 255, 255));
            SaveButton.FontSize = 20;
            SaveButton.Click += SaveButton_Click;
            SaveButton.Cursor = Cursors.Hand;
            CurrentCanvas.Children.Add(SaveButton);

            CurrentCanvas.Height = BaseLoc + 175;
        }

        private void EditButton_Click(object sender, RoutedEventArgs e)
        {
            this.TicketType.IsReadOnly = false;
            this.TicketType.BorderThickness = new Thickness(3);
            this.TicketType.BorderBrush = new SolidColorBrush(Colors.Black);

            this.NumberOfSeats.IsReadOnly = false;
            this.NumberOfSeats.BorderThickness = new Thickness(3);
            this.NumberOfSeats.BorderBrush = new SolidColorBrush(Colors.Black);

            this.Price.IsReadOnly = false;
            this.Price.BorderThickness = new Thickness(3);
            this.Price.BorderBrush = new SolidColorBrush(Colors.Black);

            Prev = this.TicketType.Text.ToString();
            EditButton.Visibility = Visibility.Hidden;
            SaveButton.Visibility = Visibility.Visible;
        }


        private void SaveButton_Click(object sender, RoutedEventArgs e)
        {
            this.TicketType.IsReadOnly = true;
            this.TicketType.BorderThickness = new Thickness(0);

            this.NumberOfSeats.IsReadOnly = true;
            this.NumberOfSeats.BorderThickness = new Thickness(0);

            this.Price.IsReadOnly = true;
            this.Price.BorderThickness = new Thickness(0);

            EditButton.Visibility = Visibility.Visible;
            SaveButton.Visibility = Visibility.Hidden;

            if (Prev == this.TicketType.Text || CurrentTrip.NumberOfSeats.ContainsKey(this.TicketType.Text) == false)
            {
                DataBase.UpdateTripsTickets(CurrentTrip, Prev, this.TicketType.Text, int.Parse(this.NumberOfSeats.Text), MainWindow.CurrentCurrency.ToEGP(double.Parse(Price.Text)));
            }
            else
            {
                this.TicketType.Text = Prev;
                this.NumberOfSeats.Text = CurrentTrip.NumberOfSeats[Prev].ToString();
                this.Price.Text = CurrentTrip.PriceOfSeat[Prev].ToString();
                MessageBox.Show("Ticket Type already exists");
            }
        }
    }
}
