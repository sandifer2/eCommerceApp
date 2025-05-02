using System.ComponentModel;
using System.Runtime.CompilerServices;
using Library.eCommerce.Services;
namespace Maui.eCommerce.ViewModels
{
    public class MainViewModel : INotifyPropertyChanged
    {
        private string taxRateText;
        
        public event PropertyChangedEventHandler PropertyChanged;
        
        public string Display
        {
            get
            {
                return "eCommerce System";
            }
        }
        
        public string TaxRateText
        {
            get { return taxRateText; }
            set
            {
                if (taxRateText != value)
                {
                    taxRateText = value;
                    NotifyPropertyChanged();
                }
            }
        }
        
        public string TaxRateDisplay
        {
            get { return $"Current Tax Rate: {TaxRateService.Current.TaxRate:P0}"; }
        }
        
        public void UpdateTaxRate()
        {
            if (double.TryParse(taxRateText, out double newRate))
            {
                double rate = newRate / 100.0;
                if (rate >= 0 && rate <= 0.5) 
                {
                    TaxRateService.Current.TaxRate = rate;
                    NotifyPropertyChanged(nameof(TaxRateDisplay));
                }
            }
        }
        
        public MainViewModel()
        {
            taxRateText = (TaxRateService.Current.TaxRate * 100).ToString("F1");
        }
        
        protected void NotifyPropertyChanged([CallerMemberName] string propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}