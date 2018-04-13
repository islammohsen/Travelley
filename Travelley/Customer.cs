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
        private int NumberOfTrips;
        private bool Discount;

        public int NumberOfTrips1 { get => NumberOfTrips; set => NumberOfTrips = value; }

        public Customer(string Name, string Id, string Gender, string Email, string PhoneNumber)
        {
            this.Name = Name;
            this.Id = Id;
            this.Gender = Gender;
            this.Email = Email;
            this.PhoneNumber = PhoneNumber;
            NumberOfTrips = 0;
            Tickets = new List<Ticket>();
            Discount = false;
        }
            
        public void AddTicket(Ticket obj)
        {
            Tickets.Add(obj);
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
