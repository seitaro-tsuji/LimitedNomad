using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(Collider))]
public class CollisionDetecter : MonoBehaviour
{
    [SerializeField] private TriggerEvent onTriggerStay = new TriggerEvent();
    [SerializeField] private TriggerEvent onTriggerEnter = new TriggerEvent();

    private void OnTriggerStay(Collider other)
    {
        onTriggerStay.Invoke(other);
    }

    private void OnTriggerEnter(Collider other)
    {
        onTriggerEnter.Invoke(other);
    }

    [Serializable]
    public class TriggerEvent: UnityEvent<Collider>
    {

    }
}
