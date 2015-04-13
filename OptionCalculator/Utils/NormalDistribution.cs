using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;
using OptionCalculator.Model;
using OptionCalculator.Method;

namespace OptionCalculator.Utils {
    public class NormalDistribution {

        public NormalDistribution() { }

        #region constants

        private static double a1 = 0.31938153,
                            a2 = -0.356563782,
                            a3 = 1.781477937,
                            a4 = -1.821255978,
                            a5 = 1.330274429,
                            p = 0.3275911;

        #endregion


        public static double CumulativeDensityFunction(double x){

            double L = Math.Abs(x),
                K = 1.0 / (1.0 + 0.2316419 * L), 
                dCDF = 0.0;

            dCDF = 1.0 - 1.0 / Math.Sqrt(2 * Convert.ToDouble(Math.PI.ToString())) *
                Math.Exp(-L * L / 2.0) * (a1 * K + a2 * K * K + a3 * Math.Pow(K, 3.0) +
                a4 * Math.Pow(K, 4.0) + a5 * Math.Pow(K, 5.0));
            
            return (x < 0) ? 1.0 - dCDF : dCDF;
        }

    }
}
