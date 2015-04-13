using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Configuration;
using OptionCalculator.Model;
using OptionCalculator.Method;

namespace OptionCalculator {
    /// <summary>
    /// MainWindow.xaml 的交互逻辑
    /// </summary>
    public partial class MainWindow : Window {

        #region Private Fields

        private MethodSection _methodSection;
        private UnderlyingAssetSection _underlyingAssetSection;

        private int _selectedIndex;
        private string _selectedMethod;

        #endregion


        public MainWindow() {
            InitializeComponent();
            Initialize();

            
        }

        private void Initialize() {

            try {
                Configuration config = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);

                _methodSection = MethodSection.GetConfigSection();
                _underlyingAssetSection = UnderlyingAssetSection.GetConfigSection();

                InitUI();
            }
            catch(Exception e) {
                MessageBox.Show(e.Message.ToString(), "Configuration Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void InitUI() {
            CalculateBtn.IsEnabled = false;
            PlotBtn.IsEnabled = false;

            if (_underlyingAssetSection != null) {
                for (int i = 0; i < _underlyingAssetSection.UnderlyingAssets.Count; i++)
                    AssetComboBox.Items.Add(_underlyingAssetSection.UnderlyingAssets[i].Asset);

                AssetComboBox.SelectedIndex = 0;
            }

            if (_methodSection != null) {
                for (int i = 0; i < _methodSection.Methods.Count; i++)
                    MethodComboBox.Items.Add(_methodSection.Methods[i].MethodName);
            }

        }

        private void BatchEnable() {
            SpotPriceTextBox.IsEnabled = true;
            VolatilityTextBox.IsEnabled = true;
            InterestRateTextBox.IsEnabled = true;
            PeriodTextBox.IsEnabled = true;
            StrikePriceTextBox.IsEnabled = true;
            StepTextBox.IsEnabled = true;
            PathTextBox.IsEnabled = true;

            CalculateBtn.IsEnabled = true;
            PlotBtn.IsEnabled = true;
        }

        private void EnableOrDisableFields(int index, string method) {
            BatchEnable();

            switch (index) { 
                case 0:
                    if (method == "Black-Scholes Model") {
                        StepTextBox.IsEnabled = false;
                        PathTextBox.IsEnabled = false;
                    }
                    break;
                case 1:
                    if (method == "Binomial Tree") {
                        PathTextBox.IsEnabled = false;
                        StepLabel.Content = "NO. of Step: ";
                    }
                    break;
                case 2:
                    if (method == "Implied Volatility") {
                        VolatilityTextBox.IsEnabled = false;
                        StepTextBox.IsEnabled = false;
                        PathTextBox.IsEnabled = false;
                    }
                    break;
                case 3:
                    if (method == "Standard Monte Carlo") {
                        StepLabel.Content = "Observations: ";
                    }
                    break;
                case 4:
                    if (method == "Monte Carlo (Control Variate)") {
                        StepLabel.Content = "Observations: ";
                    }
                    break;
                case 5:
                    if (method == "Closed-Form Geometric Asian Option") {
                        StepTextBox.IsEnabled = false;
                    }
                    break;
                default:
                    break;
            
            }
        }

        private void MethodComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e) {
            int index = ((ComboBox)sender).SelectedIndex;
            string method = ((ComboBox)sender).SelectedValue.ToString();

            this._selectedIndex = index;
            this._selectedMethod = method;
            EnableOrDisableFields(index, method);
        }



        private void CalculateBtn_Click(object sender, RoutedEventArgs e) {

        }
    }
}
