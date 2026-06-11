using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class ItemUsedBomb : MonoBehaviour
{
    [Header("BombSettings")]
    [SerializeField] private float timeLimit = 1f;       //起爆までの時間
    [SerializeField] private float radius = 5f;        //爆発範囲
    [SerializeField] private int damage = 5;            //ダメージ

    [Header("Effect")]
    [SerializeField] private GameObject explosionEffectPrefab;  //エフェクト
    [SerializeField] private float effectScale = 1f;       //爆風半径の倍率

    [Header("Hit Settings")]
    [SerializeField] private LayerMask damageLayer;

    private Transform rangeVisualizer;         //爆発範囲

    public void Initialize()
    {
        StartCoroutine(CountDown()); //カウントダウン開始

        //爆発範囲の可視化
        rangeVisualizer = transform.Find("ExplosionRange");
        if(rangeVisualizer != null)
        {
            rangeVisualizer.gameObject.SetActive(true);
            rangeVisualizer.localScale = Vector3.one * radius * 2f / transform.lossyScale.x;     //球のscaleは直径なので2倍(lossyscaleは親オブジェクト込みのscale)
        }
    }

    private IEnumerator CountDown()
    {
        yield return new WaitForSeconds(timeLimit);
        Debug.Log("爆発");
        Explode();
    }

    private void Explode()
    {
        if(explosionEffectPrefab == null)
        {
            Debug.Log("エフェクトprefabがありません");
            return;
        }

        //爆風エフェクトを生成し、3秒後に消す(3という数字は後でどうにかする)
        GameObject effect = Instantiate(explosionEffectPrefab, transform.position, Quaternion.identity);
        effect.transform.localScale = Vector3.one * effectScale;
        //Destroy(effect, 3f);

        //爆発が当たった敵に対して処理

        Collider[] hits = Physics.OverlapSphere(transform.position, radius, damageLayer);
        foreach(var hit in hits)
        {
            EnemyStatus enemy = hit.GetComponent<EnemyStatus>();    //enemystatusを取得
            if(enemy != null)
            {
                enemy.Damage(damage);
            }
        }

        //オブジェクトを消す
        Destroy(gameObject);
    }
}
