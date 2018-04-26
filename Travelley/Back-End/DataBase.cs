using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;
using Travelley.Back_End;
using System.IO;

namespace Travelley
{

    static class DataBase
    {
        public static List<Customer> Customers;
        public static List<TourGuide> TourGuides;
        public static List<Trip> Trips;
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
                Connection.Close();
                IsIntialized = false;
            }
        }

        private static void GetCustomers()
        {
            Customers = new List<Customer>();


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
                Customer Obj = new Customer(Id, Name, Nationality, Language, Gender, Email, PhoneNumber);
                Obj.UserImage = new CustomImage(CustomerImage);
                Customers.Add(Obj);
            }
            Reader.Close();
            return;
        }

        private static void GetTourGuides()
        {
            TourGuides = new List<TourGuide>();

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
                TourGuide Obj = new TourGuide(Id, Name, Nationality, Language, Gender, Email, PhoneNumber);
                Obj.UserImage = new CustomImage(TourGuideImage);
                TourGuides.Add(Obj);
            }
            Reader.Close();
            return;
        }

        private static void GetTrips()
        {
            Trips = new List<Trip>();

            //created a command
            Command.CommandText = "SELECT * FROM Trips";

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
                DateTime Start = (DateTime)Reader["Start"];
                DateTime End = (DateTime)Reader["End"];
                Byte[] TripImage = (Byte[])Reader["Image"];
                TourGuide CurrentTourGuide = SelectTourGuide(TourGuideId);
                if (CurrentTourGuide == null)
                    continue;
                Trip Obj = new Trip(TripId, CurrentTourGuide, Depature, Destination, Discount, Start, End);
                Obj.TripImage = new CustomImage(TripImage);
                Trips.Add(Obj);
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

                foreach (Trip C in Trips)
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
                else if (TypeOfTrip == "Genral")
                    tripType = new General();
                else if (TypeOfTrip == "Lonely")
                    tripType = new Lonely();


                Ticket CurrentTicket = new Ticket(SerialNumber, CurrentTrip, TypeOfTicket, tripType, Price, NumberOfSeats);
                CurrentCustomer.AddTicket(CurrentTicket);
                CurrentTrip.AddTicket(CurrentTicket);
            }
            Reader.Close();
            return;
        }

        public static void UpdateCustomer(Customer CurrentCustomer, string Id, string Name, string Nationality, string Language, string Gender, string Email, string PhoneNumber, CustomImage CustomerImage)
        {
            //update database
            Command.CommandText = $"UPDATE Customer set Id = '{Id}', set Name = '{Name}', set Nationality = '{Nationality}', " +
                $"set Language = '{Language}', set Gender = '{Gender}', set Email = '{Email}', set PhoneNumber = '{PhoneNumber}'," +
                $"set Image = @image where Id = '{CurrentCustomer.Id}'";
            Command.Parameters.AddWithValue("@image", CustomerImage.GetByteImage());
            Command.ExecuteNonQuery();
            Command.Parameters.Clear();

            //update object
            CurrentCustomer.Id = Id;
            CurrentCustomer.Name = Name;
            CurrentCustomer.Nationality = Nationality;
            CurrentCustomer.Language = Language;
            CurrentCustomer.Gender = Gender;
            CurrentCustomer.Email = Email;
            CurrentCustomer.PhoneNumber = PhoneNumber;
            CurrentCustomer.UserImage = CustomerImage;
        }

        public static void UpdateTourGuide(TourGuide CurrentTourGuide, string Id, string Name, string Nationality, string Language,string Gender, string Email, string PhoneNumber, CustomImage TourGuideImage)
        {
            //update databae
            Command.CommandText = $"UPDATE Customer set Id = '{Id}', set Name = '{Name}', set Nationality = '{Nationality}', " +
                $"set Language = '{Language}',set Gender = '{Gender}', set Email = '{Email}', set PhoneNumber = '{PhoneNumber}', " +
                $"set Image = @image where Id = '{CurrentTourGuide.Id}'";
            Command.Parameters.AddWithValue("@image", TourGuideImage.GetByteImage());
            Command.ExecuteNonQuery();
            Command.Parameters.Clear();

            //update object
            CurrentTourGuide.Id = Id;
            CurrentTourGuide.Name = Name;
            CurrentTourGuide.Nationality = Nationality;
            CurrentTourGuide.Gender = Gender;
            CurrentTourGuide.Email = Email;
            CurrentTourGuide.PhoneNumber = PhoneNumber;
            CurrentTourGuide.UserImage = TourGuideImage;
        }

        public static void UpdateTrip(Trip CurrentTrip, string TripId, string TourGuideId, string Depature, string Destination, double Discount, DateTime Start, DateTime End, CustomImage TripImage)
        {
            //update database
            //update Trip table
            Command.CommandText = $"UPDATE Trips set TripId = '{TripId}', set TourGuideId = '{TourGuideId}', " +
                $"set Depature = '{Depature}', set Destination = '{Destination}', set Discont = {Discount}, set Start = '{Start}'," +
                $"set End = '{End}', set Image = @Image  where TripId = '{CurrentTrip.TripId}'";
            Command.Parameters.AddWithValue("@image", TripImage.GetByteImage());
            Command.ExecuteNonQuery();
            Command.Parameters.Clear();

            //update TripsTickets Table
            Command.CommandText = $"UPDATE TripsTickets set TripId = '{TripId}' where TripId = '{CurrentTrip.TripId}'";
            Command.ExecuteNonQuery();

            //update Transactions Table
            Command.CommandText = $"UPDATE Transactions set TripId = '{TripId}' where TripId = '{CurrentTrip.TripId}'";
            Command.ExecuteNonQuery();

            //update objects
            CurrentTrip.TripId = TripId;

            //remove the trip from the previous TourGuide and add it to the new selected tourguide 
            CurrentTrip.Tour.Trips.Remove(CurrentTrip);
            TourGuide NewTour = SelectTourGuide(TourGuideId);
            CurrentTrip.Tour = NewTour;
            NewTour.Trips.Add(CurrentTrip);
            
            CurrentTrip.Departure = Depature;
            CurrentTrip.Destination = Destination;
            CurrentTrip.Discount = Discount;
            CurrentTrip.Start = Start;
            CurrentTrip.End = End;
            CurrentTrip.TripImage = TripImage;
        }

        public static void UpdateTripsTickets(Trip CurrentTrip, string PrevType, string NewType, int NumberOfSeats, double Price)
        {
            Command.CommandText = $"UPDATE TripsTickets set Type = '{NewType}', set NumberOfSeats = {NumberOfSeats}, set Price = {Price} where TripId = '{CurrentTrip.TripId} And Type = '{PrevType}'";
            CurrentTrip.NumberOfSeats.Remove(PrevType);
            CurrentTrip.PriceOfSeat.Remove(PrevType);
            CurrentTrip.NumberOfSeats.Add(NewType, NumberOfSeats);
            CurrentTrip.PriceOfSeat.Add(NewType, Price);
            Command.ExecuteNonQuery();
        }

        public static void InsertCustomer(Customer CurrentCustomer)
        {
            Command.CommandText = $"INSERT INTO Customer values('{ CurrentCustomer.Id }','{CurrentCustomer.Name }' ," +
                $" '{CurrentCustomer.Nationality}' , '{CurrentCustomer.Language}' ,'{ CurrentCustomer.Gender}','{CurrentCustomer.Email}'," +
                $"'{CurrentCustomer.PhoneNumber}', @image );";
            Command.Parameters.AddWithValue("@image", CurrentCustomer.UserImage.GetByteImage());
            Command.ExecuteNonQuery();
            Command.Parameters.Clear();
            Customers.Add(CurrentCustomer);
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
            TourGuides.Add(CurrentTourGuide);
            return;
        }

        public static void InsertTrip(Trip CurrentTrip)
        {
            Command.CommandText = $"INSERT INTO Trips values('{CurrentTrip.TripId}', '{CurrentTrip.Tour.Id}', '{CurrentTrip.Departure}', " +
                $"'{CurrentTrip.Destination}', {CurrentTrip.Discount}, '{CurrentTrip.Start.ToString()}', '{CurrentTrip.End.ToString()}'," +
                $"@image)";
            Command.Parameters.AddWithValue("@image", CurrentTrip.TripImage.GetByteImage());
            Command.ExecuteNonQuery();
            Command.Parameters.Clear();
            Trips.Add(CurrentTrip);
            return;
        }

        public static void InsertTripTickets(string TripId, string Type, int NumbrOfSeats, double Price)
        {
            Command.CommandText = $"INSERT INTO TripsTickets values( '{TripId}', '{Type}', {NumbrOfSeats}, {Price} )";
            Command.ExecuteNonQuery();
            return;
        }

        public static void InsertTransactions(string SerialNumber, string CustomerId, string TripId, string TypeOfTicket, string TypeOfTrip, double Price, int NumberOfSeats)
        {
            Command.CommandText = $"INSERT INTO Transactions values( '{SerialNumber}', '{CustomerId}', '{TripId}'," +
                $" '{TypeOfTicket}', '{TypeOfTrip}', {Price}, {NumberOfSeats} )";
            Command.ExecuteNonQuery();
            return;
        }

        //select with id
        private static Customer SelectCustomer(string Id)
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

        public static Trip SelectTrip(string Id)
        {
            foreach (Trip C in Trips)
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
        public static bool CheckUniqueCustomerId(string Id)
        {
            foreach (Customer C in Customers)
            {
                if (C.Id == Id)
                    return false;
            }
            return true;
        }

        public static bool CheckUniqueTourGuideId(string Id)
        {

            foreach (TourGuide T in TourGuides)
            {
                if (T.Id == Id)
                    return false;

            }
            return true;
        }

        public static bool CheckUniqueTripId(string Id)
        {
            foreach (Trip T in Trips)
            {
                if (T.TripId == Id)
                    return false;
            }
            return true;
        }
    }
}
