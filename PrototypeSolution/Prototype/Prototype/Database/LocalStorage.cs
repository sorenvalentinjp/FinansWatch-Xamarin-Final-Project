using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using Prototype.ModelControllers;

namespace Prototype.Database
{
    public static class LocalStorage
    {
        public static string SerializeToJson(object obj)
        {
            try
            {                
                var json = JsonConvert.SerializeObject(obj, Formatting.Indented,
                    new JsonSerializerSettings
                    {
                        PreserveReferencesHandling = PreserveReferencesHandling.Objects
                    });

                return json;

            }
            catch(Exception ex)
            {
                throw;
            }
        }

        public static T DeserializeFromJson<T>(string jsonObj)
        {
            try
            {
                var result = JsonConvert.DeserializeObject<T>(jsonObj,
                    new JsonSerializerSettings
                    {
                        PreserveReferencesHandling = PreserveReferencesHandling.Objects
                    });

                return result;
            }
            catch(Exception ex)
            {
                throw;
            }
        }
    }
}
