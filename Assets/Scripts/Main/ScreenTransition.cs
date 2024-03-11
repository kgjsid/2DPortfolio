using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class ScreenTransition : MonoBehaviour
{
    [SerializeField] Transform target; // 이동해야 할 위치

    public Vector2 ReturnPosition()
    {
        return target.position;
    }
}
