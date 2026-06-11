using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemThrownAxe : MonoBehaviour
{
    [SerializeField] private float speed = 10f;         //速さ
    [SerializeField] private float rotateSpeed = 720f;  //回転速度
    [SerializeField] private int damage = 3;            //ダメージ
    private Vector3 moveDirection;  //動く方向

    // Update is called once per frame
    void Update()
    {
        transform.position += moveDirection * speed * Time.deltaTime;
        transform.Rotate(Vector3.forward * rotateSpeed * Time.deltaTime);
    }

    public void Initialize(Vector3 direction)
    {
        moveDirection = direction.normalized;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Enemy"))
        {
            //Debug.Log("ヒット");

            //ダメージを与える
            MobStatus targetStatus = other.GetComponent<MobStatus>();
            targetStatus.Damage(damage);

            Destroy(gameObject);
        }
    }
}
