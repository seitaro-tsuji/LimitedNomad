using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemsDialog : MonoBehaviour
{
    [SerializeField] private int buttonNumber = 15;
    [SerializeField] private ItemButton itemButton;
    [SerializeField] private SettingItem settingItem;

    private ItemButton[] _itemButtons;

    // Start is called before the first frame update
    void Start()
    {
        gameObject.SetActive(false);
        //アイテム欄を必要数だけ複製する
        for(var i = 0; i < buttonNumber; i++)
        {
            ItemButton _button = Instantiate(itemButton, transform);
            _button.Init(settingItem);
        }

        //子要素のItemButtonを一括取得
        _itemButtons = GetComponentsInChildren<ItemButton>();
    }

    public void Toggle()
    {
        gameObject.SetActive(!gameObject.activeSelf);
        if (gameObject.activeSelf)
        {
            var items = InventoryManager.Instance.Items;

            //表示された場合はアイテム欄をリフレッシュする
            for(var i = 0; i < buttonNumber; i++)
            {
                //各アイテム欄に所持アイテム情報をセットする
                _itemButtons[i].ItemData = items.Count > i ? items[i] : null;
            }
        }
    }
}
