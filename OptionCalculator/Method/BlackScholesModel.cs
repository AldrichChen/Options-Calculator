using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;
using OptionCalculator.Model;
using OptionCalculator.Utils;

namespace OptionCalculator.Method {
    public class BlackScholesModel: Method {
        public BlackScholesModel(): base() { }

        public BlackScholesModel(MethodElement method_element): base(method_element) {
            
        }

        public BlackScholesModel(double stock_price, double strike_price, int period, 
            double interest_rate, double volatility): base(stock_price, strike_price, period, 
            interest_rate, volatility) {
            
        }

        public override double Calculate(OptionType option_type) {

            double price = 0.0;

            double d1 = (Math.Log(SpotPrice / StrikePrice) + (InterestRate + Volatility * Volatility / 2) * Period) / (Volatility * Math.Sqrt(Period)), 
                d2 = d1 - Volatility * Math.Sqrt(Period);

            double N1 = NormalDistribution.CumulativeDensityFunction(d1), 
                N2 = NormalDistribution.CumulativeDensityFunction(d2),
                negN1 = NormalDistribution.CumulativeDensityFunction(-d1), 
                negN2 = NormalDistribution.CumulativeDensityFunction(-d2);

            if (option_type == OptionType.Call)
                price = SpotPrice * N1 - StrikePrice * Math.Exp(-InterestRate * Period) * N2;
            else
                price = StrikePrice * Math.Exp(-InterestRate * Period) * negN2 - SpotPrice * negN1;

            return price;
        }
    }
}
