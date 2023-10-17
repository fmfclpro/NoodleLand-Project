using NoodleLand.Data.Databases;
using NoodleLand.Entities;
using UnityEditor;
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

namespace NoodleLand.Data.Entities
{
    public interface IData
    {
        public string GetTag();
    }

    [CreateAssetMenu(menuName = "NoodleLand/Data/Entities/Entities/New Entity Data", fileName = "New EntityData",
        order = 0)]
    public class EntityData : ScriptableObject, IData
    {
        [SerializeField] public Entity _entity;
        [SerializeField] private string entityTag;


        public string EntityTag => entityTag;
        public Entity Entity => _entity;

        private void Awake()
        {
            RegisterSelf();
        }

        private void RegisterSelf()
        {
            var objs = AssetDatabase.LoadAllAssetsAtPath(EntityDatabase.FolderPath);

            if (objs != null && objs.Length != 0)
            {
                foreach (var o in objs)
                {
                    if (o is EntityDatabase entityDatabase)
                    {
                        if (!entityDatabase.Has(this))
                            entityDatabase.Register(this);
                        return;
                    }
                }
            }
        }

        public string GetTag()
        {
            return entityTag;
        }
    }
}