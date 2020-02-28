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
    public class ConfigurationsController : ApiController
    {
        #region About Us Sections
        [HttpPost]
        [Route("Configurations/AddConfiguration")]
        public async Task<HttpResponseMessage> AddConfiguration([FromBody] cat_Configurations json)
        {
            Dictionary<string, object> dict = new Dictionary<string, object>();
            Entities entity = new Entities();
            HttpStatusCode statusCode = HttpStatusCode.BadRequest;

            try
            {
                ConfigurationUtils.AddConfiguration(json.Configuration_Key, json.Configuration_Value);

                statusCode = HttpStatusCode.OK;
                dict.Add("message", "Configuration added successfully");
            }
            catch (Exception ex)
            {
                dict.Add("message", ex.Message);
            }

            await Task.CompletedTask;
            return Request.CreateResponse(statusCode, dict);
        }

        [HttpPost]
        [Route("Configurations/UpdateConfiguration")]
        public async Task<HttpResponseMessage> UpdateConfiguration([FromBody] cat_Configurations json)
        {
            Dictionary<string, object> dict = new Dictionary<string, object>();
            Entities entity = new Entities();
            HttpStatusCode statusCode = HttpStatusCode.BadRequest;

            try
            {
                ConfigurationUtils.EditConfiguration(json.Configuration_Key, json.Configuration_Value);

                statusCode = HttpStatusCode.OK;
                dict.Add("message", "Configuration updated successfully");
            }
            catch (Exception ex)
            {
                dict.Add("message", ex.Message);
            }

            await Task.CompletedTask;
            return Request.CreateResponse(statusCode, dict);
        }

        [HttpPost]
        [Route("Configurations/DeleteConfiguration")]
        public async Task<HttpResponseMessage> DeleteConfiguration([FromBody] cat_Configurations json)
        {
            Dictionary<string, object> dict = new Dictionary<string, object>();
            Entities entity = new Entities();
            HttpStatusCode statusCode = HttpStatusCode.BadRequest;

            try
            {
                ConfigurationUtils.DeleteConfiguration(json.Configuration_Key);

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
        [Route("Configurations/GetConfigurations")]
        public async Task<List<cat_Configurations>> GetConfigurations()
        {
            Entities entity = new Entities();

            await Task.CompletedTask;
            return entity.cat_Configurations.ToList();
        }

        [HttpGet]
        [Route("Configurations/GetConfiguration/{Configuration_Key}")]
        public async Task<cat_Configurations> GetConfiguration(string Configuration_Key)
        {
            Entities entity = new Entities();

            await Task.CompletedTask;
            return entity.cat_Configurations.FirstOrDefault(x => x.Configuration_Key == Configuration_Key);
        }
        #endregion
    }
}