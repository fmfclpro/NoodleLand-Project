using System.Collections.Generic;
using NoodleLand.Data.Crops;
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


namespace NoodleLand.Farming.Crops
{
    
    [RequireComponent(typeof(SpriteRenderer))]
    public class CropInstance : MonoBehaviour
    {
        [SerializeField] private CropData _cropData;
        
        private SpriteRenderer _spriteRenderer;
        private FarmingAge _currentFarmingAge;
        private Dictionary<int, Sprite> age_to_sprite;
        private int currentAge = 0;
        private int maxAgeFase = 0;
        
        private void Awake()
        {
            Construct(_cropData);
        }

        public CropInstance Construct(CropData cropData)
        {

            _cropData = cropData;
            age_to_sprite = new Dictionary<int, Sprite>();
            currentAge = 0;
            
       
            for (var i = 0; i < cropData.FarmingAgeCount; i++)
            {
                FarmingAge farmingAge = cropData.FarmingFarmingAges[i];
             
                int age = farmingAge.Age;
                
                age_to_sprite.Add(age,farmingAge.Sprite);
                
                if (maxAgeFase < age)
                {
                    maxAgeFase = age;

                }

            }
            
        
            SetSpriteFromAge(0);
            
            return this;

        }

        private float k0;
        private void OnTick(World world)
        {
            k0 += Time.deltaTime;
            if (k0 >= 1)
            {
                int rn = UnityEngine.Random.Range(0, 100);
                if (rn <= 15)
                {
                    AgeUp();
                }
                k0 = 0;
            }

        }

        private void SetSpriteFromAge(int ageFase)
        {
            _spriteRenderer.sprite = age_to_sprite[ageFase];

        }
        public void AgeUp()
        {
            int next = currentAge + 1;
            
            if (next >= maxAgeFase)
            {
                // remove from tickers
                return;
            }

           // Debug.Log("age up");
            currentAge = next;
            SetSpriteFromAge(currentAge);

        }      
    }
}