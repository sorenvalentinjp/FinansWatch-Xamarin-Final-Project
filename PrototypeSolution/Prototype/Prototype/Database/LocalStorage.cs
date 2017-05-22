using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Text;
using Prototype.ModelControllers;

namespace Prototype.Database
{
    /// <summary>
    /// This class serializing/deserializing of object to and from json.
    /// This is used when data needs to be stored locally on the user's device.
    /// </summary>
    public static class LocalStorage
    {
        /// <summary>
        /// Serializes an object to a string containing the object represented as json.
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
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
                Debug.WriteLine(@"ERROR {0}", ex.Message);
                throw;
            }
        }

        /// <summary>
        /// Deserializes a string containing an object represented as json.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="jsonObj"></param>
        /// <returns></returns>
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
            catch (Exception ex)
            {
                Debug.WriteLine(@"ERROR {0}", ex.Message);
                throw;
            }
        }
    }
}
