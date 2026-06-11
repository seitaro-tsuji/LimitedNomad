using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemBomb : Item
{
    public override bool Use(GameObject user)
    {
        Debug.Log("爆弾を使用");
        GameObject prefab = database.GetUsePrefab(type);    //使用後の爆弾prefab

        if(prefab == null)
        {
            Debug.Log("使用後の爆弾prefabがありません。");
            return false;
        }

        GameObject bomb = Instantiate(prefab, user.transform.position,user.transform.rotation);
        bomb.GetComponent<ItemUsedBomb>().Initialize();

        return true;
    }

    private void OnValidate()
    {
        if(type != ItemType.Bomb && type != ItemType.Bomb2)
        {
            type = ItemType.Bomb;
        }
    }
}
