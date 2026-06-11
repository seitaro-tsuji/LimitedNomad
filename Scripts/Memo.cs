using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Memo : MonoBehaviour
{
    [TextArea(3, 10)]
    [SerializeField] private string memo;
}
