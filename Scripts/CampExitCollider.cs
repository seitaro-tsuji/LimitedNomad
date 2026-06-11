using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CampExitCollider : MonoBehaviour
{
    [SerializeField] private GameObject player; //プレイヤーのオブジェクト
    [SerializeField] private GameObject startPoint; //プレイヤーの拠点を出た後のスタート地点
    [SerializeField] private Transform inCamp_spTransform;  //拠点内のスタート地点
    [SerializeField] private Transform exitCommands;   //出口関連のコマンドをまとめたオブジェクト
    [SerializeField] private Commands commands;    //コマンドセット
    [SerializeField] private Button choice1;
    [SerializeField] private Button choice2;
    private PlayerStatus _status;
    private PlayerController playerController; //プレイヤ―コントローラー
    // Start is called before the first frame update
    void Start()
    {
        _status = player.GetComponent<PlayerStatus>();
        playerController = player.GetComponent<PlayerController>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    //コライダーに何かが入っているときのメソッド
    public void OnDetectObject(Collider collider)
    {
        if (collider.CompareTag("Player"))
        {
            playerController.Set_IsControllable(false);//操作不可能にする

            //外出可能なときは外に出るための選択肢を表示
            if (_status.isAbleToGoOut)
            {
                //commands内のオブジェクト全部を有効化する
                foreach (Transform child in exitCommands)
                {
                    child.gameObject.SetActive(true);
                }
            }
            //外出不可能なとき
            else
            {
                //コマンドを出す処理
                foreach (Transform t in commands.transform)
                {
                    t.gameObject.SetActive(true);
                }
                //コマンドの表示内容
                commands.SetTextButtonAndWindow(1, "出る(無効)");
                commands.SetTextButtonAndWindow(2, "出ない");
                commands.SetTextButtonAndWindow(0, "ベッドで寝るまで拠点から出られません。");
                //ボタンを押したときの処理
                choice1.onClick.RemoveAllListeners();
                choice2.onClick.RemoveAllListeners();
                choice2.onClick.AddListener(NotExit);
            }
        }
    }

    //出ないボタンの処理(外出可能の時と同じ処理)
    private void NotExit()
    {
        //出ないボタンをクリックしたときの処理
        playerController.Set_IsControllable(true);//操作可能にする

        playerController.enabled = false;//1度無効にしないと直接プレイヤーの位置を変えられない

        player.transform.position = inCamp_spTransform.position;                                       //プレイヤーをスタートポイントに配置
        playerController.transform.LookAt(playerController.transform.position + new Vector3(0, 0, -1f)); //前方を見る
        playerController.Set_FrameControllable(false);                                                  //速度計算をされると元の場所に戻るため1フレームだけ処理停止

        playerController.enabled = true;

        //コマンドを非表示にする
        commands.Hide();
    }
}
