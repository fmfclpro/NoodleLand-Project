using System.Collections.Generic;
using FMFCLPRO.MOBILE.Input;
using NoodleLand.Data.Items;
using NoodleLand.Entities.GridEntities;
using NoodleLand.Entities.Item;
using NoodleLand.Entities.Living.Player;
using NoodleLand.Events;
using NoodleLand.Inventory;
using NoodleLand.Inventory.Items;
using NoodleLand.Registeries;
using NoodleLand.Serialization.BDS;
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

public enum MaterialType
{
    None,
    Wood,
    Stone
}
public class ContainerEntity : GridEntity
{
    [FormerlySerializedAs("containerUI")] [SerializeField] protected InventoryContainer container;

    private bool isOpen;

    private void DropContainerToWorld()
    {
        for (var i = 0; i < container.Slots.Length; i++)
        {
            InventorySlot slot = container.Slots[i];
            if (!slot.IsEmpty())
            {
                ItemEntity g0 = Instantiate(FindObjectOfType<ItemEntity>(), transform.position,
                    Quaternion.identity);
                g0.Construct(slot.StackableItem);
            }

            slot.RemoveStack();
        }
    }

    protected override void CustomSaveEntity(LDSDictionary ldsDictionary)
    {
       

        List<int> quantities = new List<int>();
        List<string> itemTags = new List<string>();
        List<int> positionAt = new List<int>();

        for (var i = 0; i < container.Slots.Length; i++)
        {
            if (!container.Slots[i].IsEmpty())
            {
                StackableItem stackableItem = container.Slots[i].StackableItem;
                quantities.Add(stackableItem.Quantity);
                itemTags.Add(stackableItem.BaseItemData.ItemTag);
                positionAt.Add(i);

            }
          
        }
        
        ldsDictionary.SetList("quantities",quantities);
        ldsDictionary.SetList("position",positionAt);
        ldsDictionary.SetStringList("itemTags",itemTags);
    }

    protected override void CustomLoadEntity(LDSDictionary ldsDictionary)
    {

        List<int> quantities = ldsDictionary.GetList<int>("quantities");
        List<string> itemTags = ldsDictionary.GetStringList("itemTags");
        List<int> positionsAt = ldsDictionary.GetList<int>("position");
        
        for (var i = 0; i < quantities.Count; i++)
        {
            BaseItemData itemData = FindObjectOfType<RegisteredItems>().Get(itemTags[i]);
            StackableItem instance = new StackableItem( itemData,quantities[i]);

            int positionAt = positionsAt[i];

            container.ForceAddInSlot(instance, positionAt);
        }



    }

    protected override void OnDamageTaken(GameObject source)
    {
        PlayerEntity player = source.GetComponent<PlayerEntity>();
        if (player != null)
        {
            OnLeave(new OnInteractLeaveEvent(player));
        }
    }

    protected override void OnObjectDeath(bool shouldDropItems = true)
    {
        base.OnObjectDeath(shouldDropItems);
        
        DropContainerToWorld();

        
        
    }
    
    

    public override void OnInteract(OnInteractEnterEvent onInteractEnterEvent)
    {
        base.OnInteract(onInteractEnterEvent);
        
        
        if (onInteractEnterEvent.analogUsed == AnalogType.B)
        {
            
            Debug.Log("b was used");
            if (isOpen)
            {
                OnLeave(new OnInteractLeaveEvent(onInteractEnterEvent.player));
                return;
            }

            InventoryEvent.InformInventoriesOpen(onInteractEnterEvent.player,new List<InventoryContainer>(){container,onInteractEnterEvent.player.HandInventory});
            isOpen = true;
            container.OpenUI();
        }

    }


    public override void OnLeave(OnInteractLeaveEvent onInteractEnterEvent)
    {
        InventoryEvent.InformInventoryCancel();
        InventoryEvent.InformInventoriesOpen(onInteractEnterEvent.player,new List<InventoryContainer>(){onInteractEnterEvent.player.HandInventory});
        isOpen = false;
        container.CloseUI();
    }
}
