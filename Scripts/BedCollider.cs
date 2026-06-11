using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Collider))]
public class BedCollider : MonoBehaviour
{
    [SerializeField] private Transform player;  //プレイヤー
    [SerializeField] private Transform inCampspTransform;   //キャンプ内のスタートポイント
    [SerializeField] private Commands commands; //コマンドセット
    [SerializeField] private Button choice1Button;
    [SerializeField] private Button choice2Button;
    [SerializeField] private BGMManager bgmManager; //BGM
    private PlayerStatus _status;
    private PlayerController _controller;
    // Start is called before the first frame update
    void Start()
    {
        _status = player.GetComponent<PlayerStatus>();
        _controller = player.GetComponent<PlayerController>();
    }

    //コライダーの範囲内にプレイヤーが入ったときの処理
    public void OnEnterDetect(Collider collider)
    {
        if (collider.CompareTag("Player"))
        {
            _controller.Set_IsControllable(false);//操作不可能にする
            commands.Display();//選択肢コマンドの表示

            //選択肢の内容の設定
            commands.SetTextButtonAndWindow(0, "ベッドで休みますか？\n" +
                "Food : " + (int)_status.food + "→" + (int)(_status.food - _status.requiredFood) +" (選択肢をクリック)");
            commands.SetTextButtonAndWindow(1, "休む");
            commands.SetTextButtonAndWindow(2, "休まない");
            choice1Button.onClick.RemoveAllListeners();
            choice2Button.onClick.RemoveAllListeners();
            choice1Button.onClick.AddListener(PushSleepButton);
            choice2Button.onClick.AddListener(PushNotSleepButton);
        }
    }

    //休むボタンの処理
    private void PushSleepButton()
    {
        //食糧100以上なら日を越せる
        if (_status.DoHaveOneDayFood())
        {
            _status.Heal(999);  //全回復
            _status.DayCountPlus();//1日進める
            _status.isAbleToGoOut = true;//外出可能にする

            _controller.enabled = false;//1度無効にしないと直接プレイヤーの位置を変えられない

            player.transform.position = inCampspTransform.position;                                       //プレイヤーをスタートポイントに配置
            _controller.transform.LookAt(_controller.transform.position + new Vector3(0, 0, 1f)); //前方を見る
            _controller.Set_FrameControllable(false);                                                  //速度計算をされると元の場所に戻るため1フレームだけ処理停止

            _controller.enabled = true;

            _controller.Set_IsControllable(true);//操作可能にする
            commands.Hide();//選択肢コマンドの消去

            //BGMの変更
            bgmManager.PlayCamp1();
        }
        //満腹度が足りないならゲームオーバー
        else
        {

        }
    }

    //休まないボタンの処理
    private void PushNotSleepButton()
    {
        _controller.Set_IsControllable(true);//操作可能にする
        commands.Hide();//選択肢コマンドの消去
    }
}
