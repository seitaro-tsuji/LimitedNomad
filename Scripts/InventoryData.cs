using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class InventoryData
{
    [Serializable]
    public class ItemData
    {
        public Item.ItemType type;
        public int count;
    }

    [SerializeField] private List<ItemData> items = new List<ItemData>();

    public List<ItemData> Items => items;  //外側から読むためのプロパティ
    
    public int GetCount(Item.ItemType type)
    {
        ItemData item = items.Find(x => x.type == type);
        return item != null ? item.count : 0;
    }

    public void AddItem(Item.ItemType type, int amount =1)
    {
        ItemData item = items.Find(x => x.type == type);

        if (item == null)
        {
            item = new ItemData();
            item.type = type;
            item.count = 0;
            items.Add(item);
        }

        item.count += amount;
    }

    public bool UseItem(Item.ItemType type, int amount = 1)
    {
        ItemData item = items.Find(x => x.type == type);

        if (item == null || item.count < amount)
        {
            return false;
        }

        item.count -= amount;

        if(item.count <= 0)
        {
            items.Remove(item);
        }

        return true;
    }

    public void Clear()
    {
        items.Clear();
    }
}
