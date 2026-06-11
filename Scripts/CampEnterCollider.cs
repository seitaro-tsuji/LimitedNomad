using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using Unity.VisualScripting.Antlr3.Runtime.Tree;
using UnityEngine;
using UnityEngine.UI;

public class CampEnterCollider : MonoBehaviour
{
    [SerializeField] private GameObject player; //プレイヤーのオブジェクト
    [SerializeField] private Commands commands; //コマンド
    [SerializeField] private Button choice1;    
    [SerializeField] private Button choice2;
    [SerializeField] private Transform inCampspTransform;   //キャンプ内のスタートポイント
    [SerializeField] private Transform spTransform;         //スタートポイント
    [SerializeField] private LimitTime limitTime;   //タイムリミット表示UI
    [SerializeField] private Spawner spawner;   //スポナー
    [SerializeField] private Transform enemies; //Enemiesオブジェクト
    [SerializeField] private CommandsFreeChoice commandsFreeChoice; //選択肢
    [SerializeField] private BGMManager bgmManager;     //BGM
    private PlayerStatus _status;
    private PlayerController playerController;  
    // Start is called before the first frame update
    void Start()
    {
        _status = player.GetComponent<PlayerStatus>();
        playerController = player.GetComponent<PlayerController>();
    }

    //コライダーに何か入ってきた時のメソッド
    public void OnDetectObject(Collider collider)
    {
        if (collider.CompareTag("Player"))
        {
            Time.timeScale = 0;//時間停止
            playerController.Set_IsControllable(false);  //操作も不可能にする(向きを変更できないようにするため)

            //コマンドの表示内容 食糧が100以上あるかで内容が変わる
            if (_status.DoHaveOneDayFood())
            {
                //コマンドを出す処理
                foreach (Transform t in commands.transform)
                {
                    t.gameObject.SetActive(true);
                }
                commands.SetTextButtonAndWindow(1, "戻る");
                commands.SetTextButtonAndWindow(2, "戻らない");
                commands.SetTextButtonAndWindow(0, "拠点に戻りますか？\n(ベッドで休むまで出られません)");
                //ボタンを押したときの処理
                choice1.onClick.RemoveAllListeners();
                choice2.onClick.RemoveAllListeners();
                choice1.onClick.AddListener(EnterCamp);
                choice2.onClick.AddListener(NotEnterCamp);
            }
            //100未満
            else
            {
                //todo
                commandsFreeChoice.CreateCommands(1);//選択肢1でコマンド生成
                commandsFreeChoice.SetText(1, "OK");    //選択肢の内容をセット
                commandsFreeChoice.SetText(0, "1日分の食料が無いと拠点に戻れません。");
                commandsFreeChoice.SetOnclickedAction(1, NotEnterCamp);
            }
        }
    }

    private void EnterCamp()
    {
        //戻るボタンをクリックしたときの処理
        playerController.Set_IsControllable(true);//操作可能にする
        _status.isOutOfCamp = false;    //外出をfalseに

        playerController.enabled = false;//1度無効にしないと直接プレイヤーの位置を変えられない

        player.transform.position = inCampspTransform.position;                                       //プレイヤーをスタートポイントに配置
        playerController.transform.LookAt(playerController.transform.position + new Vector3(0, 0, -1f)); //前方を見る
        playerController.Set_FrameControllable(false);                                                  //速度計算をされると元の場所に戻るため1フレームだけ処理停止

        playerController.enabled = true;

        //タイムリミットを非表示にする
        limitTime.gameObject.SetActive(false);

        //コマンドを非表示
        commands.Hide();

        //時間停止を戻す
        Time.timeScale = 1;

        //スポナーと敵を無効化(lifegaugecontainerのゲージも消去)
        spawner.Disactivate();
        EnemyStatus status;
        foreach(Transform t in enemies)
        {
            status = t.GetComponent<EnemyStatus>();
            LifeGaugeContainer.Instance.Remove(status);
            Destroy(t.gameObject);
        }

        //外出は不可にする
        _status.isAbleToGoOut = false;

        //BGMを変更
        Debug.Log("BGMを変更");
        bgmManager.PlayCamp2();
    }

    private void NotEnterCamp()
    {
        //戻らないボタンをクリックしたときの処理
        playerController.Set_IsControllable(true);//操作可能にする

        playerController.enabled = false;//1度無効にしないと直接プレイヤーの位置を変えられない

        player.transform.position = spTransform.position;                                       //プレイヤーをスタートポイントに配置
        playerController.transform.LookAt(playerController.transform.position + new Vector3(0, 0, -1f)); //前方を見る
        playerController.Set_FrameControllable(false);                                                  //速度計算をされると元の場所に戻るため1フレームだけ処理停止

        playerController.enabled = true;

        //コマンドを非表示(このメソッドはどちらの選択肢でも呼び出しうるから非表示も2つ)
        commands.Hide();
        commandsFreeChoice.SetActiveCommands(false);

        //時間停止を戻す
        Time.timeScale = 1;
    }
}
