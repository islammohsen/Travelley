using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;
using Travelley.Back_End;

namespace Travelley
{

    static class DataBase
    {
        public static List<Customer> Customers;
        public static List<TourGuide> TourGuides;
        public static List<Trip> Trips;
        private static SqlConnection Connection = new SqlConnection("Data Source = (LocalDB)\\MSSQLLocalDB; AttachDbFilename=|DataDirectory|\\Travelley.mdf; Integrated Security = True");
        private static SqlCommand Command = new SqlCommand();
        private static SqlDataReader Reader;
        private static bool IsIntialized = false;

        public static void Intialize()
        {
            if (!IsIntialized)
            {
                Connection.Open();
                Command.Connection = Connection;
                IsIntialized = true;
            }
            GetCustomers();
            GetTourGuides();
            GetLanguages();
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

        public static void GetCustomers()
        {
            Customers = new List<Customer>();


            //created a command
            Command = new SqlCommand("SELECT * FROM Customer", Connection);

            //excuted the command
            Reader = Command.ExecuteReader();

            //Reading Data
            while (Reader.Read())
            {
                string Id = (string)Reader["Id"];
                string Name = (string)Reader["Name"];
                string Nationality = (string)Reader["Nationality"];
                string Language = (string)Reader["Language"];
                List<string> Languages = new List<string>();
                Languages.Add(Language);
                string Gender = (string)Reader["Gender"];
                string Email = (string)Reader["Email"];
                string PhoneNumber = (string)Reader["PhoneNumber"];
                byte[] CustomerImage = (byte[])Reader["CustomerImage"];
                Customer Obj = new Customer(Id, Name, Nationality, Languages, Gender, Email, PhoneNumber);
                Obj.UserImage = new CustomImage(CustomerImage);
                Customers.Add(Obj);
            }
            Reader.Close();
            return;
        }

        public static void GetTourGuides()
        {
            TourGuides = new List<TourGuide>();

            //created a command
            Command = new SqlCommand("SELECT * from TourGuide", Connection);

            Reader = Command.ExecuteReader();

            //Reading Data
            while (Reader.Read())
            {
                string Id = (string)Reader["Id"];
                string Name = (string)Reader["Name"];
                string Nationality = (string)Reader["Nationality"];
                string Gender = (string)Reader["Gender"];
                string Email = (string)Reader["Email"];
                string PhoneNumber = (string)Reader["PhoneNumber"];
                byte[] TourGuideImage = (byte[])Reader["TourGuideImage"];
                TourGuide Obj = new TourGuide(Id, Name, Nationality, Gender, Email, PhoneNumber);
                Obj.UserImage.SetImage(TourGuideImage);
                TourGuides.Add(Obj);
            }
            Reader.Close();
            return;
        }

        public static void GetTrips()
        {
            Trips = new List<Trip>();

            //created a command
            Command = new SqlCommand("SELECT * FROM Trip", Connection);

            //excuted the command
            Reader = Command.ExecuteReader();

            //Reading Data
            while (Reader.Read())
            {
                String TripId = (string)Reader["TripId"];
                String TourGuideId = (string)Reader["TripId"];
                String Type = (string)Reader["TripId"];
                String Depature = (string)Reader["TripId"];
                String Destination = (string)Reader["TripId"];
                Double Discount = (Double)Reader["Discount"];
                DateTime Start = (DateTime)Reader["Start"];
                DateTime End = (DateTime)Reader["End"];
                Byte[] TripImage = (Byte[])Reader["TripImage"];
                TourGuide CurrentTourGuide = SelectTourGuide(TourGuideId);
                Trip Obj = new Trip(TripId, CurrentTourGuide, Type, Depature, Destination, Discount, Start, End);
                Obj.TripImage = new CustomImage(TripImage);
                Trips.Add(Obj);
            }
            Reader.Close();
            return;
        }

        public static void TripsTickets()
        {

            //created a command
            Command = new SqlCommand("select * from TripsTickets", Connection);

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

        public static void Transactions()
        {

            //created a command
            Command = new SqlCommand("select * from Transactions", Connection);

            Reader = Command.ExecuteReader();

            while (Reader.Read())
            {
                string SerialNumber = (string)Reader["SerialNumber"];
                string CustomerId = (string)Reader["CustomerId"];
                string TripId = (string)Reader["TripId"];
                string TypeOfTicket = (string)Reader["TypeOfTicket"];
                double Price = (double)Reader["Price"];
                int NumberOfSeats = (int)Reader["NumberOfSeats"];
                Customer CurrentCustomer = SelectCustomer(CustomerId);
                Trip CurrentTrip = SelectTrip(TripId);
                Ticket CurrentTicket = new Ticket(SerialNumber, CurrentTrip, TypeOfTicket, Price, NumberOfSeats);
                CurrentCustomer.AddTicket(CurrentTicket);
                CurrentTrip.AddTicket(CurrentTicket);
            }
            Reader.Close();
            return;
        }

        public static void GetLanguages()
        {
            Command = new SqlCommand("SELECT * FROM TourGuideLanguage", Connection);

            Reader = Command.ExecuteReader();

            while (Reader.Read())
            {
                string Id = (string)Reader["Id"];
                string Language = (string)Reader["Language"];
                foreach (TourGuide C in TourGuides)
                {
                    if (C.Id == Id)
                    {
                        C.Languages.Add(Language);
                        break;
                    }
                }
            }

            Reader.Close();
            return;
        }

        public static bool UpdateCustomer(Customer CurrentCustomer, string Id, string Name, string Nationality, string Language, string Gender, string Email, string PhoneNumber, CustomImage CustomerImage)
        {
            if (CheckUniqueCustomerId(Id) || CurrentCustomer.Id == Id)
            {
                Command = new SqlCommand($"UPDATE Customer set Id = '{Id}', set Name = '{Name}', set Nationality = '{Nationality}', " +
                    $"set Language = '{Language}', set Gender = '{Gender}', set Email = '{Email}', set PhoneNumber = '{PhoneNumber}'," +
                    $" set CustomerImage = {CustomerImage} where Id = '{CurrentCustomer.Id}'",
                    Connection);
                Command.ExecuteNonQuery();
                CurrentCustomer.Id = Id;
                CurrentCustomer.Name = Name;
                CurrentCustomer.Nationality = Nationality;
                CurrentCustomer.Languages[0] = Language;
                CurrentCustomer.Gender = Gender;
                CurrentCustomer.Email = Email;
                CurrentCustomer.PhoneNumber = PhoneNumber;
                CurrentCustomer.UserImage = CustomerImage;
                return true;
            }
            return false;
        }

        public static bool UpdateTourGuide(TourGuide CurrentTourGuide, string Id, string Name, string Nationality, string Gender, string Email, string PhoneNumber, CustomImage TourGuideImage)
        {
            if (CheckUniqueTourGuideId(Id) || CurrentTourGuide.Id == Id)
            {
                Command = new SqlCommand($"UPDATE Customer set Id = '{Id}', set Name = '{Name}', set Nationality = '{Nationality}', " +
                    $"set Gender = '{Gender}', set Email = '{Email}', set PhoneNumber = '{PhoneNumber}', " +
                    $"set TourGuideImage = {TourGuideImage.GetByteImage()} where Id = '{CurrentTourGuide.Id}'",
                    Connection);
                CurrentTourGuide.Id = Id;
                CurrentTourGuide.Name = Name;
                CurrentTourGuide.Nationality = Nationality;
                CurrentTourGuide.Gender = Gender;
                CurrentTourGuide.Email = Email;
                CurrentTourGuide.PhoneNumber = PhoneNumber;
                CurrentTourGuide.UserImage = TourGuideImage;
                return true;
            }
            return false;
        }

        public static bool UpdateTrip(Trip CurrentTrip, string TripId, string TourGuideId, string Type, string Depature, string Destination, double Discount, DateTime Start, DateTime End, CustomImage TripImage)
        {
            if (CheckUniqueTripId(TripId) || TripId == CurrentTrip.TripId)
            {
                Command = new SqlCommand($"UPDATE Trips set TripId = '{TripId}', set TourGuideId = '{TourGuideId}', set Type = '{Type}', " +
                    $"set Depature = '{Depature}', set Destination = '{Destination}', set Discont = {Discount}, set Start = '{Start}'," +
                    $"set End = '{End}', set Image = {TripImage.GetByteImage()}  where TripId = '{CurrentTrip.TripId}'", Connection);
                Command = new SqlCommand($"UPDATE TripsTickets set TripId = '{TripId}' where TripId = '{CurrentTrip.TripId}'");
                Command = new SqlCommand($"UPDATE Transactions set TripId = '{TripId}' where TripId = '{CurrentTrip.TripId}'");
                CurrentTrip.TripId = TripId;
                TourGuide T = SelectTourGuide(TourGuideId);
                CurrentTrip.Tour = T;
                CurrentTrip.Type = Type;
                CurrentTrip.Departure = Depature;
                CurrentTrip.Destination = Destination;
                CurrentTrip.Discount = Discount;
                CurrentTrip.Start = Start;
                CurrentTrip.End = End;
                CurrentTrip.TripImage = TripImage;
                Command.ExecuteNonQuery();
                return true;
            }
            return false;
        }

        public static bool UpdateTripsTickets(Trip CurrentTrip, string Type, int NumberOfSeats, double Price)
        {
            if (CurrentTrip.NumberOfSeats.ContainsKey(Type))
            {
                Command = new SqlCommand($"UPDATE TripsTickets set Type = '{Type}', set NumberOfSeats = {NumberOfSeats}, set Price = {Price} where TripId = '{CurrentTrip.TripId}'");
                CurrentTrip.NumberOfSeats[Type] = NumberOfSeats;
                CurrentTrip.PriceOfSeat[Type] = Price;
                Command.ExecuteNonQuery();
                return true;
            }
            return false;
        }

        public static bool UpdateTourGuideLanguage()
        {
            //todo
            return false;
        }

        public static void InsertCustomer(Customer CurrentCustomer)
        {
            Command = new SqlCommand($"INSERT INTO Customer values('{ CurrentCustomer.Id }','{CurrentCustomer.Name }' ," +
                $" '{CurrentCustomer.Nationality}'F , '{CurrentCustomer.Languages[0]}' ,'{ CurrentCustomer.Gender}','{CurrentCustomer.Email}'," +
                $"'{CurrentCustomer.PhoneNumber}', {CurrentCustomer.UserImage.GetByteImage()});", Connection);
            Customers.Add(CurrentCustomer);
            Command.ExecuteNonQuery();
            return;
        }

        public static void InsertTourGuide(TourGuide CurrentTourGuide)
        {
            Command = new SqlCommand($"INSERT INTO TourGuide values('{ CurrentTourGuide.Id }','{CurrentTourGuide.Name }' ," +
                $" '{CurrentTourGuide.Nationality}','{ CurrentTourGuide.Gender}','{CurrentTourGuide.Email}'," +
                $"'{CurrentTourGuide.PhoneNumber}', {CurrentTourGuide.UserImage.GetByteImage()})", Connection);
            TourGuides.Add(CurrentTourGuide);
            Command.ExecuteNonQuery();
            return;
        }

        public static void InsertTrip(Trip CurrentTrip)
        {
            Command = new SqlCommand($"INSERT INTO Trip values('{CurrentTrip.TripId}', '{CurrentTrip.Tour.Id}', '{CurrentTrip.Type}', '{CurrentTrip.Departure}', " +
                $"'{CurrentTrip.Destination}', {CurrentTrip.Discount}, '{CurrentTrip.Start.ToString()}', '{CurrentTrip.End.ToString()}'," +
                $"{CurrentTrip.TripImage.GetByteImage()})", Connection);
            Trips.Add(CurrentTrip);
            Command.ExecuteNonQuery();
            return;
        }

        public static void InsertTripTickets(string TripId, string Type, int NumbrOfSeats, double Price)
        {
            Command = new SqlCommand($"INSERT INTO TripsTickets values( '{TripId}', '{Type}', {NumbrOfSeats}, {Price} )", Connection);
            Command.ExecuteScalar();
            return;
        }

        public static void InsertTransactions(string SerialNumber, string CustomerId, string TripId, string Type, double Price, int NumberOfSeats)
        {
            Command = new SqlCommand($"INSERT INTO Transactions values( '{SerialNumber}', '{CustomerId}', '{TripId}'," +
                $" '{Type}', {Price}, {NumberOfSeats} )", Connection);
            Command.ExecuteNonQuery();
            return;
        }

        public static void InsertLanguage(string TourGuideId, string Language)
        {
            Command = new SqlCommand($"INSERT INTO TourGuideLanguage values('{TourGuideId}', '{Language}')", Connection);
            Command.ExecuteNonQueryAsync();
            return;
        }

        public static Customer SelectCustomer(string Id)
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
