using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CommandsFreeChoice : MonoBehaviour
{
    [SerializeField] private Transform messageWindow;
    private int choicesNum; //選択肢の数 
    private Transform choices;  //子オブジェクトのChoices
    [SerializeField] private Transform choiceButton; //孫オブジェクトのChoiceButton
    private List<GameObject> choicesList;    //選択肢のリスト(destroyメソッドでおかしくなる可能性があるから生成したオブジェクトをリストに入れて管理する)

    // Start is called before the first frame update
    void Start()
    {
        choicesList = new List<GameObject>(); // リストの初期化
        choicesNum = 1;
        choices = transform.Find("Choices");
        //choiceButton = transform.Find("Choices/ChoiceButton1");

        //デバッグ
        if(choiceButton == null)
        {
            Debug.Log("ChoiceButton1が見つかりません");
        }
        else
        {
            //Debug.Log("Ok");
        }

        SetActiveCommands(false); //最初は全て非表示
    }

    public void CreateCommands(int _choicesNum)
    {
        choicesNum = _choicesNum;
        MakeChoicesButton(_choicesNum);

        //全て表示状態にする
        SetActiveCommands(true);
    }

    private void MakeChoicesButton(int _choicesNum)
    {
        //選択肢をすべて削除する
        foreach(Transform t in choices)
        {
            Destroy(t.gameObject);
        }
        choicesList.Clear();

        //選択肢を必要数だけ用意する
        for(var i = 0; i < _choicesNum; i++)
        {
            GameObject obj = Instantiate(choiceButton.gameObject, choices);
            choicesList.Add(obj);//リストに追加
        }
    }

    public void SetText(int n, string str)
    {
        if (n == 0)
        {
            //todo メッセージウィンドウの文章変更
            Text text = messageWindow.GetComponentInChildren<Text>();
            text.text = str;
            Debug.Log("メッセージウィンドウの文字を変更");
        }
        else if (n > 0 && n <= choicesNum)
        {
            //Transform choice = choices.GetChild(n - 1);//上からn番目のオブジェクトを取得
            GameObject choice = choicesList[n - 1]; //n番目のオブジェクトをリスト内から取得
            Text text = choice.GetComponentInChildren<Text>();  //その子オブジェクトのさらに子オブジェクトからtextを取得
            text.text = str;

            if(choice != null && text != null)
            {
                Debug.Log(n + "番目の選択肢の文章を変更");
            }
        }
        else
        {
            //todo無効な数値
            Debug.Log("無効な数値です");
        }
    }

    public void SetActiveCommands(bool b)
    {
        //全て非表示状態にする
        foreach (Transform t in transform)
        {
            t.gameObject.SetActive(b);
        }
    }

    public void SetOnclickedAction(int n, Action action)
    {
        if (n > 0 && n <= choicesNum)
        {
            //有効な数値が渡された場合
            GameObject choice = choicesList[n - 1]; //n番目のオブジェクトをリスト内から取得
            Button choiceButton = choice.GetComponent<Button>();

            choiceButton.onClick.RemoveAllListeners();  // リスナーの重複を防ぐ
            choiceButton.onClick.AddListener(() => action());
        }
        else
        {
            //todo 無効な数値の時
        }
    }
}
