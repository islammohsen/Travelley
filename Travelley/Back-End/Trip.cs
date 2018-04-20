using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Windows.Controls;
using System.Windows.Media.Imaging;

namespace Travelley
{
    class Trip
    {
        private string tripId;
        private TourGuide tour;
        private string type;
        private string departure;
        private string destination;
        private Double discount;
        private DateTime start;
        private DateTime end;
        private Dictionary<string, int> numberOfSeats;
        private Dictionary<string, double> priceOfSeat;
        private List<Ticket> tickets;
        private byte[] tripImage;

        public DateTime Start { get => start; set => start = value; }
        public DateTime End { get => end; set => end = value; }
        public string Departure { get => departure; set => departure = value; }
        public string Destination { get => destination; set => destination = value; }
        internal TourGuide Tour { get => tour; set => tour = value; }
        public double Discount { get => discount; set => discount = value; }
        public string Type { get => type; set => type = value; }
        public string TripId { get => TripId; set => TripId = value; }
        internal List<Ticket> Tickets { get => tickets; set => tickets = value; }
        public Dictionary<string, int> NumberOfSeats { get => numberOfSeats; set => numberOfSeats = value; }
        public Dictionary<string, double> PriceOfSeat { get => priceOfSeat; set => priceOfSeat = value; }
        public byte[] TripImage { get => tripImage; set => tripImage = value; }
        
        public void AddSeats(string Type, int Number, double Price)
        {
            NumberOfSeats[Type] = Number;
            PriceOfSeat[Type] = Price;
        }

        public Trip(string TripId, TourGuide Tour, string Type, string Depature, string Destination, Double Discount, DateTime Start, DateTime End, byte[] TripImage)
        {
            this.tripId = TripId;
            this.tour = Tour;
            this.type = Type;
            this.departure = Depature;
            this.destination = Destination;
            this.discount = Discount;
            this.start = Start;
            this.end = End;
            this.TripImage = TripImage;
            Tickets = new List<Ticket>();
            NumberOfSeats = new Dictionary<string, int>();
            PriceOfSeat = new Dictionary<string, double>();
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

            
            string serial = Guid.NewGuid().ToString();
            NumberOfSeats[Type] -= NumberOfOrderedSeats;

            double TicketPrice = PriceOfSeat[Type] * NumberOfOrderedSeats * (1.0 - discount);
            Ticket T = new Ticket(serial, this, Type, TicketPrice, NumberOfOrderedSeats);
            return T;
        }

        public Image GetImage()
        {
            BitmapImage bi = new BitmapImage();
            bi.BeginInit();
            bi.CreateOptions = BitmapCreateOptions.None;
            bi.CacheOption = BitmapCacheOption.Default;
            bi.StreamSource = new MemoryStream(tripImage);
            bi.EndInit();

            Image img = new Image();  //Image control of wpf

            img.Source = bi;
            return img;
        }

    }
}
