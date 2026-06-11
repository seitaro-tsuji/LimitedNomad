using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.UI;

public class Menu : MonoBehaviour
{
    [SerializeField] private Button pauseButton;
    [SerializeField] private GameObject pausePanel;
    [SerializeField] private Button resumeButton;

    [SerializeField] private Button itemsButton;
    [SerializeField] private ItemsDialog itemsDialog;
    // Start is called before the first frame update
    void Start()
    {
        pausePanel.SetActive(false);//ポーズのパネルは初期状態では非表示

        pauseButton.onClick.AddListener(Pause);
        resumeButton.onClick.AddListener(Resume);
        itemsButton.onClick.AddListener(ToggleItemsDialog);
    }

    private void Pause()
    {
        Time.timeScale = 0;
        pausePanel.SetActive(true);
    }

    private void Resume()
    {
        Time.timeScale = 1;
        pausePanel.SetActive(false);
    }

    private void ToggleItemsDialog()
    {
        itemsDialog.Toggle();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
