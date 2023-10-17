using NoodleLand.Data.Items.Fuel;
using NoodleLand.Events;
using NoodleLand.Farming;
using NoodleLand.Inventory;
using UnityEngine;
using UnityEngine.UI;

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

namespace NoodleLand.Entities.GridEntities.Processors
{
    public class FurnaceEntity : ContainerEntity, ITickable
    {
        [SerializeField] private Image progressImage;

        private InventorySlot _input;
        private InventorySlot _output;
        private InventorySlot _fuel;

        protected override void Awake()
        {
            base.Awake();

            _input = container.Slots[0];
            _output = container.Slots[1];
            _fuel = container.Slots[2];
        }

        private bool _isOpened;
        public override void OnInteract(OnInteractEnterEvent onInteractEnterEvent)
        {
            base.OnInteract(onInteractEnterEvent);
            _isOpened = !_isOpened;
            if (_isOpened)
            {
                progressImage.gameObject.SetActive(true);
            }
            else
            {
                progressImage.gameObject.SetActive(false);
            }
        }

        private float _amountToTick;
        private bool _isBurning;

        //TODO MELHORAR ESTA PARTE
        public void OnTick(World world)
        {
            Debug.Log(!_fuel.IsEmpty() && _fuel.StackableItem.BaseItemData is FuelItem kappa);
          
            if (!_isBurning && !_fuel.IsEmpty() && _fuel.StackableItem.BaseItemData is FuelItem fuelItem)
            {
                Debug.Log("found fuel");
                float burnTimer = fuelItem.BurnTimer;
                _amountToTick = burnTimer;
                _fuel.StackableItem.RemoveFromStack(1);
                _isBurning = true;

            }

            if (_amountToTick == 0)
            {
                _isBurning = false;
                return;
            }

            if (_isOpened)
            {
                progressImage.gameObject.SetActive(true);
                progressImage.fillAmount += 1f / 100f;
            
                if(progressImage.fillAmount >= 1)
                {
                    progressImage.fillAmount = 0;
                }

            }

        }
    }
}
