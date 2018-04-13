using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;

namespace Travelley
{

    class DataBase
    {
        public List<Customer> Customers;
        public List<TourGuide> TourGuides;
        public List<Trip> Trips;

        public void GetCustomers()
        {
            Customers = new List<Customer>();

            //opened the connection
            SqlConnection Connection = new SqlConnection();
            Connection.Open();

            //created a command
            SqlCommand Command = new SqlCommand("select * from Customenrs", Connection);

            //excuted the command
            SqlDataReader Rdr = Command.ExecuteReader();

            //Reading Data
            while(Rdr.Read())
            {
                string Name = (string)Rdr["Name"];
                string Id = (string)Rdr["Id"];
                string Gender = (string)Rdr["Gender"];
                string Email = (string)Rdr["Email"];
                string PhoneNumber = (string)Rdr["PhoneNumber"];
                Customer obj = new Customer(Name, Id, Gender, Email, PhoneNumber);
                Customers.Add(obj);
            }
            Rdr.Close();
            Connection.Close();
            return;
        }

        public void GetTourGuides()
        {
            TourGuides = new List<TourGuide>();

            //opened the connection
            SqlConnection Connection = new SqlConnection();
            Connection.Open();

            //created a command
            SqlCommand Command = new SqlCommand("select * from TourGuide", Connection);

            //excuted the command
            SqlDataReader Rdr = Command.ExecuteReader();

            //Reading Data
            while (Rdr.Read())
            {
                string Name = (string)Rdr["Name"];
                string Id = (string)Rdr["Id"];
                string Gender = (string)Rdr["Gender"];
                string Email = (string)Rdr["Email"];
                string PhoneNumber = (string)Rdr["PhoneNumber"];
                TourGuide obj = new TourGuide(Name, Id, Gender, Email, PhoneNumber);
                TourGuides.Add(obj);
            }
            Rdr.Close();
            Connection.Close();
            return;

        }

        public void GetTrips()
        {
            Trips = new List<Trip>();

            //opened the connection
            SqlConnection Connection = new SqlConnection();
            Connection.Open();

            //created a command
            SqlCommand Command = new SqlCommand("select * from Trip", Connection);

            //excuted the command
            SqlDataReader Rdr = Command.ExecuteReader();

            //Reading Data
            while (Rdr.Read())
            {
                DateTime Start = (DateTime)Rdr["Start"];
                DateTime End = (DateTime)Rdr["End"];
                string Depature = (string)Rdr["Depature"];
                string Destination = (string)Rdr["Destination"];
                string TourGuideId = (string)Rdr["TourGuideId"];
                double Discount = (double)Rdr["Dicount"];
                string TripId = (string)Rdr["TripId"];
                string Type = (string)Rdr["Type"];

                TourGuide CurrentTourGuide = null;
                foreach(TourGuide c in TourGuides)
                {
                    if(c.Id == TourGuideId)
                    {
                        CurrentTourGuide = c;
                        break;
                    }
                }
                Trip obj = new Trip(Start, End, Depature, Destination, CurrentTourGuide, Discount, Type, TripId);
                Trips.Add(obj);
            }
            Rdr.Close();
            Connection.Close();
            return;
        }

        public void TripTickets()
        {
            //opened the connection
            SqlConnection Connection = new SqlConnection();
            Connection.Open();

            //created a command
            SqlCommand Command = new SqlCommand("select * from TripTickets", Connection);

            //excuted the command
            SqlDataReader Rdr = Command.ExecuteReader();

            while(Rdr.Read())
            {
                string TripId = (string)Rdr["TripId"];
                string Type = (string)Rdr["Type"];
                int NoOfSeats = (int)Rdr["NoOfSeats"];
                double Price = (double)Rdr["Price"];

                foreach(Trip c in Trips)
                {
                    if(c.Trip_ID == TripId)
                    {
                        c.AddSeats(Type, NoOfSeats, Price);
                        break;
                    }
                }
            }
            Rdr.Close();
            Connection.Close();
            return;
        }

        public void Transactions()
        {
            //opened the connection
            SqlConnection Connection = new SqlConnection();
            Connection.Open();

            //created a command
            SqlCommand Command = new SqlCommand("select * from Transactions", Connection);

            //excuted the command
            SqlDataReader Rdr = Command.ExecuteReader();

            while(Rdr.Read())
            {
                string CustomerId = (string)Rdr["CustomerId"];
                string TripId = (string)Rdr["TripId"];
                string TypeOfTicket = (string)Rdr["TypeOfTicket"];
                int NumberOfSeats = (int)Rdr["NumberOfSeats"];
                double Price = (double)Rdr["Price"];
                string SerialNumber = (string)Rdr["SerialNumber"];
                Customer CurrentCustomer = SelectCustomer(CustomerId);
                Trip CurrentTrip = SelectTrip(TripId);
                Ticket CurrentTicket = new Ticket(TypeOfTicket, NumberOfSeats, SerialNumber, Price, CurrentTrip);
                CurrentCustomer.AddTicket(CurrentTicket);

            }
            Rdr.Close();
            Connection.Close();
            return;
        }

        private Customer SelectCustomer(string Id)
        {
            foreach(Customer C in Customers)
            {
                if(C.Id == Id)
                {
                    return C;
                }
            }
            return null;
        }

        private Trip SelectTrip(string Id)
        {
            foreach (Trip C in Trips)
            {
                if (C.Trip_ID == Id)
                {
                    return C;
                }
            }
            return null;
        }

        private TourGuide SelectTourGuide(string Id)
        {
            foreach (TourGuide C in TourGuides)
            {
                
                if (C.Id == Id)
                {
                    return C;
                }
            }
            return null;
        }
    }
}
