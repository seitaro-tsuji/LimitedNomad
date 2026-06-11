using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerLifeGauge : MonoBehaviour
{
    [SerializeField] private Image fillImage;
    [SerializeField] private PlayerStatus _status;
    [SerializeField] private Text hpText;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        fillImage.fillAmount = _status.Life / _status.LifeMax;//HPÉQÅ[ÉWÇÃçƒåvéZ
        if (fillImage.fillAmount < 0.25)
        {
            fillImage.color = Color.red;
            hpText.color = Color.red;
        }
        else
        {
            fillImage.color = Color.green;
            hpText.color = Color.green;
        }

        hpText.text = "HP : " + _status.Life + " / " + _status.LifeMax;
    }
}
