using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using UnityEngine;

/*
MIT License

Copyright (c) 2023 Filipe Lopes | FMFCLPRO

Permission is hereby granted, free of charge, to any person obtaining a copy
of this software and associated documentation files (the "Software"), to deal
in the Software without restriction, including without limitation the rights
to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
copies of the Software, and to permit persons to whom the Software is
furnished to do so, subject to the following conditions:

The above copyright notice and this permission notice shall be included in all
copies or substantial portions of the Software.

THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE
SOFTWARE.
*/

namespace NoodleLand.Serialization.BDS
{
    public class SavingException : Exception
    {
        public override string Message => " ailed to save";
    }
 
    /// <summary>
    ///  Lusitano Data Saving Dictionary
    /// </summary>
    [SerializeField]
    public class LDSDictionary
    {
        [JsonProperty] private Dictionary<string, object> _map;

        public LDSDictionary()
        {
            _map  = new();
        }
      
        // Getters
        public T? Get<T>(string key) where T : unmanaged
        {
            try
            {
                return (T) _map[key];
            }  
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
          
        }

       
        public string? GetString(string key) 
        {
            try
            {
                return (string) _map[key];
            }  
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
          
        }

        #region Unity Related

        public Vector3 GetVector3(string key)
        {
            try
            {
                float[] arr = GetArray<float>(key);
                return new Vector3(arr[0], arr[1], arr[2]);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
            
        }
        public void SetVector3(string key,Vector3 vector3)
        {
            float x = vector3.x;
            float y = vector3.y;
            float z = vector3.z;

            float[] arr = {x, y, z};
            SetArray(key,arr);
        }
        

        #endregion
        // Setters
        public void SetString(string key, string value)
        {
            _map.Add(key,value);
        }

        public void SetList<T>(string key, List<T> values) where T : unmanaged
        {
            _map.Add(key,values);
        }
        public void SetStringList(string key, List<string> values) 
        {
            _map.Add(key,values);
        }

        public List<string> GetStringList(string key) 
        {
            JArray array = _map[key] as JArray;
            List<string> arr = array.ToObject<List<string>>();
            return arr;
           
        }

        public List<T> GetList<T>(string t) where T : unmanaged
        {
            JArray array = _map[t] as JArray;
            List<T> arr = array.ToObject<List<T>>();
            return arr;
        }

        public T[] GetArray<T>(string t) where T : unmanaged
        {
            JArray array = _map[t] as JArray;
            T[] arr = array.ToObject<T[]>();
            return arr;
        }
        
        public void SetArray<T>(string key, T[] values) where T : unmanaged
        {
            _map.Add(key,values);
        }
     
        public void Set<T>(string key, T value) where T : unmanaged
        {
            _map[key] = value;
        }

    }
}