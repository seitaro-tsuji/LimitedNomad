using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(MobStatus))]
public class MobAttack : MonoBehaviour
{
    [SerializeField] private float attackCooldown = 0.5f;   //攻撃のクールダウン
    [SerializeField] private int attackPower = 1;           //攻撃の威力
    [SerializeField] private Collider attackCollider;

    private MobStatus _status;

    // Start is called before the first frame update
    void Start()
    {
        _status = GetComponent<MobStatus>();
    }

    //攻撃可能な状態なら攻撃を行う
    public void AttackIfPossible()
    {
        if (!_status.IsAttackable) return;
        _status.GoToAttackStateIfPossible();
    }

    //攻撃対象が範囲に入ったときに呼ばれる
    public void OnAttackRangeEnter()
    {
        AttackIfPossible();
    }

    //攻撃の開始時に呼ばれる 攻撃判定を出す
    public void OnAttackStart()
    {
        attackCollider.enabled = true;
    }

    //攻撃判定がヒットしたときに呼ばれる
    public void OnHitAttack(Collider collider)
    {
        var targetMob = collider.GetComponent<MobStatus>(); 
        if(null == targetMob ) return;

        targetMob.Damage(attackPower);//ダメージを与える
        Debug.Log(gameObject.name + "の攻撃がヒット");
    }

    //攻撃の終了時に呼ばれる 攻撃判定を消す
    public void OnAttackFinished()
    {
        attackCollider.enabled = false;
        StartCoroutine(CooldownCoroutine());//クールダウン
    }

    private IEnumerator CooldownCoroutine()
    {
        yield return new WaitForSeconds(attackCooldown);
        _status.GoToNormalStateIfPossible();
    }
}
