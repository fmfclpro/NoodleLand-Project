using System.Collections.Generic;
using NoodleLand.Data.Items;
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

namespace NoodleLand.Entities.GridEntities.Storage
{
    [System.Serializable]
    public class ItemStackTest
    {
        [SerializeField] private BaseItemData _itemData;
        [SerializeField] private int quantity;

        public BaseItemData ItemData => _itemData;
        public int Quantity => quantity;
    }
    public class TestContainerEntity : ContainerEntity
    {
        public List<ItemStackTest> toAddToChest;

        protected override void Awake()
        {
            foreach (var itemStackTest in toAddToChest)
            {
                container.TryAdd(itemStackTest.ItemData,itemStackTest.Quantity);
            }
        }

        protected override void CustomSaveEntity(LDSDictionary ldsDictionary)
        {
           
        }

        protected override void CustomLoadEntity(LDSDictionary ldsDictionary)
        {
            
        }
    }
}