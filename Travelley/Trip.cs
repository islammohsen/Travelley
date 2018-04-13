using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Travelley
{
    class Trip
    {
        private DateTime Start;
        private DateTime End;
        private string Departure;
        private string Destination;
        private TourGuide Tour;
        private Double Discount;
        private string Type;
        private string Trip_ID;
        private Dictionary<string, int> NumberOfSeats;
        private Dictionary<string, double> PriceOfSeat;
        private List<Ticket> Tickets;

        public DateTime Start1 { get => Start; set => Start = value; }
        public DateTime End1 { get => End; set => End = value; }
        public string Departure1 { get => Departure; set => Departure = value; }
        public string Destination1 { get => Destination; set => Destination = value; }
        public double Discount1 { get => Discount; set => Discount = value; }
        public string Type1 { get => Type; set => Type = value; }
        public string Trip_ID1 { get => Trip_ID; set => Trip_ID = value; }
        internal TourGuide Tour1 { get => Tour; set => Tour = value; }

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

            Ticket T = new Ticket(Type, NumberOfOrderedSeats, serial, PriceOfSeat[Type] * NumberOfOrderedSeats, this);

            return T;
        }

    }
}
