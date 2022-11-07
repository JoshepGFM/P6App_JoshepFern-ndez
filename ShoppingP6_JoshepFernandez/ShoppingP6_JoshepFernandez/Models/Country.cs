using Newtonsoft.Json;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace ShoppingP6_JoshepFernandez.Models
{
    public class Country
    {
        public RestRequest request { get; set; }
        const string mimetype = "application/json";
        const string contentType = "Content-Type";

        public Country()
        {
            //Stores = new HashSet<Store>();
            //Users = new HashSet<User>();
        }

        public int Idcountry { get; set; }
        public int CountryCode { get; set; }
        public string CountryName { get; set; } = null!;
        public bool? Active { get; set; }

        //public virtual ICollection<Store> Stores { get; set; }
        //public virtual ICollection<User> Users { get; set; }

        public async Task<List<Country>> GetCountry()
        {

            try
            {

                string RouteSufix = string.Format("Countries");
                string FinalURL = Services.CnnToP6API.ProductionURL + RouteSufix;

                RestClient client = new RestClient(FinalURL);

                request = new RestRequest(FinalURL, Method.Get);

                //agregar la info de seguridad del api, en este caso ApiKey
                request.AddHeader(Services.CnnToP6API.ApiKeyName, Services.CnnToP6API.ApiKeyValue);
                request.AddHeader(contentType, mimetype);

                RestResponse response = await client.ExecuteAsync(request);

                HttpStatusCode statusCode = response.StatusCode;



                if (statusCode == HttpStatusCode.OK)
                {
                    //carga de la info en un json
                    var list = JsonConvert.DeserializeObject<List<Country>>(response.Content);
                    return list;
                }
                else
                {
                    return null;
                }
            }
            catch (Exception ex)
            {
                string msg = ex.Message;
                //TODO: guardar estos errores en una bitacora para su posterior analisis
                throw;
            }

        }
    }
}
