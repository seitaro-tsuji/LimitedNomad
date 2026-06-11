using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class MaterialIcon : MonoBehaviour
{
    [SerializeField] private Image iconImage;
    [SerializeField] private Text amountText;
    [SerializeField] private ItemDatabase database;
    public Item.ItemType materialType;      //ëfçﬁÇÃéÌóﬁ
    public int amount;                      //ëfçﬁÇÃïKóvêî

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

    public void Setup(ItemAmount material)
    {
        materialType = material.type;
        amount = material.amount;
        iconImage.sprite = database.GetSprite(materialType);
        amountText.text = $"x {amount.ToString()} / {InventoryManager.Instance.GetCount(materialType)}";
    }

    public void UpdateCount(Item.ItemType type)
    {
        if (type != materialType) return;

        amountText.text = $"x {amount.ToString()} / {InventoryManager.Instance.GetCount(materialType)}";
    }
}
