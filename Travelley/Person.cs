using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Travelley
{
    abstract class Person
    {
        private string name;
        private string id;
        private string gender;
        private string email;
        private string phoneNumber;

        public string Name { get => name; set => name = value; }
        public string Id { get => id; set => id = value; }
        public string Gender { get => gender; set => gender = value; }
        public string Email { get => email; set => email = value; }
        public string PhoneNumber { get => phoneNumber; set => phoneNumber = value; }
    }
}
