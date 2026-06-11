using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Diagnostics;
using UnityEngine.UI;

public class CampExitCommands : MonoBehaviour
{
    [SerializeField] private GameObject player; //プレイヤーのオブジェクト
    [SerializeField] private Transform startpointTransform;//スタートポイント
    [SerializeField] private Transform inCamp_spTransform;  //キャンプ内のスタートポイント
    [SerializeField] private LimitTime limitTime;   //タイムリミット表示UI
    [SerializeField] private Spawner spawner;//スポナー
    [SerializeField] private Transform enemies; //enemies
    [SerializeField] private BGMManager bgmManager;     //BGM
    private PlayerStatus playerStatus;  
    private PlayerController playerController;
    private Button exitButton;  //出るボタン
    private Button notexitButton;//出ないボタン
    // Start is called before the first frame update
    void Start()
    {
        //この内部のオブジェクトを全部無効にする
        HideCommands();

        //子オブジェクトのボタンコンポーネントを取得し、押したときの処理を追加
        Transform choices = transform.Find("Choices");
        Transform button1 = choices.Find("Button_exit");
        if (button1 != null)
        {
            exitButton = button1.GetComponent<Button>();
            exitButton.onClick.AddListener(ExitButtonPush);
        }
        Transform button2 = choices.Find("Button_notexit");
        if (button2 != null)
        {
            notexitButton = button2.GetComponent<Button>();
            notexitButton.onClick.AddListener(NotExitButtonPush);
        }

        //playerControllerを取得
        playerStatus = player.GetComponent<PlayerStatus>();
        playerController = player.GetComponent<PlayerController>();
    }

    private void ExitButtonPush()
    {
        //出るボタンをクリックしたときの処理
        playerController.Set_IsControllable(true);//操作可能にする
        playerStatus.isOutOfCamp = true;    //拠点の外にいる

        playerController.enabled = false;//1度無効にしないと直接プレイヤーの位置を変えられない

        player.transform.position = startpointTransform.position;                                       //プレイヤーをスタートポイントに配置
        playerController.transform.LookAt(playerController.transform.position + new Vector3(0, 0, 1f)); //前方を見る
        playerController.Set_FrameControllable(false);                                                  //速度計算をされると元の場所に戻るため1フレームだけ処理停止

        playerController.enabled = true;

        //外に出るときのUIなどの設定(リミット表示など)
        limitTime.Display();

        //コマンドを非表示にする
        HideCommands();

        //スポナーと敵を有効化する
        spawner.Activate() ;
        enemies.gameObject.SetActive(true);

        //BGMを変更する
        bgmManager.PlayField();
    }

    private void NotExitButtonPush()
    {
        //出ないボタンをクリックしたときの処理
        playerController.Set_IsControllable(true);//操作可能にする

        playerController.enabled = false;//1度無効にしないと直接プレイヤーの位置を変えられない

        player.transform.position = inCamp_spTransform.position;                                       //プレイヤーをスタートポイントに配置
        playerController.transform.LookAt(playerController.transform.position + new Vector3(0, 0, -1f)); //前方を見る
        playerController.Set_FrameControllable(false);                                                  //速度計算をされると元の場所に戻るため1フレームだけ処理停止

        playerController.enabled = true;

        //コマンドを非表示にする
        HideCommands();
    }

    private void HideCommands()
    {
        //コマンドを全部非表示にする
        foreach (Transform t in transform)
        {
            t.gameObject.SetActive(false);
        }
    }
}
