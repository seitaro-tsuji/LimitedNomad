using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem.XR;

[RequireComponent(typeof(Collider))]
public class WBCollider : MonoBehaviour
{
    [SerializeField] private Transform player;
    [SerializeField] public CraftingPanel craftingPanel;
    private PlayerController _controller;

    // Start is called before the first frame update
    void Start()
    {
        _controller = player.GetComponent<PlayerController>();
    }

    //コライダーの範囲内にプレイヤーが入ったときの処理
    public void OnEnterDetect(Collider collider)
    {
        if (collider.CompareTag("Player"))
        {
            //Debug.Log("wb enter")
            craftingPanel.DetectPlayer(); //パネル表示
        }
    }
}
