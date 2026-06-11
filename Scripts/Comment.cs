using UnityEngine;

[DisallowMultipleComponent]
public class Comment : MonoBehaviour
{
    [TextArea(3, 10)]
    public string comment;
}