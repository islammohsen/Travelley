using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Travelley
{
    class Ticket
    {
        private string TicketType;
        private int NumberOfSeats;
        private string SerialNumber;
        private double Price;
        private Trip TicketTrip;
        
        public Ticket(string TicketType, int NumberOfSeats, string SerialNumber, double Price,Trip TicketTrip)
        {
            this.TicketType1 = TicketType;
            this.NumberOfSeats1 = NumberOfSeats;
            this.SerialNumber1 = SerialNumber;
            this.Price1 = Price;
            this.TicketTrip1 = TicketTrip;
        }
        
        public string TicketType1 { get => TicketType; set => TicketType = value; }
        public int NumberOfSeats1 { get => NumberOfSeats; set => NumberOfSeats = value; }
        public string SerialNumber1 { get => SerialNumber; set => SerialNumber = value; }
        public double Price1 { get => Price; set => Price = value; }
        internal Trip TicketTrip1 { get => TicketTrip; set => TicketTrip = value; }
    }
}
