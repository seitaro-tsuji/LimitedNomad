using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

[RequireComponent(typeof(Collider))]
public class Item : MonoBehaviour
{
    //アイテムの種類定義
    public enum ItemType
    {
        //注:順番を変えないこと 追加するなら最後に追加
        Wood,       //木
        Stone,      //石
        ThrowAxe,   //投げ斧
        ThrowAxe2,  //投げ斧2 金属製
        ThrowAxe3,  //投げ斧3 魔法金属製
        Food,        //食糧
        BlueGem,    //青い宝石
        RedGem,     //赤い宝石
        GoldGem,    //金色の宝石
        Bomb,       //爆弾
        Bomb2,      //大きい爆弾
        Mushroom,   //キノコ
        Crystal,    //鉱石
        MagicMetal,     //魔法金属
        StoneCrystal,   //石に埋まった鉱石
        GreenPotion,    //緑のポーション
        RedPotion,      //赤いポーション
        BluePotion,     //青いポーション
        BluePowder,     //青い粉末
        NoItem      //アイテム無し
    }

    [SerializeField] protected ItemType type;
    [SerializeField] private int foodPoint = 0;//食糧回復量(food以外は0)
    [SerializeField] protected ItemDatabase database;       //継承したクラスで使う用(使用したときにオブジェクト生成する用など)
    private PlayerStatus _status;  //食糧回復のため

    //初期化処理
    public void Initialize()
    {
        //プレイヤーの情報を取得
        if(type == ItemType.Food)
        {
            var playerObject = GameObject.Find("PlayerCharacter");
            _status = playerObject.GetComponent<PlayerStatus>();
        }

        //アニメーションが終了するまでコライダーは無効にする
        var colliderCache = GetComponent<Collider>();
        colliderCache.enabled = false;

        //出現アニメーション
        var transformCache = transform;
        var dropPosition = transform.localPosition + new Vector3(Random.Range(-1f, 1f), 0.2f, Random.Range(-1f, 1f));
        transformCache.DOLocalMove(dropPosition, 0.5f);
        var defaultScale = transformCache.localScale;
        transformCache.localScale = Vector3.zero;
        transformCache.DOScale(defaultScale, 0.5f)
            .SetEase(Ease.OutBounce)
            .OnComplete(() =>
            {
                //出現アニメーションが終わったらコライダーを有効にする
                colliderCache.enabled = true;
            });
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!other.CompareTag("Player")) return;

        //食べ物でなければ所持品として追加する
        if (foodPoint == 0)
        {
            /*OwnedItemsData.Instance.Add(type);
            OwnedItemsData.Instance.Save();*/
            InventoryManager.Instance.AddItem(type);
            //Debug.Log($"Added: {type}");
            //Debug.Log($"Count: {InventoryManager.Instance.GetCount(type)}");
        }
        //食糧回復
        else
        {
            if(_status != null)
            {
                _status.food += foodPoint;
            }
        }

            //オブジェクトを破棄する
            Destroy(gameObject);
    }

    public virtual bool Use(GameObject user)
    {
        //仮想メソッド　デフォルト：何もしない
        Debug.Log("使用できません。");
        return false;
    }
}
