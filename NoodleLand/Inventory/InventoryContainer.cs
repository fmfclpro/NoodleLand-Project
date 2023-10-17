using NoodleLand.Data.Items;
using NoodleLand.Inventory.Items;
using NoodleLand.MessageHandling.Inventory;
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

namespace NoodleLand.Inventory
{
    public class InventoryContainer : MonoBehaviour
    {
        [SerializeField] private InventorySlot[] _inventorySlots;


        public GameObject[] UIObjectS;
        public InventorySlot[] Slots => _inventorySlots;
        private InventorySlot currentSelected;


        public bool HasSlot(InventorySlot invSlot)
        {
            for (var i = 0; i < _inventorySlots.Length; i++)
            {
                if (_inventorySlots[i] == invSlot)
                {
                    return true;
                }
            }

            return false;
        }

        public bool HasSpace()
        {
            for (var i = 0; i < _inventorySlots.Length; i++)
            {
                if (_inventorySlots[i].IsEmpty())
                {
                    return true;
                }
            }

            return false;
        }

        public InventoryMessage ForceAddInSlot(StackableItem stackableItem, int slot)
        {
            _inventorySlots[slot].ForceAddNewStack(stackableItem);
            return InventoryMessage.InventoryFull;
        }

        public InventoryMessage TryAdd(BaseItemData baseItem, int amount, bool substitute = true)
        {
            InventorySlot freeInventory = null;
            foreach (var inventorySlot in _inventorySlots)
            {
                if (inventorySlot.IsEmpty())
                {
                    if (freeInventory == null)
                        freeInventory = inventorySlot;
                    continue;
                }

                if (inventorySlot.IsSameItem(baseItem) && substitute)
                {
                    inventorySlot.AddToStack(amount);
                    return InventoryMessage.AddedToInventory;
                }
            }

            if (freeInventory != null)
            {
                freeInventory.ForceAddNewStack(new StackableItem(baseItem, amount));
                return InventoryMessage.AddedToInventory;
            }

            return InventoryMessage.InventoryFull;
        }

        public InventoryMessage TryAdd(StackableItem stackableItem, bool substitute = true)
        {
            return TryAdd(stackableItem.BaseItemData, stackableItem.Quantity, substitute);
        }

        private void UpdateAllSlotsUI()
        {
            for (var i = 0; i < _inventorySlots.Length; i++)
            {
                _inventorySlots[i].UpdateUI();
            }
        }

        public void OpenUI()
        {
            foreach (var o in UIObjectS)
            {
                o.SetActive(true);
            }

            UpdateAllSlotsUI();
        }


        public void CloseUI()
        {
            foreach (var o in UIObjectS)
            {
                o.SetActive(false);
            }
        }


        private void OnDestroy()
        {
        }
    }
}