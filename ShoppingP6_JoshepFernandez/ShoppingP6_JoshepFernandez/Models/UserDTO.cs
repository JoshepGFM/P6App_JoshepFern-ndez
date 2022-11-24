using Newtonsoft.Json;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace ShoppingP6_JoshepFernandez.Models
{
    public class UserDTO
    {
        public RestRequest request { get; set; }
        const string mimetype = "application/json";
        const string contentType = "Content-Type";

        public int IdUsuario { get; set; }
        public string Nombre { get; set; } = null!;
        public string CorreoElectronico { get; set; } = null!;
        public string Contrasennia { get; set; } = null!;
        public string CorreoRespaldo { get; set; } = null!;
        public string NumeroTelefono { get; set; } = null!;
        public int IDRol { get; set; }
        public int IDPais { get; set; }
        public string RolDescripcion { get; set; } = null!;
        public string PaisNombre { get; set; } = null;

        //Funcion para actualizar la info de un usuario el DTO en lugar del objeto completo.
        public async Task<UserDTO> GetUserData(string email)
        {

            try
            {

                string RouteSufix = string.Format("Users/GetUserInfo?email={0}", email);
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
                    var list = JsonConvert.DeserializeObject<List<UserDTO>>(response.Content);

                    var item = list[0];

                    return item;
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

        public async Task<bool> UpdateUser()
        {

            try
            {

                string RouteSufix = string.Format("TODO");
                string FinalURL = Services.CnnToP6API.ProductionURL + RouteSufix;

                RestClient client = new RestClient(FinalURL);

                request = new RestRequest(FinalURL, Method.Put);//Normalmente se usa PUT(todo) o PATCH(una parte) para actualizar

                //agregar la info de seguridad del api, en este caso ApiKey
                request.AddHeader(Services.CnnToP6API.ApiKeyName, Services.CnnToP6API.ApiKeyValue);
                request.AddHeader(contentType, mimetype);

                //tenemos que serializar la clase para poder enviar al API
                string SerialClass = JsonConvert.SerializeObject(this);

                request.AddBody(SerialClass, mimetype);


                RestResponse response = await client.ExecuteAsync(request);

                HttpStatusCode statusCode = response.StatusCode;



                if (statusCode == HttpStatusCode.OK)
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
    }
}
