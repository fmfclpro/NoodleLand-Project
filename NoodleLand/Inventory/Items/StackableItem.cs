using System;
using NoodleLand.Data.Items;
using NoodleLand.Events;
using NoodleLand.Events.Item;

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

namespace NoodleLand.Inventory.Items
{
    public class StackableItem
    {
        private BaseItemData _baseItemData;
        private int quantity;
        private bool isStackable;

        // public Action onSlotLeave;

        public Action UpdateUI;

        public BaseItemData BaseItemData => _baseItemData;
        public int Quantity => quantity;
        public bool IsStackable => isStackable;

        public void AddToStack(int amount)
        {
            quantity += amount;
            FakeEventBus.InvokeEvent(new ItemStackModifiedEvent(this));
            UpdateUI?.Invoke();
        }


        public void RemoveFromStack(int amount)
        {
            quantity -= amount;
            FakeEventBus.InvokeEvent(new ItemStackModifiedEvent(this));
            UpdateUI?.Invoke();
            // onStackModified?.Invoke();
        }

        public bool Split(out StackableItem stackableItem)
        {
            int toGive;

            if (quantity == 1)
            {
                stackableItem = null;
                return false;
            }

            toGive = quantity / 2;

            StackableItem st = new StackableItem(_baseItemData, toGive);
            stackableItem = st;
            quantity /= 2;
            UpdateUI?.Invoke();
            return true;
        }

        public StackableItem Copy()
        {
            return new StackableItem(_baseItemData, quantity);
        }

        public StackableItem(BaseItemData itemData, int quantity)
        {
            _baseItemData = itemData;
            isStackable = _baseItemData.IsStackable;
            this.quantity = quantity;
        }
    }
}