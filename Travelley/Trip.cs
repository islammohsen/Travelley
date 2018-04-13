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
        private List<Ticket> Tickets;

        public Ticket ReserveTicket(string Type,int NumberOfOrderedSeats)
        {
            if (NumberOfSeats[Type] == 0 || NumberOfOrderedSeats>NumberOfSeats[Type])
                return null;

            Guid g = new Guid();
            string serial = g.ToString();
            NumberOfSeats[Type] -= NumberOfOrderedSeats;

            Ticket T = new Ticket(Type, NumberOfOrderedSeats, serial, PriceOfSeat[Type] * NumberOfSeats, this);

            return T;
        }
        
    }
}
