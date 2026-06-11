using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//所持品を管理するためのもの　inbentorydataクラスをUnity内で使う用

[DefaultExecutionOrder(-100)]
public class InventoryManager : MonoBehaviour
{
    [SerializeField] private CraftRecipeDatabase craftRecipeDatabase;

    public static InventoryManager Instance { get; private set; }

    public InventoryData inventoryData = new InventoryData();

    public List<InventoryData.ItemData> Items => inventoryData.Items;

    public event Action<Item.ItemType> OnItemCountChanged;

    private void Awake()
    {
        Debug.Log("InventoryManager Awake");
        Instance = this;
    }

    public void AddItem(Item.ItemType type, int amount = 1)
    {
        inventoryData.AddItem(type, amount);
        OnItemCountChanged?.Invoke(type);
    }

    public bool UseItem(Item.ItemType type, int amount = 1)
    {
        bool used = inventoryData.UseItem(type, amount);
        if (used)
            OnItemCountChanged?.Invoke(type);
        return used;
        
    }

    public int GetCount(Item.ItemType type)
    {
        return inventoryData.GetCount(type);
    }

    public void ResetInventory()
    {
        inventoryData.Clear();
    }

    public bool CraftItem(Item.ItemType type)
    {
        //データベースから作るアイテムのレシピを取得
        CraftRecipe recipe = craftRecipeDatabase.recipes.Find(x => x.result == type);

        if (recipe == null)
        {
            Debug.Log("レシピがありません。");
            return false;
        }

        //素材を取得して要求数あるか確認
        List<ItemAmount> materials = recipe.materials;
        foreach(ItemAmount material in materials)
        {
            if(GetCount(material.type) < material.amount)
            {
                Debug.Log($"素材が足りません : {material.type}");
                return false;
            }
        }

        foreach(ItemAmount material in materials)
        {
            UseItem(material.type, material.amount);
        }

        AddItem(recipe.result);

        return true;
    }
}
