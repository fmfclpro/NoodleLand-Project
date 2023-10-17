using FMFCLPRO.MOBILE.Input;
using NoodleLand.Data.Items;
using NoodleLand.Entities.Item;
using NoodleLand.Events;
using NoodleLand.Inventory.Items;
using NoodleLand.Registeries;
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

namespace NoodleLand.Entities.GridEntities
{
    public class RockEntity : GridEntity
    {
        public override void OnInteract(OnInteractEnterEvent onInteractEnterEvent)
        {
            base.OnInteract(onInteractEnterEvent);

            if (onInteractEnterEvent.analogUsed == AnalogType.A)
            {
                StackableItem handStackableItem = onInteractEnterEvent.player.HandStackableItem;
                if(handStackableItem != null)
                {

                    if (handStackableItem.BaseItemData.ItemTag == "Flint")
                    {
                       handStackableItem.RemoveFromStack(1);
                       
                       //todo MAKE OBJECT POOL INSTEAD
                       ItemEntity e0 = Instantiate(FindObjectOfType<ItemEntity>());
                       BaseItemData it = RegisteredItems.Instance.Flint;

                       for (int i = 0; i < Random.Range(1,2); i++)
                       {
                           e0.Construct(new StackableItem(it,1));

                       }
                       World.AddEntity(e0, transform.position);
                       
                    }
                }
            }
         
        }
    }
}