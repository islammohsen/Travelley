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

        public Ticket ReserveTicket(string Type)
        {
            return null;
        }
        
    }
}
