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

        public TourGuide(string Name, string Id, string Gender, string Email, string PhoneNumber)
        {
            this.Name = Name;
            this.Id = Id;
            this.Gender = Gender;
            this.Email = Email;
            this.PhoneNumber = PhoneNumber;
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
