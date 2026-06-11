using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using static UnityEngine.Terrain;

[RequireComponent(typeof(Button))]
public class CraftButton : MonoBehaviour
{
    [SerializeField] private Image craftItemImage;
    [SerializeField] private Text ownedAmount;
    [SerializeField] private ItemDatabase itemDatabase;

    public Item.ItemType craftItemType; //çÏÇÈÉAÉCÉeÉÄ

    public void OnEnable()
    {
        if (InventoryManager.Instance != null)
            InventoryManager.Instance.OnItemCountChanged += UpdateCount;
        else
            Debug.Log("inventoryManagerÇ™null");
    }

    public void OnDisable()
    {
        if (InventoryManager.Instance != null)
            InventoryManager.Instance.OnItemCountChanged -= UpdateCount;
        else
            Debug.Log("inventoryManagerÇ™null");
    }


    public void Setup(Item.ItemType craftType)
    {
        craftItemType = craftType;
        craftItemImage.sprite = itemDatabase.GetSprite(craftType);
        ownedAmount.text = $"{InventoryManager.Instance.GetCount(craftItemType)}";
    }

    public void OnClickCraft()
    {
        Debug.Log("craft");
        InventoryManager.Instance.CraftItem(craftItemType);
    }

    public void UpdateCount(Item.ItemType type)
    {
        if (type != craftItemType) return;

        ownedAmount.text = $"x {InventoryManager.Instance.GetCount(craftItemType)}";
    }
}
