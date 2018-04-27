using System;
using System.Drawing;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Media;
using System.IO;
using System.Windows.Controls;
using System.Windows.Media.Imaging;
using Travelley.Back_End;

namespace Travelley
{
    
    public class Customer : Person
    {
        private List<Ticket> Tickets;
        private HashSet<Trip> Mark;
        private bool discount;
        public int NumberOfTrips { get => NumberOfTrips; }
        public bool Discount { get => Discount; }

        public Customer(string Id, string Name, string Nationality, string Language, string Gender, string Email, string PhoneNumber)
        {
            this.Id = Id;
            this.Name = Name;
            this.Nationality = Nationality;
            this.Language = Language;
            this.Gender = Gender;
            this.Email = Email;
            this.PhoneNumber = PhoneNumber;
            discount = false;
            Tickets = new List<Ticket>();
            Mark = new HashSet<Trip>();
        }

        public void AddTicket(Ticket obj)
        {
            Tickets.Add(obj);
            Mark.Add(obj.TicketTrip);
            if (Mark.Count >= 2)
                discount = true;
            return;

        }

        public void ReserveTicket(Trip CurrentTrip, TripType tripType, string TicketType, int NumberOfOrderedSeats)
        {
            Ticket obj = CurrentTrip.ReserveTicket(tripType, TicketType, NumberOfOrderedSeats);
            Tickets.Add(obj);
        }

        public List<Ticket> GetTickets()
        {
            List<Ticket> Ret = new List<Ticket>();
            foreach (Ticket c in Tickets)
            {
                Ret.Add(c);
            }
            return Ret;
        }
    }
}
