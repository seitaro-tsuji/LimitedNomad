using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ResultPanel : MonoBehaviour
{
    [SerializeField] private Text dayText;
    [SerializeField] private Text causeText;

    public void Initialize(int dayCount, string cause)
    {
        dayText.text = $"¶‚«c‚Á‚½“ú”F{dayCount}“ú";
        causeText.text = "€ˆöF"+cause;
    }
}
