using System;
using System.Collections.Generic;
using System.Text;

namespace ShoppingP6_JoshepFernandez.Services
{
    public static class CnnToP6API
    {

        //en esta clase se definen las rutas de consuma del API
        //ademas se define la infocde API Key necesaria
        //para poder consumir los controladores.

        public static string ProductionURL = "http://192.168.1.109:45457/api/";
        public static string TestingURL = "http://192.168.1.109:45457/api/";



        public static string ApiKeyName = "P6ApiKey";
        public static string ApiKeyValue = "P6shoppingQwerty1234/*";

    }
}
