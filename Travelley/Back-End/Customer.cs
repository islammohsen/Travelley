using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;

namespace Travelley
{
    class Customer: Person
    {
        private List<Ticket> Tickets;
        private HashSet<Trip> Mark;
        private bool discount;


        public int NumberOfTrips { get => NumberOfTrips; }
        public bool Discount { get => Discount; }

        public Customer(string Id, string Name, string Nationality, List<string> Languages, string Gender, string Email, string PhoneNumber)
        {
            this.Id = Id;
            this.Name = Name;
            this.Nationality = Nationality;
            this.Languages = Languages;
            this.Gender = Gender;
            this.Email = Email;
            this.PhoneNumber = PhoneNumber;
            this.discount = false;
            this.Tickets = new List<Ticket>();
        }
            
        public void AddTicket(Ticket obj)
        {
            Tickets.Add(obj);
            Mark.Add(obj.TicketTrip);
            if (Mark.Count >= 2)
                discount = true;
            return;
        }

        public bool ReserveTicket(Trip obj, string Type)
        {
            return true;
        }

        public List<Ticket> GetTickets()
        {
            List<Ticket> Ret = new List<Ticket>();
            foreach(Ticket c in Tickets)
            {
                Ret.Add(c);
            }
            return Ret;
        }
    }
}
