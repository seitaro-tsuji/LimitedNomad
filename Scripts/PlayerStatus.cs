using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStatus : MobStatus
{
    public int dayCount;   //日付
    public bool isAbleToGoOut; //外出可能かどうか
    [SerializeField] public float food = 50f;    //食糧
    [SerializeField] public float requiredFood = 100f;  //食糧の要求量
    [SerializeField] public float requiredFoodPlus = 20f;  //食糧要求量の増加量
    [SerializeField] private GameObject gameoverBackGround;
    [SerializeField] private ResultPanel resultPanel;
    [SerializeField] private GameObject all_UI;     //UIs
    public bool isOutOfCamp;   //拠点の外にいるか
    private string deathCause;  //死因(limittimeなどから設定できるように)

    protected override void Start()
    {
        //初期状態の設定
        base.Start();
        //dayCount = 20;
        isAbleToGoOut = true;
        deathCause = "alive";
    }

    //deayCountを増やす、食糧値を100減らす
    public void DayCountPlus()
    {
        dayCount++;
        food -= requiredFood;
        requiredFood += requiredFoodPlus;
        Debug.Log("食糧必要量アップ");
    }

    private void Update()
    {
        //拠点の外なら食糧が減る(1秒で5)
        if (isOutOfCamp)
        {
            food -= Time.deltaTime / 5;

            //food0以下なら餓死
            if (food <= 0)
            {
                DieFood();
            }
        }
    }

    public override void Damage(int damage)
    {
        base.Damage(damage);

        //ダメージ後死んでいるなら死因を設定する
        if(_state == StateEnum.Die)
        {
            deathCause = "モンスターに倒された";
        }
    }

    protected override void OnDie()
    {
        base.OnDie();
        all_UI.SetActive(false);    //UIの表示を終了する
        gameoverBackGround.SetActive(true); //死亡時の背景を表示(画面を暗くする)
        StartCoroutine(ResultShowCoroutine());
    }

    private IEnumerator ResultShowCoroutine()
    {
        //3秒待ってresultPanelの表示をする
        yield return new WaitForSeconds(3);  
        resultPanel.gameObject.SetActive(true);
        resultPanel.Initialize(dayCount, deathCause);
    }

    //1日分の食料を持っているか
    public bool DoHaveOneDayFood()
    {
        return food >= requiredFood;
    }

    //日付を取得
    public int GetDayCount()
    {
        return dayCount;
    }

    //foodが原因で死亡したとき
    private void DieFood()
    {
        deathCause = "飢えて死んでしまった";
        OnDie();
    }

    //timeが原因で死亡したとき
    public void DieTime()
    {
        deathCause = "時間内に戻れなかった";
        OnDie();
    }
}
