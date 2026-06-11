using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemDropperDebug : MonoBehaviour
{
    [SerializeField] private List<InventoryData.ItemData> items;  //アイテム名と個数のセット
    [SerializeField] private ItemDatabase database;  //アイテムデータベース

    // Start is called before the first frame update
    void Start()
    {
        //foreach (var item in items)
            //Debug.Log(item.type +" "+ item.count);

        DropItems();
    }

    public void DropItems()
    {
        foreach(var item in items)
        {
            var itemAmount = item.count; //出現量
            Item.ItemType type = item.type; //アイテムの種類
            Item itemPrefab = database.GetPrefab(type); //prefab

            for (var i = 0; i < itemAmount; i++)
            {
                var dropItem = Instantiate(itemPrefab, transform.position, Quaternion.identity);
                dropItem.Initialize();
            }
        }
    }
}
