using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Travelley
{
    class TourGuide: Person
    {
        private List<Trip> trips;
        internal List<Trip> Trips { get => trips; set => trips = value; }

        public TourGuide(string Id, string Name, string Nationality, string Gender, string Email, string PhoneNumber)
        {
            this.Id = Id;
            this.Name = Name;
            this.Nationality = Nationality;
            this.Languages = new List<string>();
            this.Gender = Gender;
            this.PhoneNumber = PhoneNumber;
            Trips = new List<Trip>();
        }

        void AddTrip(Trip obj)
        {
            Trips.Add(obj);
        }

        double GetSalary(int month,int year)
        {
            int currentMonth = DateTime.Now.Month;
            int currentYear = DateTime.Now.Year;
            double salary = 0;
            if (currentMonth <= month && currentYear <= year)
                return -1;
          
            foreach(Trip T in trips)
            {
                //Tourguide takes money if he starts the trip
                if (T.Start.Month == month && T.Start.Year == year)
                {
                    salary += 150;
                }
            }
            return salary;
        }
    }
}
