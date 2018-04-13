using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Travelley
{
    class Trip
    {
        private DateTime start;
        private DateTime end;
        private string departure;
        private string destination;
        private TourGuide tour;
        private Double discount;
        private string type;
        private string trip_ID;
        private Dictionary<string, int> numberOfSeats;
        private Dictionary<string, double> priceOfSeat;
        private List<Ticket> tickets;

        public DateTime Start { get => start; set => start = value; }
        public DateTime End { get => end; set => end = value; }
        public string Departure { get => departure; set => departure = value; }
        public string Destination { get => destination; set => destination = value; }
        internal TourGuide Tour { get => tour; set => tour = value; }
        public double Discount { get => discount; set => discount = value; }
        public string Type { get => type; set => type = value; }
        public string Trip_ID { get => trip_ID; set => trip_ID = value; }
        public Dictionary<string, int> NumberOfSeats { get => numberOfSeats; set => numberOfSeats = value; }
        public Dictionary<string, double> PriceOfSeat { get => priceOfSeat; set => priceOfSeat = value; }
        internal List<Ticket> Tickets { get => tickets; set => tickets = value; }

        public void AddSeats(string Type, int Number, double Price)
        {
            NumberOfSeats[Type] = Number;
            PriceOfSeat[Type] = Price;
        }

        public Trip(DateTime Start, DateTime End, string Departure, string Destination, TourGuide Tour, Double Discount, string Type, string Trip_ID)
        {
            this.Start = Start;
            this.End = End;
            this.Departure = Departure;
            this.Destination = Destination;
            this.Tour = Tour;
            this.Discount = Discount;
            this.Type = Type;
            this.Trip_ID = Trip_ID;
            NumberOfSeats = new Dictionary<string, int>();
            PriceOfSeat = new Dictionary<string, double>();
            Tickets = new List<Ticket>();
        }

        public void AddTicket(Ticket obj)
        {
            Tickets.Add(obj);
            return;
        }

        public Ticket ReserveTicket(string Type, int NumberOfOrderedSeats)
        {
            if (NumberOfSeats[Type] == 0 || NumberOfOrderedSeats > NumberOfSeats[Type])
                return null;

            Guid g = new Guid();
            string serial = g.ToString();
            NumberOfSeats[Type] -= NumberOfOrderedSeats;

            double TicketPrice = PriceOfSeat[Type] * NumberOfOrderedSeats * (1.0 - discount);
            Ticket T = new Ticket(Type, NumberOfOrderedSeats, serial,  TicketPrice, this);

            return T;
        }

    }
}
