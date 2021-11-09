using api.dev_cart.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Utils
{
    public class CouponUtils
    {
        public static string CreateCouponCode()
        {
            Entities entity = new Entities();
            string couponCode = String.Empty;

            do
            {
                couponCode = RandomKey(8);
            } 
            while (entity.cat_Coupons.SingleOrDefault(x => x.Coupon_Code == couponCode) != null);

            return couponCode;
        }

        private static string RandomKey(int length)
        {
            Random random = new Random();
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";

            return new string(Enumerable.Repeat(chars, length).Select(s => s[random.Next(s.Length)]).ToArray());
        }
    }
}