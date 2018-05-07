using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using Travelley.Back_End;
using System.IO;

namespace Travelley
{

    static class DataBase
    {
        private static SqlConnection Connection;
        private static SqlCommand Command = new SqlCommand();
        private static SqlDataReader Reader;
        private static bool IsIntialized = false;
        
        private static string GetPath()
        {
            string path = Directory.GetCurrentDirectory();
            char[] c = { '\\', '\\' };
            string[] paths = path.Split(c);
            path = "";
            for (int i = 0; i < paths.Length - 2; i++)
                path += paths[i] + "\\";
            path += "TravelleyData.mdf";
            return path;
        }

        public static void Intialize()
        {
            if (!IsIntialized)
            {
                string path = GetPath();
                Connection = new SqlConnection($"Data Source=(LocalDB)\\MSSQLLocalDB;AttachDbFilename={path};Integrated Security=True");
                Connection.Open();
                Command.Connection = Connection;
                IsIntialized = true;
            }
            GetCustomers();
            GetTourGuides();
            GetTrips();
            TripsTickets();
            Transactions();
        }

        public static void ShutDown()
        {
            if (IsIntialized)
            {
                foreach (Trip T in Trip.Trips)
                    T.UpdateTripsStatus();
                Connection.Close();
                IsIntialized = false;
            }
        }

        private static void GetCustomers()
        {
            Customer.Customers = new List<Customer>();


            //created a command
            Command.CommandText = "Select * From Customer";

            //excuted the command
            Reader = Command.ExecuteReader();

            //Reading Data
            while (Reader.Read())
            {
                string Id = (string)Reader["Id"];
                string Name = (string)Reader["Name"];
                string Nationality = (string)Reader["Nationality"];
                string Language = (string)Reader["Language"];
                string Gender = (string)Reader["Gender"];
                string Email = (string)Reader["Email"];
                string PhoneNumber = (string)Reader["PhoneNumber"];
                byte[] CustomerImage = (byte[])Reader["Image"];
                Customer Obj = new Customer(Id, Name, Nationality, Language, Gender, Email, PhoneNumber, new CustomImage(CustomerImage));
                Customer.Customers.Add(Obj);
            }
            Reader.Close();
            return;
        }

        private static void GetTourGuides()
        {
            TourGuide.TourGuides = new List<TourGuide>();

            //created a command
            Command.CommandText = "SELECT * from TourGuide";

            Reader = Command.ExecuteReader();

            //Reading Data
            while (Reader.Read())
            {
                string Id = (string)Reader["Id"];
                string Name = (string)Reader["Name"];
                string Nationality = (string)Reader["Nationality"];
                string Language = (string)Reader["Language"];
                string Gender = (string)Reader["Gender"];
                string Email = (string)Reader["Email"];
                string PhoneNumber = (string)Reader["PhoneNumber"];
                byte[] TourGuideImage = (byte[])Reader["Image"];
                TourGuide Obj = new TourGuide(Id, Name, Nationality, Language, Gender, Email, PhoneNumber, new CustomImage(TourGuideImage));
                TourGuide.TourGuides.Add(Obj);
            }
            Reader.Close();
            return;
        }

        private static void GetTrips()
        {
            Trip.Trips = new List<Trip>();

            //created a command
            Command.CommandText = "SELECT * FROM Trip";

            //excuted the command
            Reader = Command.ExecuteReader();

            //Reading Data
            while (Reader.Read())
            {
                String TripId = (string)Reader["TripId"];
                String TourGuideId = (string)Reader["TourGuideId"];
                String Depature = (string)Reader["Depature"];
                String Destination = (string)Reader["Destination"];
                Double Discount = (Double)Reader["Discount"];
                DateTime Start = (DateTime)Reader["TripStartDate"];
                DateTime End = (DateTime)Reader["TripEndDate"];
                Byte[] TripImage = (Byte[])Reader["Image"];
                bool IsClosed = (bool)Reader["IsClosed"];
                TourGuide CurrentTourGuide = SelectTourGuide(TourGuideId);
                if (CurrentTourGuide == null)
                    continue;
                Trip Obj = new Trip(TripId, CurrentTourGuide, Depature, Destination, Discount, Start, End, new CustomImage(TripImage), IsClosed);
                Trip.Trips.Add(Obj);
                CurrentTourGuide.Trips.Add(Obj);
            }
            Reader.Close();
            return;
        }

        private static void TripsTickets()
        {

            //created a command
            Command.CommandText = "select * from TripsTickets";

            //excuted the command
            Reader = Command.ExecuteReader();

            while (Reader.Read())
            {
                string TripId = (string)Reader["TripId"];
                string Type = (string)Reader["Type"];
                int NumberOfSeats = (int)Reader["NumberOfSeats"];
                double Price = (double)Reader["Price"];

                foreach (Trip C in Trip.Trips)
                {
                    if (C.TripId == TripId)
                    {
                        C.AddSeats(Type, NumberOfSeats, Price);
                        break;
                    }
                }
            }
            Reader.Close();
            return;
        }

        private static void Transactions()
        {

            //created a command
            Command.CommandText = "select * from Transactions";

            Reader = Command.ExecuteReader();

            while (Reader.Read())
            {
                string SerialNumber = (string)Reader["SerialNumber"];
                string CustomerId = (string)Reader["CustomerId"];
                string TripId = (string)Reader["TripId"];
                string TypeOfTicket = (string)Reader["TypeOfTicket"];
                string TypeOfTrip = (string)Reader["TypeOfTrip"];
                double Price = (double)Reader["Price"];
                int NumberOfSeats = (int)Reader["NumberOfSeats"];
                Customer CurrentCustomer = SelectCustomer(CustomerId);
                Trip CurrentTrip = SelectTrip(TripId);
                if (CurrentCustomer == null || CurrentTrip == null)
                    continue;

                //gets typeoftrip
                TripType tripType = null;
                if (TypeOfTrip == "Family")
                    tripType = new Family();
                else if (TypeOfTrip == "Couple")
                    tripType = new Couple();
                else if (TypeOfTrip == "General")
                    tripType = new General();
                else if (TypeOfTrip == "Lonely")
                    tripType = new Lonely();
                else if (TypeOfTrip == "Friends")
                    tripType = new Friends();


                Ticket CurrentTicket = new Ticket(SerialNumber, CurrentTrip, TypeOfTicket, tripType, Price, NumberOfSeats);
                CurrentCustomer.AddTicket(CurrentTicket);
                CurrentTrip.AddTicket(CurrentTicket);
            }
            Reader.Close();
            return;
        }

        public static void UpdateCustomer(Customer CurrentCustomer, Customer NewCustomer)
        {
            //update database
            Command.CommandText = $"UPDATE Customer set Id = '{NewCustomer.Id}', Name = '{NewCustomer.Name}', Nationality = '{NewCustomer.Nationality}', " +
                $"Language = '{NewCustomer.Language}', Gender = '{NewCustomer.Gender}', Email = '{NewCustomer.Email}', PhoneNumber = '{NewCustomer.PhoneNumber}'," +
                $"Image = @image where Id = '{CurrentCustomer.Id}'";
            Command.Parameters.AddWithValue("@image", NewCustomer.UserImage.GetByteImage());
            Command.ExecuteNonQuery();
            Command.Parameters.Clear();

            //update object
            CurrentCustomer.Id = NewCustomer.Id;
            CurrentCustomer.Name = NewCustomer.Name;
            CurrentCustomer.Nationality = NewCustomer.Nationality;
            CurrentCustomer.Language = NewCustomer.Language;
            CurrentCustomer.Gender = NewCustomer.Gender;
            CurrentCustomer.Email = NewCustomer.Email;
            CurrentCustomer.PhoneNumber = NewCustomer.PhoneNumber;
            CurrentCustomer.UserImage = NewCustomer.UserImage;
        }

        public static void UpdateTourGuide(TourGuide CurrentTourGuide, TourGuide NewTourGuide)
        {
            //update databae
            Command.CommandText = $"UPDATE TourGuide set Id = '{NewTourGuide.Id}', Name = '{NewTourGuide.Name}', Nationality = '{NewTourGuide.Nationality}', " +
                $"Language = '{NewTourGuide.Language}', Gender = '{NewTourGuide.Gender}', Email = '{NewTourGuide.Email}', PhoneNumber = '{NewTourGuide.PhoneNumber}', " +
                $"Image = @image where Id = '{CurrentTourGuide.Id}'";
            Command.Parameters.AddWithValue("@image", NewTourGuide.UserImage.GetByteImage());
            Command.ExecuteNonQuery();
            Command.Parameters.Clear();

            //update object
            CurrentTourGuide.Id = NewTourGuide.Id;
            CurrentTourGuide.Name = NewTourGuide.Name;
            CurrentTourGuide.Nationality = NewTourGuide.Nationality;
            CurrentTourGuide.Language = NewTourGuide.Language;
            CurrentTourGuide.Gender = NewTourGuide.Gender;
            CurrentTourGuide.Email = NewTourGuide.Email;
            CurrentTourGuide.PhoneNumber = NewTourGuide.PhoneNumber;
            CurrentTourGuide.UserImage = NewTourGuide.UserImage;
        }

        public static void UpdateTrip(Trip CurrentTrip, Trip NewTrip)
        {
            //update database
            //update Trip table
           Command.CommandText = $"UPDATE Trip set TripId = '{NewTrip.TripId}', TourGuideId = '{NewTrip.Tour.Id}', Depature = '{NewTrip.Departure}', " +
                $"Destination = '{NewTrip.Destination}', Discount = {NewTrip.Discount} , TripEndDate = '{NewTrip.End.ToString()}' , " +
                $" TripStartDate = '{NewTrip.Start.ToString()}' , Image = @image, IsClosed = '{NewTrip.IsClosed}' where TripId = '{CurrentTrip.TripId}'";
            Command.Parameters.AddWithValue("@image", NewTrip.TripImage.GetByteImage());
            Command.ExecuteNonQuery();
            Command.Parameters.Clear();

            //update TripsTickets Table
            Command.CommandText = $"UPDATE TripsTickets set TripId = '{NewTrip.TripId}' where TripId = '{CurrentTrip.TripId}'";
            Command.ExecuteNonQuery();

            //update Transactions Table
            Command.CommandText = $"UPDATE Transactions set TripId = '{NewTrip.TripId}' where TripId = '{CurrentTrip.TripId}'";
            Command.ExecuteNonQuery();

            //update objects
            CurrentTrip.TripId = NewTrip.TripId;

            //remove the trip from the previous TourGuide and add it to the new selected tourguide 
            CurrentTrip.Tour.Trips.Remove(CurrentTrip);
            CurrentTrip.Tour = NewTrip.Tour;
            CurrentTrip.Tour.Trips.Add(CurrentTrip);
            
            CurrentTrip.Departure = NewTrip.Departure;
            CurrentTrip.Destination = NewTrip.Destination;
            CurrentTrip.Discount = NewTrip.Discount;
            CurrentTrip.Start = NewTrip.Start;
            CurrentTrip.End = NewTrip.End;
            CurrentTrip.TripImage = NewTrip.TripImage;
        }

        public static void UpdateTripsTickets(Trip CurrentTrip, string PrevType, string NewType, int NumberOfSeats, double Price)
        {
            Command.CommandText = $"UPDATE TripsTickets set Type = '{NewType}', NumberOfSeats = {NumberOfSeats}, Price = {Price} where TripId = '{CurrentTrip.TripId}' And Type = '{PrevType}'";
            Command.ExecuteNonQuery();
            Command.CommandText = $"UPDATE Transactions set TypeOfTicket = '{NewType}' where TripId = '{CurrentTrip.TripId}' And TypeOfTicket = '{PrevType}'";
            Command.ExecuteNonQuery();
            UpdateTickets(CurrentTrip, PrevType, NewType);
            CurrentTrip.NumberOfSeats.Remove(PrevType);
            CurrentTrip.PriceOfSeat.Remove(PrevType);
            CurrentTrip.NumberOfSeats.Add(NewType, NumberOfSeats);
            CurrentTrip.PriceOfSeat.Add(NewType, Price);
        }

        private static void UpdateTickets(Trip CurrentTrip, string PrevType, string NewType)
        {
            foreach(Ticket T in CurrentTrip.Tickets)
            {
                if (T.TicketType == PrevType)
                    T.TicketType = NewType;
            }
        }

        public static void InsertCustomer(Customer CurrentCustomer)
        {
            Command.CommandText = $"INSERT INTO Customer values('{ CurrentCustomer.Id }','{CurrentCustomer.Name }' ," +
                $" '{CurrentCustomer.Nationality}' , '{CurrentCustomer.Language}' ,'{ CurrentCustomer.Gender}','{CurrentCustomer.Email}'," +
                $"'{CurrentCustomer.PhoneNumber}', @image );";
            Command.Parameters.AddWithValue("@image", CurrentCustomer.UserImage.GetByteImage());
            Command.ExecuteNonQuery();
            Command.Parameters.Clear();
            Customer.Customers.Add(CurrentCustomer);
            return;
        }

        public static void InsertTourGuide(TourGuide CurrentTourGuide)
        {
            Command.CommandText = $"INSERT INTO TourGuide values('{ CurrentTourGuide.Id }','{CurrentTourGuide.Name }' ," +
                $" '{CurrentTourGuide.Nationality}', '{CurrentTourGuide.Language}', '{ CurrentTourGuide.Gender}','{CurrentTourGuide.Email}'," +
                $"'{CurrentTourGuide.PhoneNumber}', @image)";
            Command.Parameters.AddWithValue("@image", CurrentTourGuide.UserImage.GetByteImage());
            Command.ExecuteNonQuery();
            Command.Parameters.Clear();
            TourGuide.TourGuides.Add(CurrentTourGuide);
            return;
        }

        public static void InsertTrip(Trip CurrentTrip)
        {
            Command.CommandText = $"INSERT INTO Trip values('{CurrentTrip.TripId}', '{CurrentTrip.Tour.Id}', '{CurrentTrip.Departure}', " +
                $"'{CurrentTrip.Destination}', {CurrentTrip.Discount}, '{CurrentTrip.Start.ToString()}', '{CurrentTrip.End.ToString()}'," +
                $"@image, '{CurrentTrip.IsClosed}')";
            Command.Parameters.AddWithValue("@image", CurrentTrip.TripImage.GetByteImage());
            Command.ExecuteNonQuery();
            Command.Parameters.Clear();
            Trip.Trips.Add(CurrentTrip);
            CurrentTrip.Tour.Trips.Add(CurrentTrip);
            return;
        }

        public static void InsertTripTickets(string TripId, string Type, int NumbrOfSeats, double Price)
        {
            Command.CommandText = $"INSERT INTO TripsTickets values( '{TripId}', '{Type}', {NumbrOfSeats}, {Price} )";
            Command.ExecuteNonQuery();
            Trip T = SelectTrip(TripId);
            T.NumberOfSeats.Add(Type, NumbrOfSeats);
            T.PriceOfSeat.Add(Type, Price);
            return;
        }

        public static void InsertTransactions(string SerialNumber, string CustomerId, string TripId, string TypeOfTicket, string TypeOfTrip, double Price, int NumberOfSeats)
        {
            Command.CommandText = $"INSERT INTO Transactions values( '{SerialNumber}', '{CustomerId}', '{TripId}'," +
                $" '{TypeOfTicket}', '{TypeOfTrip}', {Price}, {NumberOfSeats} )";
            Command.ExecuteNonQuery();
            Trip t = SelectTrip(TripId);
            UpdateTripsTickets(t, TypeOfTicket, TypeOfTicket, t.NumberOfSeats[TypeOfTicket] - NumberOfSeats, t.PriceOfSeat[TypeOfTicket]);
            return;
        }

        public static void DeleteCustomer(Customer CurrnetCustomer)
        {
            while (CurrnetCustomer.Tickets.Count != 0)
            {
                DeleteTicket(CurrnetCustomer.Tickets[0]);
            }
            Command.CommandText = $"Delete From Customer where Id = '{CurrnetCustomer.Id}'";
            Command.ExecuteNonQuery();

            Customer.Customers.Remove(CurrnetCustomer);
        }

        public static void DeleteTourGuide(TourGuide CurrentTourGuide)
        {
            Command.CommandText = $"Delete From TourGuide where Id = '{CurrentTourGuide.Id}'";
            Command.ExecuteNonQuery();
            TourGuide.TourGuides.Remove(CurrentTourGuide);
        }

        public static void DeleteTrip(Trip CurrentTrip)
        {
            //Delete TripsTickets of the Current Trip
            Command.CommandText = $"Delete From TripsTickets where TripId = '{CurrentTrip.TripId}'";
            Command.ExecuteNonQuery();

            //Delete the tickets of the current trip
            while(CurrentTrip.Tickets.Count != 0)
            {
                DeleteTicket(CurrentTrip.Tickets[0]);
            }

            //delete the trip
            Command.CommandText = $"Delete From Trip where TripId = '{CurrentTrip.TripId}'";
            Command.ExecuteNonQuery();

            Trip.Trips.Remove(CurrentTrip);
            CurrentTrip.Tour.Trips.Remove(CurrentTrip);
        }

        public static void DeleteTicket(Ticket t)
        {
            Command.CommandText = $"Select * From Transactions where serialnumber = '{t.SerialNumber}'";
            Reader = Command.ExecuteReader();
            Reader.Read();
            Trip T = SelectTrip((string)Reader["TripId"]);
            Customer Cus = SelectCustomer((string)Reader["CustomerId"]);
            string TicketType = (string)Reader["TypeOfTicket"];
            int NumberOfSeats = (int)Reader["NumberOfSeats"];
            T.Tickets.Remove(t);
            Cus.Tickets.Remove(t);
            Cus.UpdateTripMarking();
            Reader.Close();
            if (Cus.Tickets.Count == 0)
                DeleteCustomer(Cus);
            UpdateTripsTickets(T, TicketType, TicketType, T.NumberOfSeats[TicketType] + NumberOfSeats, T.PriceOfSeat[TicketType]);
            Command.CommandText = $"Delete From Transactions where serialnumber = '{t.SerialNumber}'";
            Command.ExecuteNonQuery();
        }

        //select with id
        public static Customer SelectCustomer(string Id)
        {
            foreach (Customer C in Customer.Customers)
            {
                if (C.Id == Id)
                {
                    return C;
                }
            }
            return null;
        }

        public static Trip SelectTrip(string Id)
        {
            foreach (Trip C in Trip.Trips)
            {
                if (C.TripId == Id)
                {
                    return C;
                }
            }
            return null;
        }

        public static TourGuide SelectTourGuide(string Id)
        {
            foreach (TourGuide C in TourGuide.TourGuides)
            {
                if (C.Id == Id)
                {
                    return C;
                }
            }
            return null;
        }

        //Return True if the given Id is unique
        public static bool CheckUniqueCustomerId(string Id)
        {
            foreach (Customer C in Customer.Customers)
            {
                if (C.Id == Id)
                    return false;
            }
            return true;
        }

        public static bool CheckUniqueTourGuideId(string Id)
        {

            foreach (TourGuide T in TourGuide.TourGuides)
            {
                if (T.Id == Id)
                    return false;

            }
            return true;
        }

        public static bool CheckUniqueTripId(string Id)
        {
            foreach (Trip T in Trip.Trips)
            {
                if (T.TripId == Id)
                    return false;
            }
            return true;
        }

        public static int GetNumberOfAvailableTourGuides()
        {
            int ret = 0;
            foreach(TourGuide T in TourGuide.TourGuides)
            {
                if (T.CheckAvailability(DateTime.Today, DateTime.Today))
                    ret++;
            }
            return ret;
        }
    }
}
