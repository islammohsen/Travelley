using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Windows.Controls;
using System.Windows.Media.Imaging;

namespace Travelley
{
    class TourGuide: Person
    {
        private List<Trip> trips;
        private byte[] tourGuideImage;

        internal List<Trip> Trips { get => trips; set => trips = value; }
        public byte[] TourGuideImage { get => tourGuideImage; set => tourGuideImage = value; }

        public TourGuide(string Id, string Name, string Nationality, string Gender, string Email, string PhoneNumber, byte[] TourGuideTmage)
        {
            this.Id = Id;
            this.Name = Name;
            this.Nationality = Nationality;
            this.Languages = new List<string>();
            this.Gender = Gender;
            this.PhoneNumber = PhoneNumber;
            this.tourGuideImage = TourGuideTmage;
            Trips = new List<Trip>();
            this.Email = Email;
        }

        public Image GetImage()
        {
            BitmapImage bi = new BitmapImage();
            bi.BeginInit();
            bi.CreateOptions = BitmapCreateOptions.None;
            bi.CacheOption = BitmapCacheOption.Default;
            bi.StreamSource = new MemoryStream(tourGuideImage);
            bi.EndInit();

            Image img = new Image();  //Image control of wpf

            img.Source = bi;
            return img;
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
        public static TourGuide GetBestTourGuide(int Month)
        {

            int year = DateTime.Today.Year;
            if (Month == 0)  //handles the case if we are in month 1
            {
                Month = 12;
                year--;
            }
            TourGuide ret = DataBase.TourGuides[0];

            foreach(TourGuide t in DataBase.TourGuides)
            {
                if (t.GetSalary(Month, year) > ret.GetSalary(Month, year))
                    ret = t;
            }
            return ret;
        }

    }
}
