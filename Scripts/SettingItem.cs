using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SettingItem : MonoBehaviour
{
    [SerializeField] private Image image;
    [SerializeField] private ItemDatabase itemDatabase;
    private Text itemName;
    private Text itemCount;
    private Item.ItemType currentType;    //セットされてるアイテムの種類

    public void Start()
    {
        currentType = Item.ItemType.NoItem;
        itemName = transform.Find("ItemName").GetComponent<Text>();
        itemCount = transform.Find("ItemCount").GetComponent<Text>();

        //ボタンを押した時の処理
        GetComponent<Button>().onClick.AddListener(OnClickButton);
    }

    public void OnEnable()
    {
        if (InventoryManager.Instance != null)
        {
            InventoryManager.Instance.OnItemCountChanged += UpdateCount;
        }
        else
        {
            Debug.Log("inventoryManagerがnull");
        }
    }

    public void OnDisable()
    {
        InventoryManager.Instance.OnItemCountChanged -= UpdateCount;
    }

    public void UpdateCount(Item.ItemType changedType)
    {
        if (currentType != changedType) return;

        //所持数が0になるならnoitemをセットして終了
        int count = InventoryManager.Instance.GetCount(currentType);
        if(count <= 0)
        {
            Set(Item.ItemType.NoItem);
            return;
        }

        itemCount.text = "x" + count;
    }

    public void Set(Item.ItemType type)
    {
        currentType = type;

        if (type != Item.ItemType.NoItem)
        {
            image.sprite = itemDatabase.GetSprite(type);
            itemName.text = "Set : " + type;
            itemCount.text = "x" + InventoryManager.Instance.GetCount(type);
        }
        else
        {
            image.sprite = null;
            itemName.text = "Set : Nothing";
            itemCount.text = "x0";
        }
    }

    public void OnClickButton()
    {
        if(InventoryManager.Instance.GetCount(currentType) <= 0)
        {
            Debug.Log("アイテムがない");
            return;
        }

        Item item = itemDatabase.GetPrefab(currentType);

        if (item == null)
        {
            Debug.Log("prefabがない");
            return;
        }

        //アイテムを使用して、使用できた場合所持数を1個減らす
        if (item.Use(GameObject.FindWithTag("Player")))
            InventoryManager.Instance.UseItem(currentType);    
    }
}
