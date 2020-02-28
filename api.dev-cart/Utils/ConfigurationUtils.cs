using api.dev_cart.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Utils
{
    public class ConfigurationUtils
    {
        public static void AddConfiguration(string key, string value)
        {
            Entities entity = new Entities();
            var config = new cat_Configurations();

            config.Configuration_Key = key;
            config.Configuration_Value = value;

            entity.cat_Configurations.Add(config);
            entity.SaveChanges();
        }

        public static void EditConfiguration(string key, string value)
        {
            Entities entity = new Entities();
            var config = entity.cat_Configurations.FirstOrDefault(x => x.Configuration_Key == key);

            config.Configuration_Key = key;
            config.Configuration_Value = value;

            entity.SaveChanges();
        }

        public static void DeleteConfiguration(string key)
        {
            Entities entity = new Entities();
            var config = entity.cat_Configurations.FirstOrDefault(x => x.Configuration_Key == key);

            entity.cat_Configurations.Remove(config);
            entity.SaveChanges();
        }

        public static string GetConfiguration(string key, string @default)
        {
            Entities entity = new Entities();
            var value = entity.cat_Configurations.Where(x => x.Configuration_Key == key).Select(x => x.Configuration_Value).FirstOrDefault();
            return value ?? @default;
        }

        public static int GetConfiguration(string key, int @default)
        {
            Entities entity = new Entities();
            var value = entity.cat_Configurations.Where(x => x.Configuration_Key == key).Select(x => x.Configuration_Value).FirstOrDefault();
            return value.Length == 0 ? @default : int.Parse(value);
        }
    }
}