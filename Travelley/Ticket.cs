using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Travelley
{
    class Ticket
    {
        private string ticketType;
        private int numberOfSeats;
        private string serialNumber;
        private double price;
        private Trip ticketTrip;
        
        public Ticket(string TicketType, int NumberOfSeats, string SerialNumber, double Price,Trip TicketTrip)
        {
            this.TicketType = TicketType;
            this.NumberOfSeats = NumberOfSeats;
            this.SerialNumber = SerialNumber;
            this.Price = Price;
            this.TicketTrip = TicketTrip;
        }

        public string TicketType { get => ticketType; set => ticketType = value; }
        public int NumberOfSeats { get => numberOfSeats; set => numberOfSeats = value; }
        public string SerialNumber { get => serialNumber; set => serialNumber = value; }
        public double Price { get => price; set => price = value; }
        internal Trip TicketTrip { get => ticketTrip; set => ticketTrip = value; }
    }
}
