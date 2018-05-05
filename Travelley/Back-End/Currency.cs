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

    public class EGP : Currency
    {
        public override double GetValue(double EGP)
        {
            return Math.Round(EGP, 2);
        }

        public override double ToEGP(double EGP)
        {
            return Math.Round(EGP, 2);
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
            double dollar = Math.Round(EGP * 0.057, 2);
            return dollar;
        }

        public override double ToEGP(double Dollar)
        {
            return Math.Round(Dollar * (1 / 0.057), 2);
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
            double euro = Math.Round(EGP * 0.047, 2);
            return euro;
        }

        public override double ToEGP(double Euro)
        {
            return Math.Round(Euro * (1 / 0.047), 2);
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
            double riyal = Math.Round(EGP * 0.21, 2);
            return riyal;
        }

        public override double ToEGP(double Riyal)
        {
            return Math.Round(Riyal * (1 / 0.21), 2);
        }

        public override string ToString()
        {
            return "Riyal";
        }

    }

}
