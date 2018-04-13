using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Travelley
{
    class TourGuide: Person
    {
        private double salary;
        private List<Trip> Trips;

        public double Salary { get => salary; set => salary = value; }
        internal List<Trip> Trips1 { get => Trips; set => Trips = value; }

        public TourGuide(string Name, string Id, string Gender, string Email, string PhoneNumber)
        {
            this.Name = Name;
            this.Id = Id;
            this.Gender = Gender;
            this.Email = Email;
            this.PhoneNumber = PhoneNumber;
            this.PhoneNumber = PhoneNumber;
            this.salary = 0;
            Trips = new List<Trip>();
        }

        void AddTrip(Trip obj)
        {
            Trips.Add(obj);
            salary += 1;
        }
    }
}
