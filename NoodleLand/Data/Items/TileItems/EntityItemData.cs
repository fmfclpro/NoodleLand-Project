using NoodleLand.Entities.GridEntities;
using NoodleLand.Events;
using NoodleLand.Farming;
using UnityEngine;
using UnityEngine.Serialization;

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

namespace NoodleLand.Data.Items.TileItems
{
    [CreateAssetMenu(menuName = "NoodleLand/Data/Items/New NonLiving Entity Data", fileName = "NonLivingEntityItemData", order = 0)]
    public class EntityItemData : BaseItemData
    {

        [FormerlySerializedAs("nonLivingEntity")] [SerializeField] private GridEntity gridEntity;

        public override ActionMessage OnUse(OnUseEvent eOnUseEvent)
        {

            World world = eOnUseEvent.world;

            if (world.CanPlaceAt(eOnUseEvent.location))
            {
                GridEntity e0 = Instantiate(gridEntity);
                world.AddEntity(e0, eOnUseEvent.location);
                eOnUseEvent.Instance.RemoveFromStack(1);
                OnStackChange(eOnUseEvent.Instance,eOnUseEvent.PlayerEntity);
                return ActionMessage.Sucess;

            }

            return ActionMessage.Failluire;

        }
    }
}