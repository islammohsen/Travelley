using System;
using System.Collections.Generic;
using Travelley.Back_End;

namespace Travelley
{
    /// <summary>
    /// 
    /// </summary>
    public class Customer : Person
    {
        //List containing all customers 
        public static List<Customer> Customers;
        //List contating tickets reserved by the customer
        private List<Ticket> tickets;
        public List<Ticket> Tickets { get => tickets; set => tickets = value; }
        //Set contains the trips reserved by the customer
        private HashSet<Trip> Trips;
        //discount if the customer finished 2 or more trips
        private bool discount;
        public bool Discount { get => discount; }
        //Number of finished trip by the customer
        public int numberOfTrips;
        public int NumberOfTrips { get { return GetNumberOfMadeTrips(); } }

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

        /// <summary>
        /// This function is called in the begining of the program when reading tickets from database
        /// intialize Tickets List
        /// </summary>
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

        /// <summary>
        /// Get The number of finished trip by the customer
        /// </summary>
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

        /// <summary>
        /// Function called when a customer tries to reserve a ticket
        /// The function calls reserveticket in class trip and the returned ticket is added to tickets list
        /// </summary>
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

        /// <summary>
        /// Function is called after removing tickets from database
        /// The function updates the customer Trips
        /// </summary>
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

        /// <summary>
        /// To sort a list of customers according to the number of made trips
        /// </summary>
        public override int CompareTo(object obj)
        {
            return -1 * numberOfTrips.CompareTo(((Customer)obj).numberOfTrips);
        }
    }
}
