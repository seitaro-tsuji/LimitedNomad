using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UiScript : MonoBehaviour
{
    [SerializeField] private Text foodText; //食糧残量
    [SerializeField] private Text dayCount; //日にち
    [SerializeField] private PlayerStatus _status;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        dayCount.text = "Day : " + (int)_status.dayCount;
        foodText.text = "Food : " + (int)_status.food;
        foodText.color = _status.food < 40f ? Color.red : Color.cyan;//食糧40未満なら赤色
    }
}
