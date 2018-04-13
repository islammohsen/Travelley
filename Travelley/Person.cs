using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Travelley
{
    abstract class Person
    {
        protected string Name;
        protected int Id;
        protected string Gender;
        protected string Email;
        protected string PhoneNumber;

        public string Name1 { get => Name; set => Name = value; }
        public int Id1 { get => Id; set => Id = value; }
        public string Gender1 { get => Gender; set => Gender = value; }
        public string Email1 { get => Email; set => Email = value; }
        public string PhoneNumber1 { get => PhoneNumber; set => PhoneNumber = value; }
    }
}
