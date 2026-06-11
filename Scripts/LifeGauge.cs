using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class LifeGauge : MonoBehaviour
{
    [SerializeField] private Image fillImage;

    private RectTransform _parentRectTransform;
    private Camera _camera;
    private MobStatus _status;
    

    // Update is called once per frame
    void Update()
    {
        Refresh();
    }

    //ゲージを初期化する
    public void Initialize(RectTransform parentTransform, Camera camera, MobStatus status)
    {
        //座標の計算に使うパラメータを受け取り、保持しておく
        _parentRectTransform = parentTransform;
        _camera = camera;
        _status = status;
        Refresh();
    }

    //ゲージを更新する
    private void Refresh()
    {
        //残りライフの計算
        fillImage.fillAmount = _status.Life / _status.LifeMax;

        //対象mobの位置にゲージを移動する
        var screenPoint = _camera.WorldToScreenPoint(_status.transform.position);
        Vector2 localPoint;
        RectTransformUtility.ScreenPointToLocalPointInRectangle(_parentRectTransform, screenPoint, null, out localPoint);

        //ゲージをキャラの少し上に移動する
        transform.localPosition = localPoint + new Vector2(0, 80);
    }
}
