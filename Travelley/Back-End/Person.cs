using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using Travelley.Back_End;

namespace Travelley
{
  public abstract class Person
    {
        private string id;
        private string name;
        private string gender;
        private string email;
        private string phoneNumber;
        private string language;
        private string nationality;

        public string Id { get => id; set => id = value; }
        public string Name { get => name; set => name = value; }
        public string Gender { get => gender; set => gender = value; }
        public string Email { get => email; set => email = value; }
        public string PhoneNumber { get => phoneNumber; set => phoneNumber = value; }
        public string Nationality { get => nationality; set => nationality = value; }
        public string Language { get => language; set => language = value; }

        public CustomImage UserImage;
    }
}
