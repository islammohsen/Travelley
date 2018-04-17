using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Travelley
{
    class Ticket
    {
        private string serialNumber;
        private string ticketType;
        private double price;
        private Trip ticketTrip;
        private int numberOfSeats;
        
        public Ticket(string SerialNumber, Trip TicketTrip, string TicketType, double Price, int NumberOfSeats)
        {
            this.SerialNumber = SerialNumber;
            this.TicketTrip = TicketTrip;
            this.TicketType = TicketType;
            this.Price = Price;
            this.NumberOfSeats = NumberOfSeats;
        }

        public string SerialNumber { get => serialNumber; set => serialNumber = value; }
        internal Trip TicketTrip { get => ticketTrip; set => ticketTrip = value; }
        public string TicketType { get => ticketType; set => ticketType = value; }
        public double Price { get => price; set => price = value; }
        public int NumberOfSeats { get => numberOfSeats; set => numberOfSeats = value; }
    }
}
