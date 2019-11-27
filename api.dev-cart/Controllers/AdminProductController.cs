﻿using api.dev_cart.Entity;
using api.dev_cart.Models;
using Newtonsoft.Json;
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
    public class AdminProductController : ApiController
    {
        #region Categories
        [HttpPost]
        [Route("AdminProduct/AddCategory")]
        public async Task<HttpResponseMessage> AddCategory([FromBody] cat_Categories json)
        {
            Dictionary<string, object> dict = new Dictionary<string, object>();
            Entities entity = new Entities();
            HttpStatusCode statusCode = HttpStatusCode.BadRequest;

            try
            {
                var category = new cat_Categories()
                {
                    Category_Name = json.Category_Name
                };

                entity.cat_Categories.Add(category);
                entity.SaveChanges();

                statusCode = HttpStatusCode.OK;
                dict.Add("message", "Category added successfully");
            }
            catch (Exception ex)
            {
                dict.Add("message", ex.Message);
            }

            await Task.CompletedTask;
            return Request.CreateResponse(statusCode, dict);
        }

        [HttpPost]
        [Route("AdminProduct/UpdateCategory")]
        public async Task<HttpResponseMessage> UpdateCategory([FromBody] cat_Categories json)
        {
            Dictionary<string, object> dict = new Dictionary<string, object>();
            Entities entity = new Entities();
            HttpStatusCode statusCode = HttpStatusCode.BadRequest;

            try
            {
                var category = entity.cat_Categories.SingleOrDefault(x => x.Category_Id == json.Category_Id);

                category.Category_Name = json.Category_Name;
                entity.SaveChanges();

                statusCode = HttpStatusCode.OK;
                dict.Add("message", "Category updated successfully");
            }
            catch (Exception ex)
            {
                dict.Add("message", ex.Message);
            }

            await Task.CompletedTask;
            return Request.CreateResponse(statusCode, dict);
        }

        [HttpPost]
        [Route("AdminProduct/DeleteCategory")]
        public async Task<HttpResponseMessage> DeleteCategory([FromBody] cat_Categories json)
        {
            Dictionary<string, object> dict = new Dictionary<string, object>();
            Entities entity = new Entities();
            HttpStatusCode statusCode = HttpStatusCode.BadRequest;

            try
            {
                var category = entity.cat_Categories.SingleOrDefault(x => x.Category_Id == json.Category_Id);

                entity.cat_Categories.Remove(category);
                entity.SaveChanges();

                statusCode = HttpStatusCode.OK;
                dict.Add("message", "Category deleted successfully");
            }
            catch (Exception ex)
            {
                dict.Add("message", ex.Message);
            }

            await Task.CompletedTask;
            return Request.CreateResponse(statusCode, dict);
        }

        [HttpGet]
        [Route("AdminProduct/GetCategories")]
        public async Task<List<cat_Categories>> GetCategories()
        {
            Entities entity = new Entities();

            await Task.CompletedTask;
            return entity.cat_Categories.ToList();
        }
        #endregion

        #region Products
        [HttpPost]
        [Route("AdminProduct/AddProduct")]
        public async Task<HttpResponseMessage> AddProduct()
        {
            Dictionary<string, object> dict = new Dictionary<string, object>();
            Entities entity = new Entities();
            HttpStatusCode statusCode = HttpStatusCode.BadRequest;
            List<string> filenames = new List<string>();

            try
            {
                string jsonString = HttpContext.Current.Request.Form["cat_Products"];
                cat_Products json = JsonConvert.DeserializeObject<cat_Products>(jsonString);

                FileUtils.UploadImage(HttpContext.Current.Request, "ProductImages", ref statusCode, ref dict, ref filenames);

                var product = new cat_Products()
                {
                    Product_Name = json.Product_Name,
                    Product_Price = json.Product_Price,
                    Product_Disscount = json.Product_Disscount,
                    Category_Id = json.Category_Id,
                    Product_Img = "ProductImages/" + filenames[0],
                    Product_Description = json.Product_Description,
                    Product_Configurations = json.Product_Configurations,
                    Product_Creation_Date = DateTime.Now,
                    Product_Released = json.Product_Released,
                    Product_Stock = json.Product_Stock
                };

                entity.cat_Products.Add(product);
                entity.SaveChanges();
            }
            catch (Exception ex)
            {
                dict.Add("message", ex.Message);
            }

            await Task.CompletedTask;
            return Request.CreateResponse(statusCode, dict);
        }

        [HttpPost]
        [Route("AdminProduct/UpdateProduct")]
        public async Task<HttpResponseMessage> UpdateProduct()
        {
            Dictionary<string, object> dict = new Dictionary<string, object>();
            Entities entity = new Entities();
            HttpStatusCode statusCode = HttpStatusCode.BadRequest;
            List<string> filenames = new List<string>();

            try
            {
                string jsonString = HttpContext.Current.Request.Form["cat_Products"];
                cat_Products json = JsonConvert.DeserializeObject<cat_Products>(jsonString);

                var product = entity.cat_Products.SingleOrDefault(x => x.Product_Id == json.Product_Id);

                if (HttpContext.Current.Request.Files.Count > 0)
                {
                    FileUtils.ReplaceFile(product.Product_Img, HttpContext.Current.Request, "ProductImages", ref statusCode, ref dict, ref filenames);
                    product.Product_Img = "ProductImages/" + filenames[0];
                }

                product.Product_Name = json.Product_Name;
                product.Product_Price = json.Product_Price;
                product.Product_Disscount = json.Product_Disscount;
                product.Category_Id = json.Category_Id;
                product.Product_Description = json.Product_Description;
                product.Product_Configurations = json.Product_Configurations;
                product.Product_Creation_Date = DateTime.Now;
                product.Product_Released = json.Product_Released;
                product.Product_Stock = json.Product_Stock;
                entity.SaveChanges();

                statusCode = HttpStatusCode.OK;
                if(dict.Keys.Count == 0)
                {
                    dict.Add("message", "Product updated successfully");
                }
            }
            catch (Exception ex)
            {
                if (dict.Keys.Count > 0)
                {
                    dict = new Dictionary<string, object>();
                    dict.Add("message", ex.Message);
                }
            }

            await Task.CompletedTask;
            return Request.CreateResponse(statusCode, dict);
        }

        [HttpPost]
        [Route("AdminProduct/DeleteProduct")]
        public async Task<HttpResponseMessage> DeleteProduct([FromBody] cat_Products json)
        {
            Dictionary<string, object> dict = new Dictionary<string, object>();
            Entities entity = new Entities();
            HttpStatusCode statusCode = HttpStatusCode.BadRequest;

            try
            {
                var product = entity.cat_Products.SingleOrDefault(x => x.Product_Id == json.Product_Id);

                FileUtils.DeleteFile(product.Product_Img);

                entity.cat_Products.Remove(product);
                entity.SaveChanges();

                statusCode = HttpStatusCode.OK;
                dict.Add("message", "Product deleted successfully");

            }
            catch (Exception ex)
            {
                dict.Add("message", ex.Message);
            }

            await Task.CompletedTask;
            return Request.CreateResponse(statusCode, dict);
        }

        [HttpGet]
        [Route("AdminProduct/GetProducts")]
        public async Task<List<vw_Products>> GetProducts()
        {
            Entities entity = new Entities();

            await Task.CompletedTask;
            return entity.vw_Products.OrderBy(x => x.Product_Creation_Date).ToList();
        }

        [HttpGet]
        [Route("AdminProduct/GetProduct/{Product_Id}")]
        public async Task<vw_Products> GetProduct(int Product_Id)
        {
            Entities entity = new Entities();

            await Task.CompletedTask;
            return entity.vw_Products.SingleOrDefault(x => x.Product_Id == Product_Id);
        }

        [HttpGet]
        [Route("AdminProduct/GetProducts/{Product_Ids}")]
        public async Task<List<vw_Products>> GetProducts(string Product_Ids)
        {
            Entities entity = new Entities();
            List<vw_Products> Products = new List<vw_Products>();

            List<int> Ids = Product_Ids.Split(',').Select(Int32.Parse).ToList();
            foreach (int id in Ids)
            {
                var product = entity.vw_Products.SingleOrDefault(x => x.Product_Id == id);
                if(product != null)
                {
                    Products.Add(product);
                }
            }

            await Task.CompletedTask;
            return Products;
        }

        [HttpGet]
        [Route("AdminProduct/GetAllProducts")]
        public async Task<List<vw_Products>> GetAllProducts()
        {
            Entities entity = new Entities();

            await Task.CompletedTask;
            return entity.vw_Products.Where(x => x.Product_Released == true).OrderBy(x => x.Product_Creation_Date).ToList();
        }

        [HttpGet]
        [Route("AdminProduct/GetDiscount_Products")]
        public async Task<List<vw_Products>> GetDiscount_Products()
        {
            Entities entity = new Entities();

            await Task.CompletedTask;
            return entity.vw_Products.Where(x => x.Product_Disscount != 0).OrderBy(x => x.Product_Creation_Date).ToList();
        }

        [HttpGet]
        [Route("AdminProduct/GetNew_Products")]
        public async Task<List<vw_Products>> GetNew_Products()
        {
            Entities entity = new Entities();

            await Task.CompletedTask;
            return entity.vw_Products.Where(x => x.Product_Is_New == true && x.Product_Released == true).OrderBy(x => x.Product_Creation_Date).ToList();
        }

        [HttpGet]
        [Route("AdminProduct/GetSoon_Products")]
        public async Task<List<vw_Products>> GetSoon_Products()
        {
            Entities entity = new Entities();

            await Task.CompletedTask;
            return entity.vw_Products.Where(x => x.Product_Released == false).OrderBy(x => x.Product_Creation_Date).ToList();
        }

        [HttpGet]
        [Route("AdminProduct/Search_Products/{Word}")]
        public async Task<List<vw_Products>> Search_Products(string Word)
        {
            Entities entity = new Entities();

            Word = Word.Replace("+", " ");
            if(Word.ToLower()[Word.Length - 1] == 's' && Word.Length > 1)
            {
                Word = Word.Substring(0, Word.Length - 2);
            }

            await Task.CompletedTask;
            return entity.vw_Products.Where(
                x =>
                    x.Product_Name.Contains(Word) ||
                    x.Category_Name.Contains(Word) ||
                    x.Product_Description.Contains(Word)
                    &&
                    x.Product_Released == false
                ).OrderBy(x => x.Product_Creation_Date).ToList();
        }
        #endregion

        #region Cart
        [HttpPost]
        [Route("AdminProduct/AddCart")]
        public async Task<HttpResponseMessage> AddCart([FromBody] cat_Carts json)
        {
            Entities entity = new Entities();
            HttpStatusCode statusCode = HttpStatusCode.BadRequest;

            var kart = new cat_Carts()
            {
                Cart_Json_Config = json.Cart_Json_Config,
                Cart_Creation_Date = DateTime.Now
            };

            entity.cat_Carts.Add(kart);
            entity.SaveChanges();

            statusCode = HttpStatusCode.OK;

            await Task.CompletedTask;
            return Request.CreateResponse(statusCode, kart);
        }

        [HttpPost]
        [Route("AdminProduct/UpdateCart")]
        public async Task<HttpResponseMessage> UpdateCart([FromBody] cat_Carts json)
        {
            Dictionary<string, object> dict = new Dictionary<string, object>();
            Entities entity = new Entities();
            HttpStatusCode statusCode = HttpStatusCode.BadRequest;

            try
            {
                var kart = entity.cat_Carts.SingleOrDefault(x => x.Cart_Id == json.Cart_Id);

                kart.Cart_Json_Config = json.Cart_Json_Config;
                entity.SaveChanges();

                statusCode = HttpStatusCode.OK;
                dict.Add("message", "Cart updated successfully");
            }
            catch (Exception ex)
            {
                dict.Add("message", ex.Message);
            }

            await Task.CompletedTask;
            return Request.CreateResponse(statusCode, dict);
        }

        [HttpPost]
        [Route("AdminProduct/DeleteCart")]
        public async Task<HttpResponseMessage> DeleteCart([FromBody] int Cart_Id)
        {
            Dictionary<string, object> dict = new Dictionary<string, object>();
            Entities entity = new Entities();
            HttpStatusCode statusCode = HttpStatusCode.BadRequest;

            try
            {
                var kart = entity.cat_Carts.SingleOrDefault(x => x.Cart_Id == Cart_Id);

                entity.cat_Carts.Remove(kart);
                entity.SaveChanges();

                statusCode = HttpStatusCode.OK;
                dict.Add("message", "Cart deleted successfully");
            }
            catch (Exception ex)
            {
                dict.Add("message", ex.Message);
            }

            await Task.CompletedTask;
            return Request.CreateResponse(statusCode, dict);
        }

        [HttpGet]
        [Route("AdminProduct/GetCart/{Cart_Id}")]
        public async Task<List<cat_Carts>> GetCart(int Cart_Id)
        {
            Entities entity = new Entities();

            await Task.CompletedTask;
            return entity.cat_Carts.Where(x => x.Cart_Id == Cart_Id).ToList();
        }
        #endregion

        #region Orders
        [HttpPost]
        [Route("AdminProduct/UpdateOrder")]
        public async Task<HttpResponseMessage> UpdateOrder([FromBody] cat_Orders json)
        {
            Dictionary<string, object> dict = new Dictionary<string, object>();
            Entities entity = new Entities();
            HttpStatusCode statusCode = HttpStatusCode.BadRequest;

            try
            {
                var order = entity.cat_Orders.SingleOrDefault(x => x.Order_Id == json.Order_Id);

                order.Cart_Id = json.Cart_Id;
                order.Order_Client_Name = json.Order_Client_Name;
                order.Order_Client_Email = json.Order_Client_Email;
                order.Order_Client_Phone = json.Order_Client_Phone;
                order.Order_Client_Address1 = json.Order_Client_Address1;
                order.Order_Client_Address2 = json.Order_Client_Address2;
                order.Order_Client_Province = json.Order_Client_Province;
                order.Order_Client_City = json.Order_Client_City;
                order.Order_Client_Zip = json.Order_Client_Zip;
                order.Order_Client_Comments = json.Order_Client_Comments;
                order.Order_Creation_Date = DateTime.Now;
                order.Order_Delivered_Date = json.Order_Delivered_Date;
                order.Order_Tracking_Id = json.Order_Tracking_Id;
                entity.SaveChanges();

                statusCode = HttpStatusCode.OK;
                dict.Add("message", "Order updated successfully");
            }
            catch (Exception ex)
            {
                dict.Add("message", ex.Message);
            }

            await Task.CompletedTask;
            return Request.CreateResponse(statusCode, dict);
        }

        [HttpPost]
        [Route("AdminProduct/SendTrackingId")]
        public async Task<HttpResponseMessage> SendTrackingId([FromBody] TrackingInformation json)
        {
            Dictionary<string, object> dict = new Dictionary<string, object>();
            Entities entity = new Entities();
            HttpStatusCode statusCode = HttpStatusCode.BadRequest;

            try
            {
                var order = entity.cat_Orders.SingleOrDefault(x => x.Order_Id == json.Order_Id);
                order.Order_Tracking_Id = json.TrackingId;
                entity.SaveChanges();

                MailingUtils.SendTrackingIdEmail(json.ClientEmail, json.TrackingId, json.ShippingService);

                statusCode = HttpStatusCode.OK;
                dict.Add("message", "TrackingId sent successfully");
            }
            catch (Exception ex)
            {
                dict.Add("message", ex.Message);
            }

            await Task.CompletedTask;
            return Request.CreateResponse(statusCode, dict);
        }

        [HttpPost]
        [Route("AdminProduct/DeleteOrder")]
        public async Task<HttpResponseMessage> DeleteOrder([FromBody] int Order_Id)
        {
            Dictionary<string, object> dict = new Dictionary<string, object>();
            Entities entity = new Entities();
            HttpStatusCode statusCode = HttpStatusCode.BadRequest;

            try
            {
                var order = entity.cat_Orders.SingleOrDefault(x => x.Order_Id == Order_Id);

                entity.cat_Orders.Remove(order);
                entity.SaveChanges();

                statusCode = HttpStatusCode.OK;
                dict.Add("message", "Order deleted successfully");
            }
            catch (Exception ex)
            {
                dict.Add("message", ex.Message);
            }

            await Task.CompletedTask;
            return Request.CreateResponse(statusCode, dict);
        }

        [HttpGet]
        [Route("AdminProduct/GetOders")]
        public async Task<List<vw_Orders>> GetOders()
        {
            Entities entity = new Entities();

            var orders = entity.vw_Orders.ToList();

            foreach (var order in orders)
            {
                order.Order_Payment_Status = PaymentUtils.GetPaymentStatus(order.Order_Openpay_ChargeId);
            }

            await Task.CompletedTask;
            return orders;
        }
        #endregion

        #region Product_Galery_Images
        [HttpPost]
        [Route("AdminProduct/AddProductGaleryImage")]
        public async Task<HttpResponseMessage> AddProductGaleryImage()
        {
            Dictionary<string, object> dict = new Dictionary<string, object>();
            Entities entity = new Entities();
            HttpStatusCode statusCode = HttpStatusCode.BadRequest;
            List<string> filenames = new List<string>();

            try
            {
                string jsonString = HttpContext.Current.Request.Form["cat_Product_Galery_Images"];
                cat_Product_Galery_Images json = JsonConvert.DeserializeObject<cat_Product_Galery_Images>(jsonString);

                FileUtils.UploadImage(HttpContext.Current.Request, "ProductGaleryImageImages", ref statusCode, ref dict, ref filenames);

                var productGaleryImage = new cat_Product_Galery_Images()
                {
                    Product_Id = json.Product_Id,
                    Product_Galery_Image_Img = "ProductGaleryImageImages/" + filenames[0],
                    Product_Galery_Image_Order = json.Product_Galery_Image_Order
                };

                entity.cat_Product_Galery_Images.Add(productGaleryImage);
                entity.SaveChanges();
            }
            catch (Exception ex)
            {
                dict.Add("message", ex.Message);
            }

            await Task.CompletedTask;
            return Request.CreateResponse(statusCode, dict);
        }

        [HttpPost]
        [Route("AdminProduct/UpdateProductGaleryImage")]
        public async Task<HttpResponseMessage> UpdateProductGaleryImage()
        {
            Dictionary<string, object> dict = new Dictionary<string, object>();
            Entities entity = new Entities();
            HttpStatusCode statusCode = HttpStatusCode.BadRequest;
            List<string> filenames = new List<string>();

            try
            {
                string jsonString = HttpContext.Current.Request.Form["cat_Product_Galery_Images"];
                cat_Product_Galery_Images json = JsonConvert.DeserializeObject<cat_Product_Galery_Images>(jsonString);

                var productGaleryImage = entity.cat_Product_Galery_Images.SingleOrDefault(x => x.Product_Galery_Image_Id == json.Product_Galery_Image_Id);

                if (HttpContext.Current.Request.Files.Count > 0)
                {
                    FileUtils.ReplaceFile(productGaleryImage.Product_Galery_Image_Img, HttpContext.Current.Request, "ProductGaleryImageImages", ref statusCode, ref dict, ref filenames);
                    productGaleryImage.Product_Galery_Image_Img = "ProductGaleryImageImages/" + filenames[0];
                }

                productGaleryImage.Product_Galery_Image_Order = json.Product_Galery_Image_Order;
                entity.SaveChanges();

                statusCode = HttpStatusCode.OK;
                if (dict.Keys.Count == 0)
                {
                    dict.Add("message", "Product updated successfully");
                }
            }
            catch (Exception ex)
            {
                if (dict.Keys.Count > 0)
                {
                    dict = new Dictionary<string, object>();
                    dict.Add("message", ex.Message);
                }
            }

            await Task.CompletedTask;
            return Request.CreateResponse(statusCode, dict);
        }

        [HttpPost]
        [Route("AdminProduct/DeleteProductGaleryImage")]
        public async Task<HttpResponseMessage> DeleteProductGaleryImage([FromBody] cat_Product_Galery_Images json)
        {
            Dictionary<string, object> dict = new Dictionary<string, object>();
            Entities entity = new Entities();
            HttpStatusCode statusCode = HttpStatusCode.BadRequest;

            try
            {
                var productGaleryImage = entity.cat_Product_Galery_Images.SingleOrDefault(x => x.Product_Galery_Image_Id == json.Product_Galery_Image_Id);

                FileUtils.DeleteFile(productGaleryImage.Product_Galery_Image_Img);

                entity.cat_Product_Galery_Images.Remove(productGaleryImage);
                entity.SaveChanges();

                statusCode = HttpStatusCode.OK;
                dict.Add("message", "Product galery image deleted successfully");

            }
            catch (Exception ex)
            {
                dict.Add("message", ex.Message);
            }

            await Task.CompletedTask;
            return Request.CreateResponse(statusCode, dict);
        }

        [HttpGet]
        [Route("AdminProduct/GetProductGaleryImages/{Product_Id}")]
        public async Task<List<cat_Product_Galery_Images>> GetProductGaleryImages(int Product_Id)
        {
            Entities entity = new Entities();

            await Task.CompletedTask;
            return entity.cat_Product_Galery_Images.Where(x => x.Product_Id == Product_Id).OrderBy(x => x.Product_Galery_Image_Order).ToList();
        }
        #endregion
    }
}
