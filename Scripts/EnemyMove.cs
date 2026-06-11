using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
[RequireComponent (typeof(MobStatus))]
public class EnemyMove : MonoBehaviour
{
    [SerializeField] private LayerMask raycastLayerMask;//レイヤーマスク
    private NavMeshAgent _agent;
    private RaycastHit[] _raycastHits = new RaycastHit[10];
    private EnemyStatus _status;

    // Start is called before the first frame update
    void Start()
    {
        _agent = GetComponent<NavMeshAgent>();
        _status = GetComponent<EnemyStatus>();

        // デバッグメッセージ
        /*if (_agent == null)
        {
            Debug.LogError("NavMeshAgent is missing!");
        }
        else
        {
            Debug.LogError("good");
        }
        if (_status == null)
        {
            Debug.LogError("EnemyStatus is missing!");
        }
        else
        {
            Debug.LogError("good");
        }*/
    }

    public void OnDetectObject(Collider collider)
    {
        //Debug.Log("検知中");
        //nullチェック
        if (_status == null)
        {
            Debug.LogError("_status is null!");
            return;
        }
        if (_agent == null)
        {
            Debug.LogError("_agent is null!");
            return;
        }
        if (collider == null)
        {
            Debug.LogError("Collider is null!");
            return;
        }


        if (!_status.IsMovable)
        {
            _agent.isStopped = true;
            return;
        }

        if (collider.CompareTag("Player"))
        {   var positionDiff = collider.transform.position - transform.position;//自身とプレイヤーの差分座標を計算
            var distance =  positionDiff.magnitude;//距離
            var direction = positionDiff.normalized;//プレイヤーの方向

            var hitCount = Physics.RaycastNonAlloc(transform.position, direction, _raycastHits, distance, raycastLayerMask);
            if(hitCount == 0)
            {
                //プレイヤーはcolliderを使用していないので障害物がなければhitcount0のはず
                _agent.isStopped = false;
                _agent.destination = collider.transform.position;
            }
            else
            {
                _agent.isStopped = true;//見失ったら止まる
            }
        }
    }
}
