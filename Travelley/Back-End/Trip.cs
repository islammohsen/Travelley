using System;
using System.Collections.Generic;
using Travelley.Back_End;

//ticket include type --> update price according to type.
//number of seats constant per trip (no specific number of seats per type).
//add trip status (closed or opened)
namespace Travelley
{
    public class Trip : IComparable<Trip>
    {
        public static List<Trip> Trips;
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
        private bool isClosed;

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
        public bool IsClosed { get => isClosed; set => isClosed = value; }

        public CustomImage TripImage;

        public void AddSeats(string Type, int Number, double Price)
        {
            NumberOfSeats[Type] = Number;
            PriceOfSeat[Type] = Price;
        }

        public Trip(string TripId, TourGuide Tour, string Depature, string Destination, Double Discount, DateTime Start, DateTime End, CustomImage TripImage, bool IsClosed)
        {
            tripId = TripId;
            tour = Tour;
            departure = Depature;
            destination = Destination;
            discount = Discount;
            start = Start;
            end = End;
            this.TripImage = TripImage;
            Tickets = new List<Ticket>();
            NumberOfSeats = new Dictionary<string, int>();
            PriceOfSeat = new Dictionary<string, double>();
            this.IsClosed = IsClosed;
        }

        public void AddTicket(Ticket obj)
        {
            Tickets.Add(obj);
            return;
        }

        public Ticket ReserveTicket(TripType tripType, string TicketType, int NumberOfOrderedSeats, int CustomerDiscount)
        {
            string serial = Guid.NewGuid().ToString();

            double TicketPrice = PriceOfSeat[TicketType] * NumberOfOrderedSeats * Math.Max(0, (100 - discount - CustomerDiscount) / 100);
            Ticket T = new Ticket(serial, this, TicketType, tripType, TicketPrice, NumberOfOrderedSeats);
            tickets.Add(T);
            return T;
        }

        public int GetNumberOfAvailableSeats()
        {
            int ret = 0;
            foreach (KeyValuePair<string, int> C in NumberOfSeats)
            {
                ret += C.Value;
            }
            return ret;
        }

        public void UpdateTripsStatus()
        {
            if (IsClosed == false && start <= DateTime.Today)
            {
                DataBase.UpdateTrip(this, new Trip(tripId, tour, departure, destination, discount, start, end, TripImage, true));
            }
        }

        public static int GetNumberOfOpenTrips()
        {
            int ret = 0;
            foreach (Trip T in Trips)
            {
                if (!T.isClosed)
                    ret++;
            }
            return ret;
        }

        public static Trip operator <(Trip obj1, Trip obj2)
        {
            if (!obj1.isClosed && obj2.isClosed)
            {
                return obj1;
            }
            else if (obj1.isClosed && !obj2.isClosed)
            {
                return obj2;
            }
            else
            {
                if (obj1.tripId.CompareTo(obj2) < 0)
                    return obj1;
                else
                    return obj2;
            }
        }
        public static Trip operator >(Trip obj1, Trip obj2)
        {
            if (!obj1.isClosed && obj2.isClosed)
            {
                return obj2;
            }
            else if (obj1.isClosed && !obj2.isClosed)
            {
                return obj1;
            }
            else
            {
                if (obj1.tripId.CompareTo(obj2) < 0)
                    return obj2;
                else
                    return obj1;
            }
        }

        public int CompareTo(Trip obj)
        {
            if (obj == null)
                return 1;
            if (!isClosed && (obj).isClosed)
            {
                return -1;
            }
            else if (isClosed && !(obj).isClosed)
            {
                return 1;
            }
            return tripId.CompareTo((obj).TripId);
        }
    }
}
