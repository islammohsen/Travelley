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
    public class TicketDisplayCard
    {
        Rectangle BackGround;
        Label SerialNumber;
        Label CustomerName;
        Label TripName;
        Label TypeOfTicket;
        Label TypeOfTrip;
        Label NumberOfSeats;
        Label Price;

        Customer CurrentCustomer;
        Trip CurrentTrip;
        Ticket CurrentTicket;
        MainWindow CurrentWindow;

        public TicketDisplayCard(int index, Canvas CurrentCanvas, MainWindow CurrentWindow, Customer CurrentCustomer, Trip CurrentTrip, Ticket CurrentTicket)
        {
            this.CurrentWindow = CurrentWindow;
            this.CurrentCustomer = CurrentCustomer;
            this.CurrentTrip = CurrentTrip;
            this.CurrentTicket = CurrentTicket;

            double BaseLoc = (index * 340) + 40;

            //creating BackGroung
            BackGround = new Rectangle();
            BackGround.Width = 800;
            BackGround.Height = 300;
            Canvas.SetLeft(BackGround, 111);
            Canvas.SetTop(BackGround, BaseLoc);
            BackGround.Fill = new SolidColorBrush(Color.FromRgb(255, 255, 255));
            BackGround.Stroke = new SolidColorBrush(Color.FromRgb(0, 0, 0));
            BackGround.StrokeThickness = 3;
            CurrentCanvas.Children.Add(BackGround);

            CustomerName = new Label();
            CustomerName.Content = "Customer Name: " + CurrentCustomer.Name;
            CustomerName.FontSize = 25;
            CustomerName.FontWeight = FontWeights.Bold;
            CustomerName.HorizontalAlignment = HorizontalAlignment.Left;
            CustomerName.VerticalAlignment = VerticalAlignment.Center;
            CustomerName.MouseDoubleClick += CustomerName_DoubleClick;
            CustomerName.Cursor = Cursors.Hand;
            Canvas.SetLeft(CustomerName, 120);
            Canvas.SetTop(CustomerName, BaseLoc + 10);
            CurrentCanvas.Children.Add(CustomerName);

            TripName = new Label();
            TripName.Content = "Trip: " + CurrentTrip.Departure + " - " + CurrentTrip.Destination;
            TripName.FontSize = 25;
            TripName.FontWeight = FontWeights.Bold;
            TripName.HorizontalAlignment = HorizontalAlignment.Left;
            TripName.VerticalAlignment = VerticalAlignment.Center;
            TripName.MouseDoubleClick += TripName_DoubleClick;
            TripName.Cursor = Cursors.Hand;
            Canvas.SetLeft(TripName, 120);
            Canvas.SetTop(TripName, BaseLoc + 50);
            CurrentCanvas.Children.Add(TripName);

            TypeOfTicket = new Label();
            TypeOfTicket.Content = "Ticket Type: " + CurrentTicket.TicketType;
            TypeOfTicket.FontSize = 25;
            TypeOfTicket.FontWeight = FontWeights.Bold;
            TypeOfTicket.HorizontalAlignment = HorizontalAlignment.Left;
            TypeOfTicket.VerticalAlignment = VerticalAlignment.Center;
            Canvas.SetLeft(TypeOfTicket, 120);
            Canvas.SetTop(TypeOfTicket, BaseLoc + 90);
            CurrentCanvas.Children.Add(TypeOfTicket);

            TypeOfTrip = new Label();
            TypeOfTrip.Content = "Trip Type: " + CurrentTicket.TripType.ToString();
            TypeOfTrip.FontSize = 25;
            TypeOfTrip.FontWeight = FontWeights.Bold;
            TypeOfTrip.HorizontalAlignment = HorizontalAlignment.Left;
            TypeOfTrip.VerticalAlignment = VerticalAlignment.Center;
            Canvas.SetLeft(TypeOfTrip, 120);
            Canvas.SetTop(TypeOfTrip, BaseLoc + 130);
            CurrentCanvas.Children.Add(TypeOfTrip);

            NumberOfSeats = new Label();
            NumberOfSeats.Content = "Reserved Seats: " + CurrentTicket.NumberOfSeats;
            NumberOfSeats.FontSize = 25;
            NumberOfSeats.FontWeight = FontWeights.Bold;
            NumberOfSeats.HorizontalAlignment = HorizontalAlignment.Left;
            NumberOfSeats.VerticalAlignment = VerticalAlignment.Center;
            Canvas.SetLeft(NumberOfSeats, 120);
            Canvas.SetTop(NumberOfSeats, BaseLoc + 170);
            CurrentCanvas.Children.Add(NumberOfSeats);

            Price = new Label();
            Price.Content = "Price: " + CurrentTicket.Price;
            Price.FontSize = 25;
            Price.FontWeight = FontWeights.Bold;
            Price.HorizontalAlignment = HorizontalAlignment.Left;
            Price.VerticalAlignment = VerticalAlignment.Center;
            Canvas.SetLeft(Price, 120);
            Canvas.SetTop(Price, BaseLoc + 210);
            CurrentCanvas.Children.Add(Price);

            SerialNumber = new Label();
            SerialNumber.Content = "SerialNumber: " + CurrentTicket.SerialNumber;
            SerialNumber.FontSize = 25;
            SerialNumber.FontWeight = FontWeights.Bold;
            SerialNumber.HorizontalAlignment = HorizontalAlignment.Left;
            SerialNumber.VerticalAlignment = VerticalAlignment.Center;
            Canvas.SetLeft(SerialNumber, 120);
            Canvas.SetTop(SerialNumber, BaseLoc + 250);
            CurrentCanvas.Children.Add(SerialNumber);

            CurrentCanvas.Height = BaseLoc + 330;
        }

        private void CustomerName_DoubleClick(object sender, RoutedEventArgs e)
        {
            CurrentWindow.ShowCustomerFullData(CurrentCustomer);
        }

        private void TripName_DoubleClick(object sender, RoutedEventArgs e)
        {
            CurrentWindow.ShowTripFullData(CurrentTrip);
        }
    }
}
