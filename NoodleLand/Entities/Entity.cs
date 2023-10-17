using NoodleLand.Farming;
using NoodleLand.Serialization.BDS;
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

namespace NoodleLand.Entities
{
    public class Entity : MonoBehaviour
    {
        [SerializeField] private string entityTag;
        [SerializeField] private bool forceRegister;
        public World World { get; private set; }


        public void RemoveForceRegister()
        {
            forceRegister = false;
        }

        protected virtual void Awake()
        {
            World = FindObjectOfType<World>();
        }

        protected virtual void Start()
        {
            if (forceRegister)
            {
                World.AddEntity(this, transform.position);
            }
        }

        public string EntityTag => entityTag;

        public void SaveEntity(LDSDictionary ldsDictionary)
        {
            ldsDictionary.SetVector3("pos", transform.position);
            ldsDictionary.SetString("id", entityTag);
            CustomSaveEntity(ldsDictionary);
        }

        protected virtual void CustomSaveEntity(LDSDictionary ldsDictionary)
        {
        }

        protected virtual void CustomLoadEntity(LDSDictionary ldsDictionary)
        {
        }

        public void LoadEntity(LDSDictionary ldsDictionary)
        {
            transform.position = ldsDictionary.GetVector3("pos");
            Debug.Log($"{entityTag} __ World");
            World.AddEntity(this, transform.position);
            CustomLoadEntity(ldsDictionary);
        }
    }
}