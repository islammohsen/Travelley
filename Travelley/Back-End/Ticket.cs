using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Travelley.Back_End;

namespace Travelley
{
    /// <summary>
    /// Class representing the ticket
    /// </summary>
   public class Ticket
    {
        private string serialNumber; //Generated Id for the ticket
        private string ticketType;  //Ticket Type of the Ticket (Vip,Gold,Silver,...etc) (user added)
        private double price;      //Price of the ticket
        private Trip Trip;        //Trip concerned with the ticket 
        private int numberOfSeats; //Number of Seats reserved for the ticket
        private TripType tripType; //The type of the trip (Family,Friends,Lonely,...etc)

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
        internal Trip CurrentTrip { get => Trip; set => Trip = value; }
        public string TicketType { get => ticketType; set => ticketType = value; }
        public double Price { get => price; set => price = value; }
        public int NumberOfSeats { get => numberOfSeats; set => numberOfSeats = value; }
        public TripType TripType { get => tripType; set => tripType = value; }
    }
}
