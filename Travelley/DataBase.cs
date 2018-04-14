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
            while (Rdr.Read())
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
                foreach (TourGuide c in TourGuides)
                {
                    if (c.Id == TourGuideId)
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

            while (Rdr.Read())
            {
                string TripId = (string)Rdr["TripId"];
                string Type = (string)Rdr["Type"];
                int NoOfSeats = (int)Rdr["NoOfSeats"];
                double Price = (double)Rdr["Price"];

                foreach (Trip c in Trips)
                {
                    if (c.Trip_ID == TripId)
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

            while (Rdr.Read())
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
                CurrentTrip.AddTicket(CurrentTicket);
            }
            Rdr.Close();
            Connection.Close();
            return;
        }

        public void UpdateCustomer(Customer CurrentCustomer, string Name, string Id, string Gender, string Email, string PhoneNumber)
        {
            //opened the connection
            SqlConnection Connection = new SqlConnection();
            Connection.Open();

            //update Name
            if (Name != "")
            {
                SqlCommand Command = new SqlCommand($"UPDATE Customer set Name = '{Name}' where Id = '{CurrentCustomer.Id}'", Connection);
                Command.ExecuteNonQuery();
                CurrentCustomer.Name = Name;
            }

            //update Gender
            if (Gender != "")
            {
                SqlCommand Command = new SqlCommand($"UPDATE Customer set Gender = '{Gender}' where Id = '{CurrentCustomer.Id}'", Connection);
                Command.ExecuteNonQuery();
                CurrentCustomer.Gender = Gender;
            }

            //update Email
            if (Email != "")
            {
                SqlCommand Command = new SqlCommand($"UPDATE Customer set Email = '{Email}' where Id = '{CurrentCustomer.Id}'", Connection);
                Command.ExecuteNonQuery();
                CurrentCustomer.Email = Email;
            }

            //update PhoneNumber
            if (PhoneNumber != "")
            {
                SqlCommand Command = new SqlCommand($"UPDATE Customer set PhoneNumber = '{PhoneNumber}' where Id = '{CurrentCustomer.Id}'", Connection);
                Command.ExecuteNonQuery();
                CurrentCustomer.PhoneNumber = PhoneNumber;
            }

            if (Id != "" && CheckUniqueCustomerId(Id))
            {
                SqlCommand Command = new SqlCommand($"UPDATE Transactions set Id = '{Id}' where Id = '{CurrentCustomer.Id}'", Connection);
                Command.ExecuteNonQuery();
                Command = new SqlCommand($"UPDATE Customer set Id = '{Id}' where Id = '{CurrentCustomer.Id}'", Connection);
                Command.ExecuteNonQuery();
                CurrentCustomer.Id = Id;
            }
            Connection.Close();
            return;
        }

        public void UpdateTourGuide(TourGuide CurrentTourGuide, string Name, string Id, string Gender, string Email, string PhoneNumber)
        {
            //opened the connection
            SqlConnection Connection = new SqlConnection();
            Connection.Open();

            //update Name
            if (Name != "")
            {
                SqlCommand Command = new SqlCommand($"UPDATE TourGuide set Name = '{Name}' where Id = '{CurrentTourGuide.Id}'", Connection);
                Command.ExecuteNonQuery();
                CurrentTourGuide.Name = Name;
            }

            //update Gender
            if (Gender != "")
            {
                SqlCommand Command = new SqlCommand($"UPDAtE TourGuide set Gender = '{Gender}' WHERE Id = '{CurrentTourGuide.Id}'", Connection);
                Command.ExecuteNonQuery();
                CurrentTourGuide.Gender = Gender;
            }

            //update Email
            if (Email != "")
            {
                SqlCommand Command = new SqlCommand($"UPDAtE TourGuide set Email = '{Email}' WHERE Id = '{CurrentTourGuide.Id}'", Connection);
                Command.ExecuteNonQuery();
                CurrentTourGuide.Email = Email;
            }

            //update PhoneNumber
            if (PhoneNumber != "")
            {
                SqlCommand Command = new SqlCommand($"UPDAtE TourGuide set PhoneNumber = '{PhoneNumber}' WHERE Id = '{CurrentTourGuide.Id}'", Connection);
                Command.ExecuteNonQuery();
                CurrentTourGuide.PhoneNumber = PhoneNumber;
            }

            if (Id != "" && CheckUniqueTourGuideId(Id))
            {
                SqlCommand Command = new SqlCommand($"UPDATE Trip set TourGuideId = '{Id}' WHERE TourGuideId = '{CurrentTourGuide.Id}'", Connection);
                Command.ExecuteNonQuery();
                Command = new SqlCommand($"UPDATE TourGuide set Id = '{Id}' Where Id = '{CurrentTourGuide.Id}'", Connection);
                Command.ExecuteNonQuery();
                CurrentTourGuide.Id = Id;
            }
            Connection.Close();
            return;
        }
        
        public void InsertCustomer(Customer CurrentCustomer)
        {
            SqlConnection Connection = new SqlConnection();
            Connection.Open();

            SqlCommand Command = new SqlCommand($"INSERT INTO Customer values('{ CurrentCustomer.Name }','{CurrentCustomer.Id }' , '{CurrentCustomer.Gender}' , '{CurrentCustomer.Email}' ,'{ CurrentCustomer.PhoneNumber}' )", Connection);
            Command.ExecuteNonQuery();

            Connection.Close();
            return;
        }

        public void InsertTourGuide(TourGuide CurrentTourGuide)
        {
            SqlConnection Connection = new SqlConnection();
            Connection.Open();

            SqlCommand Command = new SqlCommand($"Insert InTo TourGuide Values ( '{CurrentTourGuide.Name}'  , '{CurrentTourGuide.Id},'{CurrentTourGuide.Gender}'  ,  '{ CurrentTourGuide.Email}'  ,  '{ CurrentTourGuide.PhoneNumber}' )", Connection);
            Command.ExecuteNonQuery();

            Connection.Close();
            return;
        }

        public void InsertTrip(Trip CurrentTrip)
        {
            SqlConnection Connection = new SqlConnection();
            Connection.Open();

            SqlCommand Command = new SqlCommand($"Insert InTo Trip Values ( '{CurrentTrip.Start.ToString()}'  ,  '{ CurrentTrip.End.ToString() }' ,'{ CurrentTrip.Departure }' , '{CurrentTrip.Destination}','{ CurrentTrip.Tour.Id } ' , {CurrentTrip.Discount} ,'{CurrentTrip.Trip_ID}' ,  '{CurrentTrip.Type}' )", Connection);
            Command.ExecuteNonQuery();

            Connection.Close();
            return;
        }

        public void InsertTripTickets(string Trip_Id,string Type,int NumbrOfSeats,double Price)
        {
            SqlConnection Connection = new SqlConnection();
            Connection.Open();
            
            SqlCommand Command = new SqlCommand($"INSERT INTO TripTickets values( '{Trip_Id}', '{Type}', {NumbrOfSeats}, {Price} )", Connection);
            Command.ExecuteNonQuery();

            Connection.Close();
            return;
        }

        public void InsertTransactions(string Trip_Id, string Customer_Id, string TypeOfTickets, int NumbrOfSeats, double Price,string SerialNumber)
        {
            SqlConnection Connection = new SqlConnection();
            Connection.Open();

            SqlCommand Command = new SqlCommand($"INSERT INTO Transactions values( '{Trip_Id}','{Customer_Id}', '{TypeOfTickets}', {NumbrOfSeats}, {Price},{SerialNumber} )", Connection);
            Command.ExecuteNonQuery();

            Connection.Close();
            return;
        }

        private Customer SelectCustomer(string Id)
        {
            foreach (Customer C in Customers)
            {
                if (C.Id == Id)
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

        //Return True if the given Id is unique
        private bool CheckUniqueCustomerId(string Id)
        {
            foreach (Customer C in Customers)
            {
                if (C.Id == Id)
                    return false;
            }
            return true;
        }

        private bool CheckUniqueTourGuideId(string id)
        {

            foreach(TourGuide T in TourGuides)
            {
                if (T.Id == id)

                    return false;

            }
            return true;
        }
    }
}
