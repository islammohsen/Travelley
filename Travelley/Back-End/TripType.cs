using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Travelley.Back_End
{
    /// <summary>
    /// Abstract Type for Trip Types
    /// </summary>
    public abstract class TripType
    {
        protected string name;
        public int minNumberOfSeats;
        public int maxNumberOfSeats;

        virtual public bool InRange(int NumberOfSeats)
        {
            return NumberOfSeats >= minNumberOfSeats && NumberOfSeats <= maxNumberOfSeats;
        }

        public override string ToString()
        {
            return name;
        }
    }

    /// <summary>
    ///Family type
    ///Minimum number of seats to reserve : 3 .
    ///Maximum number of seats to reserve : Inf .
    /// </summary>
    public class Family: TripType
    {
        public Family()
        {
            name = "Family";
            minNumberOfSeats = 3;
            maxNumberOfSeats = int.MaxValue;
        }
    }

    /// <summary>
    ///Couple type
    ///Minimum number of seats to reserve : 2 .
    ///Maximum number of seats to reserve : 2 .
    /// *Can only reserve 2 seats
    /// </summary>
    public class Couple: TripType
    {
        public Couple()
        {
            name = "Couple";
            minNumberOfSeats = 2;
            maxNumberOfSeats = 2;
        }
    }

    /// <summary>
    ///General type
    ///Minimum number of seats to reserve : 1 .
    ///Maximum number of seats to reserve : Inf .
    /// *Can reserve any number of seats .
    /// </summary>
    public class General: TripType
    {
        public General()
        {
            name = "General";
            minNumberOfSeats = 1;
            maxNumberOfSeats = int.MaxValue;
        }
    }

    /// <summary>
    ///Friends type
    ///Minimum number of seats to reserve : 2 .
    ///Maximum number of seats to reserve : Inf .
    /// </summary>
    public class Friends: TripType
    {
        public Friends()
        {
            name = "Friends";
            minNumberOfSeats = 2;
            maxNumberOfSeats = int.MaxValue;
        }
    }

    /// <summary>
    ///Lonely type
    ///Minimum number of seats to reserve : 1 .
    ///Maximum number of seats to reserve : 1 .
    /// *Can only reserve 1 seat
    /// </summary>
    public class Lonely: TripType
    {
        public Lonely()
        {
            name = "Lonely";
            minNumberOfSeats = 1;
            maxNumberOfSeats = 1;
        }
    } 
}
