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
        public static List<Customer> Customers;
        private List<Ticket> tickets;
        private HashSet<Trip> Mark;
        private bool discount;
        public int NumberOfTrips { get; }
        public bool Discount { get => discount; }
        public List<Ticket> Tickets { get => tickets; set => tickets = value; }

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
            Mark.Add(obj.CurrentTrip);
            if (Mark.Count >= 2)
                discount = true;
            else
                discount = false;
            return;

        }

        public Ticket ReserveTicket(Trip CurrentTrip, TripType tripType, string TicketType, int NumberOfOrderedSeats)
        {
            int CustomerDiscount = 0;
            if (discount)
                CustomerDiscount = 10;
            Ticket obj = CurrentTrip.ReserveTicket(tripType, TicketType, NumberOfOrderedSeats, CustomerDiscount);
            Tickets.Add(obj);
            Mark.Add(CurrentTrip);
            if (Mark.Count >= 2)
                discount = true;
            else
                discount = false;
            return obj;
        }

        public void UpdateTripMarking()
        {
            Mark.Clear();
            foreach(Ticket T in Tickets)
            {
                Mark.Add(T.CurrentTrip);
            }
            if (Mark.Count >= 2)
                discount = true;
            else
                discount = false;
            return;
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

        public override string ToString()
        {
            return Name;
        }
    }
}
