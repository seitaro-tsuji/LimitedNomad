using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Text))]
public class LimitTime : MonoBehaviour
{
    [SerializeField] private float limitMax;    //タイムリミットの初期値
    [SerializeField] private PlayerStatus _status;  
    private Text limitText;
    private float time;//残り時間
    // Start is called before the first frame update
    void Start()
    {
        //テキストを取得
        limitText = GetComponent<Text>();

        //残り時間設定(多分後で変更する)
        time = 60;

        //最初は非表示
        transform.gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        time -= Time.deltaTime;
        if(limitText != null)
        {
            limitText.text = "TimeLimit : " + (int)time;
        }

        //timeが0以下の時、プレイヤーを死亡にする
        if(time <= 0)
        {
            _status.DieTime();
        }
    }

    //非表示から表示状態にする
    public void Display()
    {
        transform.gameObject.SetActive(true);
        time = limitMax;     //残り時間の設定
    }
}
