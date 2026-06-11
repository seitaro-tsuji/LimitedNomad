using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Commands : MonoBehaviour
{
    [SerializeField] private Text choiceText1;
    [SerializeField] private Text choiceText2;
    [SerializeField] private Text windowText;
    // Start is called before the first frame update
    void Start()
    {
        //最初はコマンド丸ごと非表示にする
        foreach(Transform t in transform)
        {
            t.gameObject.SetActive(false);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SetTextButtonAndWindow(int i, string s)
    {
        if (i == 1)
        {
            choiceText1.text = s;
        }
        else if (i == 2)
        {
            choiceText2.text = s;
        }
        else
        {
            windowText.text = s;
        }
    }

    public void Display()
    {
        foreach (Transform t in transform)
        {
            t.gameObject.SetActive(true);
        }
    }

    public void Hide()
    {
        foreach(Transform t in transform)
        {
            t.gameObject.SetActive(false);
        }
    }
}
