using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemAxe : Item
{
    public override bool Use(GameObject user)
    {
        //Debug.Log("投げ斧を使用");
        GameObject prefab = database.GetUsePrefab(type);

        if(prefab == null)
        {
            Debug.LogWarning("投げ斧prefabがありません");
            return false;
        }

        //使用者の向きを取得
        Vector3 direction = user.transform.forward;
        direction.y = 0f;
        direction.Normalize();

        //使用者の少し前、1m浮いた位置に寝かせた向きで生成  飛んでいく方向は使用者の向きで設定する
        Quaternion rot = Quaternion.Euler(0f, user.transform.eulerAngles.y, 0f) * Quaternion.Euler(-90f, 0, 0);
        GameObject axe = Instantiate(prefab, user.transform.position + user.transform.forward * 1.5f + Vector3.up, rot);
        axe.GetComponent<ItemThrownAxe>().Initialize(direction);

        return true;
    }
}
