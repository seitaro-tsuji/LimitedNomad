using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
using System.Linq;
using UnityEngine.UIElements.Experimental;

public class ItemButton : MonoBehaviour
{
    public InventoryData.ItemData ItemData
    {
        get { return _itemData; }
        set
        {
            _itemData = value;

            //アイテムが割り当てられたかどうかでアイテム画像や所持個数の表示を切り替える
            var isEmpty = null == _itemData;
            image.gameObject.SetActive(!isEmpty);
            number.gameObject.SetActive(!isEmpty);
            _button.interactable = !isEmpty;

            if (!isEmpty)
            {
                //image.sprite = itemSprites.First(x => x.itemType == _itemData.type).sprite;
                image.sprite = itemDatabase.GetSprite(_itemData.type);
                number.text = _itemData.count.ToString();
            }
        }
    }

    //各アイテム用の画像を指定するフィールド
    //[SerializeField] private ItemTypeSpriteMap[] itemSprites;
    [SerializeField] private ItemDatabase itemDatabase;
    [SerializeField] private Image image;
    [SerializeField] private Text number;
    [SerializeField] private SettingItem settingItem;

    private Button _button;
    private InventoryData.ItemData _itemData;

    private void Awake()
    {
        _button = GetComponent<Button>();
        _button.onClick.AddListener(OnClick);
    }

    public void Init(SettingItem _setItem)
    {
        this.settingItem = _setItem;  //settingItemボタンを渡す
    }

    private void OnClick()
    {
        settingItem.Set(_itemData.type);//settingItemボタンにそのアイテムを表示する
    }

    [Serializable]
    public class ItemTypeSpriteMap
    {
        public Item.ItemType itemType;
        public Sprite sprite;
    }
}