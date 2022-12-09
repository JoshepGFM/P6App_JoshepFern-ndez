using Newtonsoft.Json;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace ShoppingP6_JoshepFernandez.Models
{
    public class Inventory
    {
        //Cuando usamos objetos de tranferencia de datos (DTO)
        //no se necesario que se llamen igual en el API y en el APP
        //En este caso el DTO en la API se llama: ItemDTO
        public RestRequest request { get; set; }
        const string mimetype = "application/json";
        const string contentType = "Content-Type";

        public int Id { get; set; }
        public string Name { get; set; } = null!;
        public string Description { get; set; } = null!;
        public string MainImageUrl { get; set; } = null!;
        public decimal Price { get; set; }
        public int Stock { get; set; }
        public string SKU { get; set; }
        public string ManufacturerNumber { get; set; }
        public string UPC { get; set; }
        public bool? IsActive { get; set; }
        public int IDStore { get; set; }
        public int IDCurrency { get; set; }

        public async Task<ObservableCollection<Inventory>> GetItemListFull()
        {

            try
            {

                string RouteSufix = string.Format("Inventories/GetItemList");
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
                    var list = JsonConvert.DeserializeObject<ObservableCollection<Inventory>>(response.Content);

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
