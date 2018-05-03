using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Travelley.Back_End;

namespace Travelley
{
   public class Ticket
    {
        private string serialNumber;
        private string ticketType;
        private double price;
        private Trip ticketTrip;
        private int numberOfSeats;
        private TripType tripType;

        public Ticket(string SerialNumber, Trip TicketTrip, string TicketType, TripType tripType, double Price, int NumberOfSeats)
        {
            this.SerialNumber = SerialNumber;
            this.CurrentTrip = TicketTrip;
            this.TicketType = TicketType;
            this.TripType = tripType;
            this.Price = Price;
            this.NumberOfSeats = NumberOfSeats;
        }

        public string SerialNumber { get => serialNumber; set => serialNumber = value; }
        internal Trip CurrentTrip { get => ticketTrip; set => ticketTrip = value; }
        public string TicketType { get => ticketType; set => ticketType = value; }
        public double Price { get => price; set => price = value; }
        public int NumberOfSeats { get => numberOfSeats; set => numberOfSeats = value; }
        public TripType TripType { get => tripType; set => tripType = value; }
    }
}
