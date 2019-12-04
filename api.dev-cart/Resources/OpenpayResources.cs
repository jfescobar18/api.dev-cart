using System.Configuration;

namespace api.dev_cart.Resources
{
    public class OpenpayResources
    {
        public static string MerchantID => ConfigurationManager.AppSettings["MerchantID"];
        public static string OpenpayPrivateKey => ConfigurationManager.AppSettings["OpenpayPrivateKey"];
        public static string OpenpayDescription => ConfigurationManager.AppSettings["OpenpayDescription"];
    }
}