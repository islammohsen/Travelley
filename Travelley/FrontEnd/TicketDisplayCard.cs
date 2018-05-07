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
        Button Delete_Button;

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
            BackGround = new Rectangle
            {
                Width = 800,
                Height = 300,
                Fill = new SolidColorBrush(Color.FromRgb(255, 255, 255)),
                Stroke = new SolidColorBrush(Color.FromRgb(0, 0, 0)),
                StrokeThickness = 3
            };
            Canvas.SetLeft(BackGround, 111);
            Canvas.SetTop(BackGround, BaseLoc);
            CurrentCanvas.Children.Add(BackGround);

            CustomerName = new Label
            {
                Content = "Customer Name: " + CurrentCustomer.Name,
                FontSize = 25,
                FontWeight = FontWeights.Bold,
                HorizontalAlignment = HorizontalAlignment.Left,
                VerticalAlignment = VerticalAlignment.Center,
                Cursor = Cursors.Hand
            };
            CustomerName.MouseDoubleClick += CustomerName_DoubleClick;
            Canvas.SetLeft(CustomerName, 120);
            Canvas.SetTop(CustomerName, BaseLoc + 10);
            CurrentCanvas.Children.Add(CustomerName);

            TripName = new Label
            {
                Content = "Trip: " + CurrentTrip.Departure + " - " + CurrentTrip.Destination,
                FontSize = 25,
                FontWeight = FontWeights.Bold,
                HorizontalAlignment = HorizontalAlignment.Left,
                VerticalAlignment = VerticalAlignment.Center,
                Cursor = Cursors.Hand
            };
            TripName.MouseDoubleClick += TripName_DoubleClick;
            Canvas.SetLeft(TripName, 120);
            Canvas.SetTop(TripName, BaseLoc + 50);
            CurrentCanvas.Children.Add(TripName);

            TypeOfTicket = new Label
            {
                Content = "Ticket Type: " + CurrentTicket.TicketType,
                FontSize = 25,
                FontWeight = FontWeights.Bold,
                HorizontalAlignment = HorizontalAlignment.Left,
                VerticalAlignment = VerticalAlignment.Center
            };
            Canvas.SetLeft(TypeOfTicket, 120);
            Canvas.SetTop(TypeOfTicket, BaseLoc + 90);
            CurrentCanvas.Children.Add(TypeOfTicket);

            TypeOfTrip = new Label
            {
                Content = "Trip Type: " + CurrentTicket.TripType.ToString(),
                FontSize = 25,
                FontWeight = FontWeights.Bold,
                HorizontalAlignment = HorizontalAlignment.Left,
                VerticalAlignment = VerticalAlignment.Center
            };
            Canvas.SetLeft(TypeOfTrip, 120);
            Canvas.SetTop(TypeOfTrip, BaseLoc + 130);
            CurrentCanvas.Children.Add(TypeOfTrip);

            NumberOfSeats = new Label
            {
                Content = "Reserved Seats: " + CurrentTicket.NumberOfSeats,
                FontSize = 25,
                FontWeight = FontWeights.Bold,
                HorizontalAlignment = HorizontalAlignment.Left,
                VerticalAlignment = VerticalAlignment.Center
            };
            Canvas.SetLeft(NumberOfSeats, 120);
            Canvas.SetTop(NumberOfSeats, BaseLoc + 170);
            CurrentCanvas.Children.Add(NumberOfSeats);

            Price = new Label
            {
                Content = "Price: " + MainWindow.CurrentCurrency.GetValue(CurrentTicket.Price),
                FontSize = 25,
                FontWeight = FontWeights.Bold,
                HorizontalAlignment = HorizontalAlignment.Left,
                VerticalAlignment = VerticalAlignment.Center
            };
            Canvas.SetLeft(Price, 120);
            Canvas.SetTop(Price, BaseLoc + 210);
            CurrentCanvas.Children.Add(Price);

            SerialNumber = new Label
            {
                Content = "SerialNumber: " + CurrentTicket.SerialNumber,
                FontSize = 25,
                FontWeight = FontWeights.Bold,
                HorizontalAlignment = HorizontalAlignment.Left,
                VerticalAlignment = VerticalAlignment.Center
            };
            Canvas.SetLeft(SerialNumber, 120);
            Canvas.SetTop(SerialNumber, BaseLoc + 250);
            CurrentCanvas.Children.Add(SerialNumber);

            Delete_Button = new Button
            {
                Content = "Delete",
                Width = 152,
                Height = 48,
                Background = new SolidColorBrush(Color.FromRgb(232, 126, 49)),
                Foreground = new SolidColorBrush(Color.FromRgb(255, 255, 255)),
                FontSize = 20,
                Cursor = Cursors.Hand
            };
            Delete_Button.Click += Delete_Button_Click;
            Canvas.SetLeft(Delete_Button, 750);
            Canvas.SetTop(Delete_Button, BaseLoc + 20);
            CurrentCanvas.Children.Add(Delete_Button);

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

        private void Delete_Button_Click(object sender, RoutedEventArgs e)
        {
            DataBase.DeleteTicket(CurrentTicket);
            CurrentWindow.ShowListOfTickets(CurrentWindow.GetAllTickets());
        }
    }
}
