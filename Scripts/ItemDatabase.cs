using JetBrains.Annotations;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class ItemDefinition
{
    public Item.ItemType type;
    public Sprite sprite;
    public Item prefab;
    public GameObject usePrefab;      //アイテムとして使用した際に生成するprefab(投げた後の挙動など)
}

[CreateAssetMenu(menuName = "Item/ItemDatabase")]
public class ItemDatabase : ScriptableObject
{
    public ItemDefinition[] definitions;

    public ItemDefinition Get(Item.ItemType type)
    {
        if(definitions == null)
        {
            return null;
        }
        return Array.Find(definitions, x => x != null && x.type == type);
    }

    public Sprite GetSprite(Item.ItemType type)
    {
        var data = Get(type);
        return data != null ? data.sprite : null;
    }

    public Item GetPrefab(Item.ItemType type)
    {
        var data = Get(type);
        return data != null ? data.prefab : null;
    }

    public GameObject GetUsePrefab(Item.ItemType type)
    {
        var data = Get(type);
        return data != null ? data.usePrefab : null;
    }
}
