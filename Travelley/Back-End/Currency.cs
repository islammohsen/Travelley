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

        virtual public double ToEGP(double EGP)
        {
            return 0;
        }
    }

    public class EGP: Currency
    {
        public override double GetValue(double EGP)
        {
            return EGP;
        }

        public override double ToEGP(double EGP)
        {
            return EGP;
        }

        public override string ToString()
        {
            return "EGP";
        }
    }

    public class Dollar : Currency
    {

        public override double GetValue(double EGP)
        {
            double dollar = EGP * 0.057;
            return dollar;
        }

        public override double ToEGP(double EGP)
        {
            return EGP * 17.63;
        }

        public override string ToString()
        {
            return "Dollar";
        }
    }

    public class EURO : Currency
    {

        public override double GetValue(double EGP)
        {
            double euro = EGP * 0.046;
            return euro;
        }

        public override double ToEGP(double EGP)
        {
            return 21.12 * EGP;
        }

        public override string ToString()
        {
            return "Euro";
        }
    }
    public class RiyalSaudi : Currency
    {
        public override double GetValue(double EGP)
        {
            double riyal = EGP * 0.21;
            return riyal;
        }

        public override double ToEGP(double EGP)
        {
            return 4.70 * EGP;
        }

        public override string ToString()
        {
            return "Riyal";
        }

    }

}
