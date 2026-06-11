using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Linq;

[Serializable]
public class OwnedItemsData
{
    private const string PlayerPrefsKey = "OWNED_ITEMS_DATA";//playerprefs保存キー

    //インスタンスを返す
    public static OwnedItemsData Instance
    {
        get
        {
            if(null == _instance)
            {
                _instance = PlayerPrefs.HasKey(PlayerPrefsKey)
                    ? JsonUtility.FromJson<OwnedItemsData>(PlayerPrefs.GetString(PlayerPrefsKey))
                    : new OwnedItemsData();
            }
            return _instance;
        }
    }

    private static OwnedItemsData _instance;

    //所持アイテム一覧を取得する
    public OwnedItem[] OwnedItems
    {
        get { return ownedItems.ToArray(); }
    }

    //度のアイテムを何個所持しているかのリスト
    [SerializeField] private List<OwnedItem> ownedItems = new List<OwnedItem>();

    //コンストラクタ　シングルトンでは外部からnewできないようにprivateにする
    private OwnedItemsData()
    {
    }

    //json化してplayerPrefsに保存する
    public void Save()
    {
        var jsonString = JsonUtility.ToJson(this);
        PlayerPrefs.SetString(PlayerPrefsKey, jsonString);
        PlayerPrefs.Save();
    }

    //アイテムを追加する
    public void Add(Item.ItemType type, int number = 1)
    {
        var item = GetItem(type);//ownedItemsリストから追加したいアイテムを探す
        if(null == item)//無ければ新しくインスタンスを作ってリストに追加
        {
            item = new OwnedItem(type);
            ownedItems.Add(item);
        }
        item.Add(number);//見つけたインスタンスにaddを使う
    }

    //アイテムを使う
    public void Use(Item.ItemType type, int number = 1)
    {
        var item = GetItem(type);
        if(null == item || item.Number < number)
        {
            throw new Exception("アイテムが足りません");
        }
        item.Use(number);
    }

    public OwnedItem GetItem(Item.ItemType type, int number = 1)
    {
        return ownedItems.FirstOrDefault(x => x.Type == type);
    }

    //アイテムの所次数管理用モデル
    [Serializable]
    public class OwnedItem
    {
        public Item.ItemType Type
        {
            get { return type; }
        }
        public int Number
        {
            get { return number; }
        }

        [SerializeField] private Item.ItemType type;
        [SerializeField] private int number;

        public OwnedItem(Item.ItemType type)
        {
            this.type = type;
        }
        public void Add(int number = 1)
        {
            this.number += number;
        }
        public void Use(int number = 1)
        {
            this.number -= number;
        }
    }
}
