﻿using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace Prototype.Database
{
    public static class LocalStorage
    {
        public static string SerializeToJson(object obj)
        {
            try
            {
                var json = JsonConvert.SerializeObject(obj);
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
                var result = JsonConvert.DeserializeObject<T>(jsonObj);
                return result;
            }
            catch(Exception ex)
            {
                throw;
            }
        }
    }
}
