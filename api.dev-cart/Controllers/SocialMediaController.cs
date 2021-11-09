using api.dev_cart.Entity;
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
    public class SocialMediaController : ApiController
    {
        #region Social Media
        [HttpPost]
        [Route("SocialMedia/AddSocialMedia")]
        public async Task<HttpResponseMessage> AddSocialMedia([FromBody] cat_Social_Media json)
        {
            Dictionary<string, object> dict = new Dictionary<string, object>();
            Entities entity = new Entities();
            HttpStatusCode statusCode = HttpStatusCode.BadRequest;

            try
            {
                var socialMedia = new cat_Social_Media()
                {
                    Social_Media_Name = json.Social_Media_Name,
                    Social_Media_Awesome_Font = json.Social_Media_Awesome_Font,
                    Social_Media_Url = json.Social_Media_Url,
                    Social_Media_Tab = json.Social_Media_Tab
                };

                entity.cat_Social_Media.Add(socialMedia);
                entity.SaveChanges();

                statusCode = HttpStatusCode.OK;
                dict.Add("message", "Section added successfully");
            }
            catch (Exception ex)
            {
                dict.Add("message", ex.Message);
            }

            await Task.CompletedTask;
            return Request.CreateResponse(statusCode, dict);
        }

        [HttpPost]
        [Route("SocialMedia/UpdateSocialMedia")]
        public async Task<HttpResponseMessage> UpdateSocialMedia([FromBody] cat_Social_Media json)
        {
            Dictionary<string, object> dict = new Dictionary<string, object>();
            Entities entity = new Entities();
            HttpStatusCode statusCode = HttpStatusCode.BadRequest;

            try
            {
                var socialMedia = entity.cat_Social_Media.SingleOrDefault(x => x.Social_Media_Id == json.Social_Media_Id);

                socialMedia.Social_Media_Name = json.Social_Media_Name;
                socialMedia.Social_Media_Awesome_Font = json.Social_Media_Awesome_Font;
                socialMedia.Social_Media_Url = json.Social_Media_Url;
                socialMedia.Social_Media_Tab = json.Social_Media_Tab;
                entity.SaveChanges();

                statusCode = HttpStatusCode.OK;
                dict.Add("message", "Section updated successfully");
            }
            catch (Exception ex)
            {
                dict.Add("message", ex.Message);
            }

            await Task.CompletedTask;
            return Request.CreateResponse(statusCode, dict);
        }

        [HttpPost]
        [Route("SocialMedia/DeleteSocialMedia")]
        public async Task<HttpResponseMessage> DeleteSocialMedia([FromBody] cat_Social_Media json)
        {
            Dictionary<string, object> dict = new Dictionary<string, object>();
            Entities entity = new Entities();
            HttpStatusCode statusCode = HttpStatusCode.BadRequest;

            try
            {
                var socialMedia = entity.cat_Social_Media.SingleOrDefault(x => x.Social_Media_Id == json.Social_Media_Id);

                entity.cat_Social_Media.Remove(socialMedia);
                entity.SaveChanges();

                statusCode = HttpStatusCode.OK;
                dict.Add("message", "Section deleted successfully");
            }
            catch (Exception ex)
            {
                dict.Add("message", ex.Message);
            }

            await Task.CompletedTask;
            return Request.CreateResponse(statusCode, dict);
        }

        [HttpGet]
        [Route("SocialMedia/GetSocialMedias")]
        public async Task<List<cat_Social_Media>> GetSocialMedias()
        {
            Entities entity = new Entities();

            await Task.CompletedTask;
            return entity.cat_Social_Media.ToList();
        }
        #endregion
    }
}
