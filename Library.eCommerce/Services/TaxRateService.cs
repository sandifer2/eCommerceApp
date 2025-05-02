namespace Library.eCommerce.Services
{
    public class TaxRateService
    {
        private static TaxRateService instance;
        private static readonly object instanceLock = new object();
        
        private double taxRate = 0.07; 
        
        public static TaxRateService Current
        {
            get
            {
                lock(instanceLock)
                {
                    if (instance == null)
                    {
                        instance = new TaxRateService();
                    }
                }
                return instance;
            }
        }
        
        public double TaxRate
        {
            get { return taxRate; }
            set { taxRate = value; }
        }
        
        private TaxRateService() { }
    }
}