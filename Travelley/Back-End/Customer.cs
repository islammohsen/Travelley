using System;
using System.Collections.Generic;
using Travelley.Back_End;

namespace Travelley
{
    
    public class Customer : Person
    {
        public static List<Customer> Customers;
        private List<Ticket> tickets;
        private HashSet<Trip> Trips;
        private bool discount;
        public int numberOfTrips;
        public int NumberOfTrips { get { return GetNumberOfMadeTrips(); } }
        public bool Discount { get => discount; }
        public List<Ticket> Tickets { get => tickets; set => tickets = value; }

        public Customer(string Id, string Name, string Nationality, string Language, string Gender, string Email, string PhoneNumber, CustomImage UserImage)
        {
            this.Id = Id;
            this.Name = Name;
            this.Nationality = Nationality;
            this.Language = Language;
            this.Gender = Gender;
            this.Email = Email;
            this.PhoneNumber = PhoneNumber;
            this.UserImage = UserImage;
            discount = false;
            Tickets = new List<Ticket>();
            Trips = new HashSet<Trip>();
        }

        public void AddTicket(Ticket obj)
        {
            Tickets.Add(obj);
            Trips.Add(obj.CurrentTrip);
            if (Trips.Count >= 2)
                discount = true;
            else
                discount = false;
            return;

        }

        private int GetNumberOfMadeTrips()
        {
            int Ret = 0;
            foreach(Trip T in Trips)
            {
                if (T.Start <= DateTime.Today)
                {
                    Ret++;
                }
            }
            numberOfTrips = Ret;
            return numberOfTrips;
        }

        public Ticket ReserveTicket(Trip CurrentTrip, TripType tripType, string TicketType, int NumberOfOrderedSeats)
        {
            int CustomerDiscount = 0;
            if (discount)
                CustomerDiscount = 10;
            Ticket obj = CurrentTrip.ReserveTicket(tripType, TicketType, NumberOfOrderedSeats, CustomerDiscount);
            Tickets.Add(obj);
            Trips.Add(CurrentTrip);
            if (Trips.Count >= 2)
                discount = true;
            else
                discount = false;
            return obj;
        }

        public void UpdateTripMarking()
        {
            Trips.Clear();
            foreach(Ticket T in Tickets)
            {
                Trips.Add(T.CurrentTrip);
            }
            if (Trips.Count >= 2)
                discount = true;
            else
                discount = false;
            return;
        }

        public override string ToString()
        {
            return Name;
        }
    }
}
