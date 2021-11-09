using api.dev_cart.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;
using System.Web.Http.Cors;
using Utils;

namespace api.dev_cart.Controllers
{
    [EnableCors(origins: "*", headers: "*", methods: "*")]
    public class AdminCouponController : ApiController
    {
        #region Coupon
        [HttpPost]
        [Route("AdminCoupon/AddCoupon")]
        public async Task<HttpResponseMessage> AddCoupon([FromBody] cat_Coupons json)
        {
            Dictionary<string, object> dict = new Dictionary<string, object>();
            Entities entity = new Entities();
            HttpStatusCode statusCode = HttpStatusCode.BadRequest;

            try
            {
                var coupon = new cat_Coupons()
                {
                    Coupon_Code = json.Coupon_Code,
                    Coupon_Amount = json.Coupon_Amount,
                    Coupon_Discount = json.Coupon_Discount,
                    Coupon_Creation_Date = DateTime.Now,
                    Coupon_Expiration_Date = json.Coupon_Expiration_Date,
                    Coupon_Use_Date = null,
                    Specific_Rule_Json_Config = json.Specific_Rule_Json_Config
                };

                entity.cat_Coupons.Add(coupon);
                entity.SaveChanges();

                statusCode = HttpStatusCode.OK;
                dict.Add("message", "Coupon added successfully");
            }
            catch (Exception ex)
            {
                dict.Add("message", ex.Message);
            }

            await Task.CompletedTask;
            return Request.CreateResponse(statusCode, dict);
        }

        [HttpPost]
        [Route("AdminCoupon/UpdateCoupon")]
        public async Task<HttpResponseMessage> UpdateCoupon([FromBody] cat_Coupons json)
        {
            Dictionary<string, object> dict = new Dictionary<string, object>();
            Entities entity = new Entities();
            HttpStatusCode statusCode = HttpStatusCode.BadRequest;

            try
            {
                var coupon = entity.cat_Coupons.SingleOrDefault(x => x.Coupon_Id == json.Coupon_Id);

                coupon.Coupon_Code = json.Coupon_Code;
                coupon.Coupon_Amount = json.Coupon_Amount;
                coupon.Coupon_Discount = json.Coupon_Discount;
                coupon.Coupon_Expiration_Date = json.Coupon_Expiration_Date;
                coupon.Specific_Rule_Json_Config = json.Specific_Rule_Json_Config;
                entity.SaveChanges();   

                statusCode = HttpStatusCode.OK;
                dict.Add("message", "Coupon updated successfully");
            }
            catch (Exception ex)
            {
                dict.Add("message", ex.Message);
            }

            await Task.CompletedTask;
            return Request.CreateResponse(statusCode, dict);
        }

        [HttpPost]
        [Route("AdminCoupon/DeleteCoupon")]
        public async Task<HttpResponseMessage> DeleteCoupon([FromBody] cat_Coupons json)
        {
            Dictionary<string, object> dict = new Dictionary<string, object>();
            Entities entity = new Entities();
            HttpStatusCode statusCode = HttpStatusCode.BadRequest;

            try
            {
                var coupon = entity.cat_Coupons.SingleOrDefault(x => x.Coupon_Id == json.Coupon_Id);

                entity.cat_Coupons.Remove(coupon);
                entity.SaveChanges();

                statusCode = HttpStatusCode.OK;
                dict.Add("message", "Coupon deleted successfully");
            }
            catch (Exception ex)
            {
                dict.Add("message", ex.Message);
            }

            await Task.CompletedTask;
            return Request.CreateResponse(statusCode, dict);
        }

        [HttpGet]
        [Route("AdminCoupon/GetRandomCoupon")]
        public async Task<HttpResponseMessage> GetRandomCoupon()
        {
            Dictionary<string, object> dict = new Dictionary<string, object>();
            Entities entity = new Entities();
            HttpStatusCode statusCode = HttpStatusCode.BadRequest;

            try
            {
                dict.Add("CouponCode", CouponUtils.CreateCouponCode());
                statusCode = HttpStatusCode.OK;
            }
            catch (Exception ex)
            {
                dict.Add("message", ex.Message);
            }

            await Task.CompletedTask;
            return Request.CreateResponse(statusCode, dict);
        }

        [HttpGet]
        [Route("AdminCoupon/GetCoupons")]
        public async Task<List<cat_Coupons>> GetCoupons()
        {
            Entities entity = new Entities();

            await Task.CompletedTask;
            return entity.cat_Coupons.ToList();
        }

        [HttpGet]
        [Route("AdminCoupon/GetCoupon/{Coupon_Id}")]
        public async Task<cat_Coupons> GetCoupon(int Coupon_Id)
        {
            Entities entity = new Entities();

            await Task.CompletedTask;
            return entity.cat_Coupons.FirstOrDefault(x => x.Coupon_Id == Coupon_Id);
        }

        [HttpGet]
        [Route("AdminCoupon/GetCouponRules")]
        public async Task<List<cat_Specific_Rules>> GetCouponRules()
        {
            Entities entity = new Entities();

            await Task.CompletedTask;
            return entity.cat_Specific_Rules.ToList();
        }

        [HttpGet]
        [Route("AdminCoupon/GetBanks")]
        public async Task<List<cat_Banks>> GetBanks()
        {
            Entities entity = new Entities();

            await Task.CompletedTask;
            return entity.cat_Banks.ToList();
        }
        #endregion
    }
}
