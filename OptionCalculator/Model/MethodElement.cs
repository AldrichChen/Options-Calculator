using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;

namespace OptionCalculator.Model {
    public class MethodElement: ConfigurationElement {

        public MethodElement() { }

        public MethodElement(string method_name, long spot_price, long strike_price, double interest_rate, 
            int period, double step, double volatility) {
            MethodName = method_name;
            SpotPrice = spot_price;
            StrikePrice = strike_price;
            InterestRate = interest_rate;
            Volatility = volatility;
            /*
            Period = period;
            Step = step;
            */
        }

        [ConfigurationProperty("method_name", IsRequired = true, IsKey = true)]
        public string MethodName {
            get { return this["method_name"] as string; }
            set { this["method_name"] = value; }
        }

        [ConfigurationProperty("spot_price", IsRequired = true, IsKey = false)]
        public double SpotPrice {
            get { return (double)this["spot_price"]; }
            set { this["spot_price"] = value; }
        }

        [ConfigurationProperty("strike_price", IsRequired = true, IsKey = false)]
        public double StrikePrice {
            get { return (double)this["strike_price"]; }
            set { this["strike_price"] = value; }
        }

        [ConfigurationProperty("interest_rate", IsRequired = true, IsKey = false)]
        public double InterestRate {
            get { return (double)this["interest_rate"]; }
            set { this["interest_rate"] = value; }
        }

        [ConfigurationProperty("volatility", IsRequired = true, IsKey = false)]
        public double Volatility {
            get { return (double)this["volatility"]; }
            set { this["volatility"] = value; }
        }

        [ConfigurationProperty("period", IsRequired = true, IsKey = false)]
        public int Period {
            get { return (int)this["period"]; }
            set { this["period"] = value; }
        }
        /*
        [ConfigurationProperty("step", IsRequired = true, IsKey = false)]
        public double Step {
            get { return (double)this["step"]; }
            set { this["step"] = value; }
        }
        */

    }

    public class MethodCollection : ConfigurationElementCollection {
        public MethodCollection(){}

        public MethodElement this[int index]{
            get { return (MethodElement)BaseGet(index); }
            set {
                if(BaseGet(index) != null) BaseRemoveAt(index);
                BaseAdd(index, value);
            }
        }

        protected override ConfigurationElement CreateNewElement() {
            return new MethodElement();
        }

        protected override Object GetElementKey(ConfigurationElement element) {
            return ((MethodElement)element).MethodName;
        }
        
        public void Clear(){
            BaseClear();
        }

        public void Add(MethodElement method_element){
            BaseAdd(method_element);
        }

        public int GetElementIndex(string name) {
            for (int i = 0; i < this.Count; i++) {
                if (this[i].MethodName == name) {
                    return i;
                }
            }
            return -1;
        }
    }

    public class MethodSection : ConfigurationSection {
        public MethodSection() { }
        [ConfigurationProperty("Methods", IsDefaultCollection = true)]
        [ConfigurationCollection(typeof(MethodCollection),
            AddItemName = "add",
            ClearItemsName = "clear",
            RemoveItemName = "remove")]
        public MethodCollection Methods {
            get { return (MethodCollection)base["Methods"]; }
        }

        public static MethodSection GetConfigSection() {
            return (ConfigurationManager.GetSection("MethodSection") as MethodSection);
        }
    }

    
}
