using Newtonsoft.Json;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace ShoppingP6_JoshepFernandez.Models
{
    public class User
    {
        public RestRequest request { get; set; }
        const string mimetype = "application/json";
        const string contentType = "Content-Type";

        public User()
        {
            //Invoices = new HashSet<Invoice>();
            //UserStores = new HashSet<UserStore>();
        }

        public int Iduser { get; set; }
        public string Name { get; set; } = null!;
        public string Email { get; set; } = null!;
        public string UserPassword { get; set; } = null!;
        public string BackUpEmail { get; set; } = null!;
        public string SecurityCode { get; set; }
        public string PhoneNumber { get; set; } = null!;
        public bool? Active { get; set; }
        public int IduserRole { get; set; }
        public int Idcountry { get; set; }

        public virtual Country? IdcountryNavigation { get; set; } = null!;
        public virtual UserRole? IduserRoleNavigation { get; set; } = null!;

        //public virtual ICollection<Invoice> Invoices { get; set; }
        //public virtual ICollection<UserStore> UserStores { get; set; }

        //funcion para agregar usuario en la base de datos
        public async Task<bool> AddUser()
        {

            try
            {

                string RouteSufix = string.Format("Users");
                string FinalURL = Services.CnnToP6API.ProductionURL + RouteSufix;

                RestClient client = new RestClient(FinalURL);

                request = new RestRequest(FinalURL, Method.Post);

                //agregar la info de seguridad del api, en este caso ApiKey
                request.AddHeader(Services.CnnToP6API.ApiKeyName, Services.CnnToP6API.ApiKeyValue);
                request.AddHeader(contentType, mimetype);

                //tenemos que serializar la clase para poder enviar al API
                string SerialClass = JsonConvert.SerializeObject(this);

                request.AddBody(SerialClass, mimetype);


                RestResponse response = await client.ExecuteAsync(request);

                HttpStatusCode statusCode = response.StatusCode;



                if (statusCode == HttpStatusCode.Created)
                {
                    //carga de la info en un json
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch (Exception ex)
            {
                string msg = ex.Message;
                //TODO: guardar estos errores en una bitacora para su posterior analisis
                throw;
            }

        }

        public async Task<bool> ValidateLogin()
        {

            try
            {

                string RouteSufix = string.Format("Users/ValidateLogin?UserName={0}&UserPassword={1}", 
                                                  this.Email, this.UserPassword);
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
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch (Exception ex)
            {
                string msg = ex.Message;
                //TODO: guardar estos errores en una bitacora para su posterior analisis
                throw;
            }

        }

        public async Task<bool> ValidateConexion()
        {

            try
            {

                string RouteSufix = string.Format("Users");
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
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch (Exception ex)
            {
                string msg = ex.Message;
                throw;
            }

        }

    }
}
