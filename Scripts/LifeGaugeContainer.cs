using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(RectTransform))]
public class LifeGaugeContainer : MonoBehaviour
{
    public static LifeGaugeContainer Instance
    {
        get { return _instance; }
    }

    private static LifeGaugeContainer _instance;

    [SerializeField] private Camera mainCamera;         //ライフゲージ表示対象のmobを写しているカメラ
    [SerializeField] private LifeGauge lifeGaugePrefab; //ライフゲージのprefab

    private RectTransform rectTransform;
    private readonly Dictionary<MobStatus, LifeGauge> _statusLifeBarMap = new Dictionary<MobStatus, LifeGauge>();//アクティブなライフゲージを保持するコンテナ


    private void Awake()
    {
        if(null != _instance)
        {
            throw new Exception("LifeGaugeContainer instance already exists.");
        }
        _instance = this;
        rectTransform = GetComponent<RectTransform>();  
    }

    //ライフゲージを追加する
    public void Add(MobStatus status)
    {
        var lifeGauge = Instantiate(lifeGaugePrefab, transform);
        lifeGauge.Initialize(rectTransform, mainCamera, status);
        _statusLifeBarMap.Add(status, lifeGauge);
    }

    //ライフゲージを破棄する
    public void Remove(MobStatus status)
    {
        Destroy(_statusLifeBarMap[status].gameObject);
        _statusLifeBarMap.Remove(status);
    }
}
