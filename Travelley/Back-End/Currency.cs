using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Travelley
{
    /// <summary>
    /// Abstract Class for Currencies
    /// </summary>
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
    
    /// <summary>
    ///  Egyptian Pund Currency
    ///  It is the Currency where the prices are saved in the database
    /// </summary>
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

    /// <summary>
    /// Dollar Currency Class
    /// </summary>
    public class Dollar : Currency
    {
        /// <summary>
        /// Converts from EGP to Dollar
        /// </summary>
        /// <param name="EGP">EGP Value</param>
        /// <returns>Dollar Value</returns>
        public override double GetValue(double EGP)
        {
            double dollar = Math.Round(EGP * 0.057, 2);
            return dollar;
        }

        /// <summary>
        /// Converts  from Dollar to EGP
        /// </summary>
        /// <param name="Dollar">Dollar Value</param>
        /// <returns>EGP Value</returns>
        public override double ToEGP(double Dollar)
        {
            return Math.Round(Dollar * (1 / 0.057), 2);
        }

        public override string ToString()
        {
            return "Dollar";
        }
    }

    /// <summary>
    /// Euro Currency Class
    /// </summary>
    public class EURO : Currency
    {
        /// <summary>
        /// Converts from EGP to Euro
        /// </summary>
        /// <param name="EGP">EGP Value</param>
        /// <returns>Euro Value</returns>
        public override double GetValue(double EGP)
        {
            double euro = Math.Round(EGP * 0.047, 2);
            return euro;
        }

        /// <summary>
        /// Converts from Euro to EGP
        /// </summary>
        /// <param name="Euro">Euro Value</param>
        /// <returns>EGP Value</returns>
        public override double ToEGP(double Euro)
        {
            return Math.Round(Euro * (1 / 0.047), 2);
        }

        public override string ToString()
        {
            return "Euro";
        }
    }

    /// <summary>
    /// Riyal Currency Class
    /// </summary>
    public class RiyalSaudi : Currency
    {
        /// <summary>
        /// Converts from EGP to Riyal
        /// </summary>
        public override double GetValue(double EGP)
        {
            double riyal = Math.Round(EGP * 0.21, 2);
            return riyal;
        }

        /// <summary>
        /// Converts from Riyal to EGP
        /// </summary>
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
