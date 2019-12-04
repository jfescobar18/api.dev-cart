using System;
using System.Configuration;
namespace api.dev_cart.Resources
{
    public class EmailResources
    {
        public static string Host => ConfigurationManager.AppSettings["MimeMailerHost"];
        public static int Port => int.Parse(ConfigurationManager.AppSettings["MimeMailerPort"]);
        public static string Username => ConfigurationManager.AppSettings["MimeMailerUsername"];
        public static string Password => ConfigurationManager.AppSettings["MimeMailerPassword"];
    }
}