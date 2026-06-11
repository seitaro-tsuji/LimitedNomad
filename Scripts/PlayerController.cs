using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;

[RequireComponent(typeof(CharacterController))]
[RequireComponent(typeof(PlayerInput))]
[RequireComponent (typeof(PlayerStatus))]
[RequireComponent(typeof(MobAttack))]
public class PlayerController : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 3;//動くスピード
    [SerializeField] private Animator animator; //キャラクターのアニメーター
    [SerializeField] private Transform cameraTransform;     //カメラ

    private bool thisFrameControllable; //1フレームだけ操作無効にする用　1フレームだけ計算を止めたいときなど
    public bool isControllable; //操作可能状態か　イベント中や移動処理の途中はfalseになる
    private CharacterController characterController;    //characterControllerのキャッシュ
    private Transform _transform;//transformのキャッシュ
    private Vector3 moveVelocity;//速度
    private PlayerInput input;
    private InputAction _move;  //アクションのキャッシュ
    private InputAction _fire;
    private PlayerStatus _status;
    private MobAttack _attack;
    // Start is called before the first frame update
    void Start()
    {
        characterController = GetComponent<CharacterController>();
        _transform = transform;
        _status = GetComponent<PlayerStatus>();
        _attack = GetComponent<MobAttack>();

        input = GetComponent<PlayerInput>();
        input.currentActionMap.Enable();    //インプットのアクションマップを有効化
        _move = input.currentActionMap.FindAction("Move");
        _fire = input.currentActionMap.FindAction("Fire");

        isControllable = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (!thisFrameControllable) 
        { 
            thisFrameControllable = true;//次フレームでは操作可能にする
            return;
        }

        //移動可能な時(このクラス内とstatusクラス内で共に移動可能なとき)は移動入力可能
        CulculateVelocityXZ();    //入力によって移動
        _transform.LookAt(_transform.position + new Vector3(moveVelocity.x, 0, moveVelocity.z));


        //攻撃アクション
        if (_fire.WasPressedThisFrame() && isControllable)
        {
            if (EventSystem.current != null && EventSystem.current.IsPointerOverGameObject())
            {
                return;
            }

            _attack.AttackIfPossible();
        }

        //地上にいるときの操作
        if (characterController.isGrounded)
        {
            //Debug.Log("地上にいます");
            moveVelocity.y = -0.1f;
        }
        //いない時の操作
        else
        {
            moveVelocity.y += Physics.gravity.y * Time.deltaTime;//重力による加速
        }

        //計算した速度によってオブジェクトを動かす
        characterController.Move(moveVelocity * Time.deltaTime);
        animator.SetFloat("MoveSpeed", new Vector3(moveVelocity.x, 0, moveVelocity.z).magnitude);
    }

    //x,z方向の速度を計算する 入力とカメラ方向から
    public void CulculateVelocityXZ()
    {
        //カメラ方向
        Vector3 cameraForward = cameraTransform.forward;
        Vector3 cameraRight = cameraTransform.right;

        //y成分(高さ成分)はいらない
        cameraForward.y = 0;
        cameraRight.y = 0;

        //正規化
        cameraForward.Normalize();
        cameraRight.Normalize();

        //入力取得
        var moveValue = Vector2.zero;
        if (isControllable && _status.IsMovable)
        {
            moveValue = _move.ReadValue<Vector2>();
        }

        //入力とカメラの方向から動く方向を計算
        Vector3 moveDirectionXZ = cameraForward * moveValue.y + cameraRight * moveValue.x;

        //速度を計算
        moveVelocity.x = moveDirectionXZ.x * moveSpeed;
        moveVelocity.z = moveDirectionXZ.z * moveSpeed;
    }

    //動ける状態かどうかの変数を変更する
    public void Set_IsControllable(bool tf)
    {
        isControllable = tf;
    }

    public void Set_FrameControllable(bool tf)
    {
        thisFrameControllable = tf;
    }
}
