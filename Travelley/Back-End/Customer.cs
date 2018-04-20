using System;
using System.Drawing;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Media;
using System.IO;
using System.Windows.Controls;
using System.Windows.Media.Imaging;

namespace Travelley
{
    class Customer: Person
    {
        private List<Ticket> Tickets;
        private HashSet<Trip> Mark;
        private bool discount;
        private byte[] customerImage;
        public int NumberOfTrips { get => NumberOfTrips; }
        public bool Discount { get => Discount; }
        public byte[] CustomerImage { get => customerImage; set => customerImage = value; }

        public Customer(string Id, string Name, string Nationality, List<string> Languages, string Gender, string Email, string PhoneNumber, byte[] CustomerImage)
        {
            this.Id = Id;
            this.Name = Name;
            this.Nationality = Nationality;
            this.Languages = Languages;
            this.Gender = Gender;
            this.Email = Email;
            this.PhoneNumber = PhoneNumber;
            this.discount = false;
            this.Tickets = new List<Ticket>();
            Mark = new HashSet<Trip>();
            this.CustomerImage = CustomerImage;
        }
            
        public Image GetImage()
        {
            BitmapImage bi = new BitmapImage();
            bi.BeginInit();
            bi.CreateOptions = BitmapCreateOptions.None;
            bi.CacheOption = BitmapCacheOption.Default;
            bi.StreamSource = new MemoryStream(CustomerImage);
            bi.EndInit();

            Image img = new Image();  //Image control of wpf
            img.Source = bi;

            return img;
        }
        


        public void AddTicket(Ticket obj)
        {
            Tickets.Add(obj);
            Mark.Add(obj.TicketTrip);
            if (Mark.Count >= 2)
                discount = true;
            return;

        }

        public bool ReserveTicket(Trip obj, string Type)
        {
            return true;
        }

        public List<Ticket> GetTickets()
        {
            List<Ticket> Ret = new List<Ticket>();
            foreach(Ticket c in Tickets)
            {
                Ret.Add(c);
            }
            return Ret;
        }
    }
}
