using JetBrains.Annotations;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class ItemAmount   //ƒAƒCƒeƒ€‚Ìí—Ş‚Æ—Ê
{
    public Item.ItemType type;
    public int amount;
}

[CreateAssetMenu(menuName ="Crafting/Recipe")]
public class CraftRecipe : ScriptableObject
{
    public string recipeName;
    public Item.ItemType result;  //¶¬•¨
    public List<ItemAmount> materials;   //‘fŞ
}
