using System;
using System.Collections.Generic;
using Travelley.Back_End;

namespace Travelley
{
   public class TourGuide : Person
    {
        public static List<TourGuide> TourGuides; //static list contains all objects of TourGuide
        private List<Trip> trips; //list contains the trips assigned to this tourguide
        internal List<Trip> Trips { get => trips; set => trips = value; }

        public TourGuide(string Id, string Name, string Nationality, string Language,string Gender, string Email, string PhoneNumber, CustomImage UserImage)
        {
            this.Id = Id;
            this.Name = Name;
            this.Nationality = Nationality;
            this.Language = Language;
            this.Gender = Gender;
            this.Email = Email;
            this.PhoneNumber = PhoneNumber;
            this.UserImage = UserImage;
            Trips = new List<Trip>();
        }

        /// <summary>
        /// Function call calculate the salary of the tourguide in the given month
        /// </summary>
        public double GetSalary(int month, int year)
        {
            int currentMonth = DateTime.Now.Month;
            int currentYear = DateTime.Now.Year;
            double salary = 0;
            if (currentMonth < month && currentYear < year)
                return -1;

            foreach (Trip T in trips)
            {
                //Tourguide takes money if he starts the trip
                if (T.Start.Month == month && T.Start.Year == year)
                {
                    salary += 150;
                }
            }
            return salary;
        }

        /// <summary>
        /// Function return The Best TourGuide in this month
        /// The tourguide is choosen based on his salary
        /// if best tourguide salary is Zero then function returns null
        /// </summary>
        public static TourGuide GetBestTourGuide(int Month)
        {
            int year = DateTime.Today.Year;
            if (Month == 0)  //handles the case if we are in month 1
            {
                Month = 12;
                year--;
            }
            TourGuide ret = TourGuides[0];

            foreach (TourGuide t in TourGuides)
            {
                if (t.GetSalary(Month, year) > ret.GetSalary(Month, year))
                    ret = t;
            }
            if (ret.GetSalary(Month, year) == 0) //Not Considered a best touguide with salary = 0 !!
                return null;
            return ret;
        }

        /// <summary>
        /// Returns the number of availble tourguides today
        /// </summary>
        public static int GetNumberOfAvailableTourGuides()
        {
            int ret = 0;
            foreach (TourGuide T in TourGuide.TourGuides)
            {
                if (T.CheckAvailability(DateTime.Today, DateTime.Today))
                    ret++;
            }
            return ret;
        }

        /// <summary>
        /// checks if the tourguide is available in the given period of time
        /// </summary>
		public bool CheckAvailability(DateTime start, DateTime end)
		{
            foreach(Trip T in trips)
            {
                if ((start >= T.Start && start <= T.End) || (end >= T.Start && end <= T.End) || (T.Start >= start && T.Start <= end) || (T.End >= start && T.End <= end))
                    return false;
            }
            return true;
		}

        public override string ToString()
        {
            return Name;
        }

        /// <summary>
        /// To sort a list of customers according to their salary this month
        /// </summary>
        public override int CompareTo(object obj)
        {
            return -1 * GetSalary(DateTime.Today.Month, DateTime.Today.Year).CompareTo(
                ((TourGuide)obj).GetSalary(DateTime.Today.Month, DateTime.Today.Year));
        }
    }
}
