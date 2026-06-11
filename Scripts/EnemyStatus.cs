using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
public class EnemyStatus : MobStatus
{
    private NavMeshAgent _agent;
    // Start is called before the first frame update
    protected override void Start()
    {
        base.Start();
        _agent = GetComponent<NavMeshAgent>();

        //ライフゲージの表示を開始
        LifeGaugeContainer.Instance.Add(this);
    }

    // Update is called once per frame
    private void Update()
    {
        _animator.SetFloat("MoveSpeed", _agent.velocity.magnitude);
    }

    protected override void OnDie()
    {
        base.OnDie();
        StartCoroutine(DestroyCoroutine());
        LifeGaugeContainer.Instance.Remove(this);//ライフゲージの表示を終了
    }

    //倒されたときの消滅コルーチン
    private IEnumerator DestroyCoroutine()
    {
        yield return new WaitForSeconds(3);
        Destroy(gameObject);
    }
}
