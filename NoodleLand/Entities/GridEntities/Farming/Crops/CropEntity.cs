using System.Collections.Generic;
using NoodleLand.Data.Crops;
using NoodleLand.Farming;
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

namespace NoodleLand.Entities.GridEntities.Farming.Crops
{
    public class CropEntity : GridEntity, ITickable
    {
        [SerializeField] private CropData _cropData;
        
        private FarmingAge _currentFarmingAge;
        private Dictionary<int, Sprite> age_to_sprite;
        private int _currentAge = 0;
        private int _maxAgeFase = 0;

        protected override void Awake()
        {
            base.Awake();

            age_to_sprite = new Dictionary<int, Sprite>();

            for (var i = 0; i < _cropData.FarmingAgeCount; i++)
            {
                FarmingAge farmingAge = _cropData.FarmingFarmingAges[i];

                int age = farmingAge.Age;

                age_to_sprite.Add(age, farmingAge.Sprite);

                if (_maxAgeFase <= age)
                {
                    _maxAgeFase = age;
                }
            }

            _currentAge = 0;
            SetSpriteFromAge(0);
        }

        public override void OnSpawn()
        {
            base.OnSpawn();
            Construct();
        }

        public void Construct()
        {
            _currentAge = 0;
            SetSpriteFromAge(0);
        }

        private float ticks;


        private bool ReachedMaxAge()
        {
            return _currentAge == _maxAgeFase;
        }

        public void OnTick(World world)
        {
            if (ReachedMaxAge())
            {
                ChangeDropCondition(DropCondition.IsGrown, true);
                world.RemoveTickableObject(this);
                return;
            }

            ticks++;
            if (ticks >= 100)
            {
                ticks = 0;
                if (CanGrow())
                    AgeUp();
            }
        }

        public bool CanGrow()
        {
            return !ReachedMaxAge();
        }

        private void SetSpriteFromAge(int ageFase)
        {
            _spriteRenderer.sprite = age_to_sprite[ageFase];
        }

        public void AgeUp()
        {
            int next = _currentAge + 1;

            if (next > _maxAgeFase)
            {
                return;
            }

            // Debug.Log("age up");
            _currentAge++;
            SetSpriteFromAge(_currentAge);
        }

        protected override void OnConditionsInitialise(Dictionary<DropCondition, bool> drops)
        {
            base.OnConditionsInitialise(drops);
            drops.Add(DropCondition.IsGrown, false);
        }
    }
}