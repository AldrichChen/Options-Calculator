using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;
using OptionCalculator.Model;
using OptionCalculator.Utils;

namespace OptionCalculator.Method {
    public class Method {

        public Method() { }

        public Method(MethodElement method_element) {
            SpotPrice = method_element.SpotPrice;
            StrikePrice = method_element.StrikePrice;
            Period = method_element.Period;
            InterestRate = method_element.InterestRate;
            Volatility = method_element.Volatility;
        }

        public Method(double spot_price, double strike_price, int period,
            double interest_rate, double volatility) {
            SpotPrice = spot_price;
            StrikePrice = strike_price;
            Period = period;
            InterestRate = interest_rate;
            Volatility = volatility;
        }

        public double SpotPrice { get; set; }
        public double StrikePrice { get; set; }
        public int Period { get; set; }
        public double InterestRate { get; set; }
        public double Volatility { get; set; }

        public virtual double Calculate(OptionType option_type) {
            throw new NotImplementedException();
        }

        public virtual double Calculate(OptionType option_type, long step) {
            throw new NotImplementedException();
        }

        public virtual double Calculate(OptionType option_type, long step, long path_count) {
            throw new NotImplementedException();
        }
    }
}
