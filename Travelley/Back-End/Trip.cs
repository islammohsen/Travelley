using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Windows.Controls;
using System.Windows.Media.Imaging;
using Travelley.Back_End;

//ticket include type --> update price according to type.
//number of seats constant per trip (no specific number of seats per type).
//add trip status (closed or opened)
namespace Travelley
{
    public class Trip
    {
        private string tripId;
        private TourGuide tour;
        private string departure;
        private string destination;
        private Double discount;
        private DateTime start;
        private DateTime end;
        private Dictionary<string, int> numberOfSeats;
        private Dictionary<string, double> priceOfSeat;
        private List<Ticket> tickets;

        public DateTime Start { get => start; set => start = value; }
        public DateTime End { get => end; set => end = value; }
        public string Departure { get => departure; set => departure = value; }
        public string Destination { get => destination; set => destination = value; }
        internal TourGuide Tour { get => tour; set => tour = value; }
        public double Discount { get => discount; set => discount = value; }
        public string TripId { get => tripId; set => tripId = value; }
        internal List<Ticket> Tickets { get => tickets; set => tickets = value; }
        public Dictionary<string, int> NumberOfSeats { get => numberOfSeats; set => numberOfSeats = value; }
        public Dictionary<string, double> PriceOfSeat { get => priceOfSeat; set => priceOfSeat = value; }
        public CustomImage TripImage;

        public void AddSeats(string Type, int Number, double Price)
        {
            NumberOfSeats[Type] = Number;
            PriceOfSeat[Type] = Price;
        }

        public Trip(string TripId, TourGuide Tour, string Depature, string Destination, Double Discount, DateTime Start, DateTime End)
        {
            this.tripId = TripId;
            this.tour = Tour;
            this.departure = Depature;
            this.destination = Destination;
            this.discount = Discount;
            this.start = Start;
            this.end = End;
            Tickets = new List<Ticket>();
            NumberOfSeats = new Dictionary<string, int>();
            PriceOfSeat = new Dictionary<string, double>();
        }

        public void AddTicket(Ticket obj)
        {
            Tickets.Add(obj);
            return;
        }

        public Ticket ReserveTicket(TripType tripType,  string TicketType, int NumberOfOrderedSeats)
        {
            string serial = Guid.NewGuid().ToString();

            double TicketPrice = PriceOfSeat[TicketType] * NumberOfOrderedSeats * (1.0 - discount);
            Ticket T = new Ticket(serial, this, TicketType, tripType, TicketPrice, NumberOfOrderedSeats);
            tickets.Add(T);
            return T;
        }

    }
}
