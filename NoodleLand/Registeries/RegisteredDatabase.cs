using System.Collections.Generic;
using NoodleLand.Data.Databases;
using NoodleLand.Data.Entities;
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

namespace NoodleLand.Registeries
{
    public abstract class RegisteredDatabase<T,K>: MonoBehaviour where T : Database<K> where  K: IData
    {
        [SerializeField] private T database;
        private Dictionary<string, K> _tagToDrops = new Dictionary<string,K>();

        public int Count => database.values.Count;


        public T Database => database;
        
        protected virtual void Awake()
        {
            InitializeDatabase();
            
        }

        private void InitializeDatabase()
        {
            for (var i = 0; i < database.values.Count; i++)
            {
                K data = database.values[i];

                Debug.Log($"{data.GetTag()} has been registered into game");
                _tagToDrops[data.GetTag()] = data;

            }
        }

        public K Get(int index)
        {
            return database.values[index];
        }

        public K Get(string tag)
        {
            if(_tagToDrops.TryGetValue(tag,out K data))
            {
                return data;
            }

            return default;
        }
    }
}