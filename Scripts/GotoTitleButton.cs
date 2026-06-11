using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GotoTitleButton : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        //ƒ^ƒCƒgƒ‹‚É–ß‚éˆ—‚ğ’Ç‰Á
        var button = GetComponent<Button>();
        button.onClick.AddListener(() =>
        {
            SceneManager.LoadScene("TitleScene");
        });
    }
}
