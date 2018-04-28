using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Travelley
{
    public abstract class Currency
    {
        virtual public double GetValue(double EGP)
        {
            return 0;
        }
    }

    public class Dollar : Currency
    {

        public override double GetValue(double EGP)
        {
            double dollar = EGP * 0.057;
            return dollar;
        }
    }
    public class EURO : Currency
    {

        public override double GetValue(double EGP)
        {
            double euro = EGP * 0.046;
            return euro;
        }
    }
    public class RiyalSaudi : Currency
    {
        public override double GetValue(double EGP)
        {
            double riyal = EGP * 0.21;
            return riyal;
        }

    }

}
