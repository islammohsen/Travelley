using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;

namespace Travelley
{

    static class DataBase
    {
        public static List<Customer> Customers;
        public static List<TourGuide> TourGuides;
        public static List<Trip> Trips;
        private static SqlConnection Connection = new SqlConnection("Data Source = (LocalDB)\\MSSQLLocalDB; AttachDbFilename=|DataDirectory|\\Test.mdf; Integrated Security = True");
        private static SqlCommand Command = new SqlCommand();
        private static SqlDataReader Reader;
        private static bool IsIntialized = false;

        public static void Intialize()
        {
            if(!IsIntialized)
            {
                Connection.Open();
                Command.Connection = Connection;
                IsIntialized = true;
            }
            GetCustomers();
            GetTourGuides();
            GetLanguages();
            GetTrips();
            TripTickets();
            Transactions();
        }

        public static void ShutDown()
        {
            if(IsIntialized)
            {
                Connection.Close();
                IsIntialized = false;
            }
        }

        public static void GetCustomers()
        {
            Customers = new List<Customer>();
            

            //created a command
            Command= new SqlCommand("SELECT * FROM Customers", Connection);

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
                Customer Obj = new Customer(Id, Name, Nationality, Languages, Gender, Email, PhoneNumber);
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

                TourGuide Obj = new TourGuide(Id, Name, Nationality, Gender, Email, PhoneNumber);
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
                TourGuide CurrentTourGuide = SelectTourGuide(TourGuideId);
                Trip Obj = new Trip(TripId, CurrentTourGuide, Type, Depature, Destination, Discount, Start, End);
                Trips.Add(Obj);
            }
            Reader.Close();
            return;
        }

        public static void TripTickets()
        {

            //created a command
            SqlCommand Command = new SqlCommand("select * from TripTickets", Connection);

            //excuted the command
            Reader = Command.ExecuteReader();

            while (Reader.Read())
            {
                string TripId = (string)Reader["TripId"];
                string Type = (string)Reader["Type"];
                int NoOfSeats = (int)Reader["NoOfSeats"];
                double Price = (double)Reader["Price"];

                foreach (Trip C in Trips)
                {
                    if (C.TripId == TripId)
                    {
                        C.AddSeats(Type, NoOfSeats, Price);
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

            while(Reader.Read())
            {
                string Id = (string)Reader["Id"];
                string Language = (string)Reader["Language"];
                foreach(TourGuide C in TourGuides)
                {
                    if(C.Id == Id)
                    {
                        C.Languages.Add(Language);
                        break;
                    }
                }
            }

            Reader.Close();
            return;
        }

        public static bool UpdateCustomer(Customer CurrentCustomer, string Id, string Name, string Nationality, string Language, string Gender, string Email, string PhoneNumber)
        {
            if(CheckUniqueCustomerId(Id) || CurrentCustomer.Id == Id)
            {
                Command = new SqlCommand($"UPDATE Customer set Id = '{Id}', set Name = '{Name}', set Nationality = '{Nationality}', " +
                    $"set Language = '{Language}', set Gender = '{Gender}', set Email = '{Email}', set PhoneNumber = '{PhoneNumber}' where Id = '{CurrentCustomer.Id}'",
                    Connection);
                Command.ExecuteNonQuery();
                CurrentCustomer.Id = Id;
                CurrentCustomer.Name = Name;
                CurrentCustomer.Nationality = Nationality;
                CurrentCustomer.Languages[0] = Language;
                CurrentCustomer.Gender = Gender;
                CurrentCustomer.Email = Email;
                CurrentCustomer.PhoneNumber = PhoneNumber;
                return true;
            }
            return false;
        }

        public static bool UpdateTourGuide(TourGuide CurrentTourGuide, string Id, string Name, string Nationality, string Gender, string Email, string PhoneNumber)
        {
            if (CheckUniqueTourGuideId(Id) || CurrentTourGuide.Id == Id)
            {
                Command = new SqlCommand($"UPDATE Customer set Id = '{Id}', set Name = '{Name}', set Nationality = '{Nationality}', " +
                    $"set Gender = '{Gender}', set Email = '{Email}', set PhoneNumber = '{PhoneNumber}' where Id = '{CurrentTourGuide.Id}'",
                    Connection);
                CurrentTourGuide.Id = Id;
                CurrentTourGuide.Name = Name;
                CurrentTourGuide.Nationality = Nationality;
                CurrentTourGuide.Gender = Gender;
                CurrentTourGuide.Email = Email;
                CurrentTourGuide.PhoneNumber = PhoneNumber;
                return true;
            }
            return false;
        }
        
        public static void InsertCustomer(Customer CurrentCustomer)
        {
            Command = new SqlCommand($"INSERT INTO Customer values('{ CurrentCustomer.Id }','{CurrentCustomer.Name }' ," +
                $" '{CurrentCustomer.Nationality}' , '{CurrentCustomer.Languages[0]}' ,'{ CurrentCustomer.Gender}','{CurrentCustomer.Email}'," +
                $"'{CurrentCustomer.PhoneNumber}')", Connection);
            Command.ExecuteNonQuery();
            return;
        }

        public static void InsertTourGuide(TourGuide CurrentTourGuide)
        {
            Command = new SqlCommand($"INSERT INTO Customer values('{ CurrentTourGuide.Id }','{CurrentTourGuide.Name }' ," +
                $" '{CurrentTourGuide.Nationality}','{ CurrentTourGuide.Gender}','{CurrentTourGuide.Email}'," +
                $"'{CurrentTourGuide.PhoneNumber}')", Connection);
            Command.ExecuteNonQuery();
            return;
        }

        public static void InsertTrip(Trip CurrentTrip)
        {
            Command = new SqlCommand($"INSERT INTO values('{CurrentTrip.TripId}', '{CurrentTrip.Tour.Id}', '{CurrentTrip.Type}', '{CurrentTrip.Departure}', " +
                $"'{CurrentTrip.Destination}', {CurrentTrip.Discount}, '{CurrentTrip.Start.ToString()}', '{CurrentTrip.End.ToString()}')", Connection);
            Command.ExecuteNonQuery();
            return;
        }

        public static void InsertTripTickets(string TripId,string Type,int NumbrOfSeats,double Price)
        {
            SqlCommand Command = new SqlCommand($"INSERT INTO TripTickets values( '{TripId}', '{Type}', {NumbrOfSeats}, {Price} )", Connection);
            Command.ExecuteNonQuery();
            return;
        }

        public static void InsertTransactions(string SerialNumber, string CustomerId, string TripId, string Type, double Price, int NumberOfSeats)
        {
            Command = new SqlCommand($"INSERT INTO Transactions values( '{SerialNumber}', '{CustomerId}', '{TripId}'," +
                $" '{Type}', {Price}, {NumberOfSeats} )", Connection);
            Command.ExecuteNonQuery();
            return;
        }

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

        private static Trip SelectTrip(string Id)
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

        private static TourGuide SelectTourGuide(string Id)
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
        private static  bool CheckUniqueCustomerId(string Id)
        {
            foreach (Customer C in Customers)
            {
                if (C.Id == Id)
                    return false;
            }
            return true;
        }

        private static bool CheckUniqueTourGuideId(string id)
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
