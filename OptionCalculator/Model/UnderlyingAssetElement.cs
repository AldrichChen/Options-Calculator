using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;

namespace OptionCalculator.Model {
    public class UnderlyingAssetElement: ConfigurationElement {
        public UnderlyingAssetElement() { }

        public UnderlyingAssetElement(string asset) {
            Asset = asset;
        }

        [ConfigurationProperty("asset", IsRequired = true, IsKey = true)]
        public string Asset {
            get { return this["asset"] as string; }
            set { this["asset"] = value; }
        }

    }

    public class UnderlyingAssetCollection : ConfigurationElementCollection {
        public UnderlyingAssetCollection() { }

        public UnderlyingAssetElement this[int index]{
            get { return (UnderlyingAssetElement)BaseGet(index); }
            set {
                if (BaseGet(index) != null) BaseRemoveAt(index);
                BaseAdd(index, value);
            }
        }

        protected override ConfigurationElement CreateNewElement() {
            return new UnderlyingAssetElement();
        }

        protected override Object GetElementKey(ConfigurationElement element) {
            return ((UnderlyingAssetElement)element).Asset;
        }

        public void Clear() {
            BaseClear();
        }

        public void Add(UnderlyingAssetElement method_element) {
            BaseAdd(method_element);
        }

        public int GetElementIndex(string name) {
            for (int i = 0; i < this.Count; i++) {
                if (this[i].Asset == name) {
                    return i;
                }
            }
            return -1;
        }
    }

    public class UnderlyingAssetSection : ConfigurationSection {
        public UnderlyingAssetSection() { }

        [ConfigurationProperty("UnderlyingAssets", IsDefaultCollection = true)]
        [ConfigurationCollection(typeof(MethodCollection),
            AddItemName = "add",
            ClearItemsName = "clear",
            RemoveItemName = "remove")]
        public UnderlyingAssetCollection UnderlyingAssets {
            get { return (UnderlyingAssetCollection)base["UnderlyingAssets"]; }
        }

        public static UnderlyingAssetSection GetConfigSection() {
            return (ConfigurationManager.GetSection("UnderlyingAssetSection") as UnderlyingAssetSection);
        }
    }
}
