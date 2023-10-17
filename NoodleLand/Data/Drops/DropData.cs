using System.Collections.Generic;
using NoodleLand.Data.Databases;
using NoodleLand.Data.Entities;
using NoodleLand.Data.Items;
using NoodleLand.Entities.GridEntities;
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

namespace NoodleLand.Data.Drops
{
    [System.Serializable]
    public class DropProperty
    {
        [SerializeField] private BaseItemData drop;
        [Range(1, 10)] [SerializeField] private int quantity;

        [Header("If Random")] public bool isRandom;
        [SerializeField] private int minQuantity;
        [SerializeField] private int maxQuantity;
        [Header("Override if has conditions")] private bool hasConditions;
        private DropCondition _condition;
        private BaseItemData _conditionDrop;

        public BaseItemData ConditionDrop => _conditionDrop;

        public bool HasCondition => hasConditions;
        public DropCondition DropCondition => _condition;

        public bool IsRandom => isRandom;
        public int MinQuantity => minQuantity;
        public int MaxQuantity => maxQuantity;
        public BaseItemData Drop => drop;
        public int Quantity => quantity;
    }

    [CreateAssetMenu(menuName = "NoodleLand/Data/Drops/New Drop", fileName = "Drop", order = 0)]
    public class DropData : ScriptableObject, IData
    {
        [SerializeField] private List<DropProperty> drop;
        [SerializeField] private string tag;

        [Header("debug")] public bool forceRegister;

        public List<DropProperty> Drops => drop;
        public string Tag => tag;


        private void OnDestroy()
        {
            Debug.Log("called");
        }

        private void OnValidate()
        {
            if (string.IsNullOrEmpty(tag))
            {
                tag = name;
            }

            if (forceRegister)
            {
                forceRegister = false;
                RegisterSelf();
            }
        }

        private void RegisterSelf()
        {
            var objs = AssetDatabase.LoadAllAssetsAtPath(DropDatabase.FolderPath);
            tag = name;

            if (objs != null && objs.Length != 0)
            {
                foreach (var o in objs)
                {
                    if (o is DropDatabase dropDatabase)
                    {
                        if (!dropDatabase.Has(this))
                            dropDatabase.Register(this);
                        return;
                    }
                }
            }
        }

        private void Awake()
        {
            RegisterSelf();
        }

        public string GetTag()
        {
            return tag;
        }
    }
}