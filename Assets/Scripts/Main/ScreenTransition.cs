using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class ScreenTransition : MonoBehaviour
{
    [SerializeField] Transform target; // �̵��ؾ� �� ��ġ

    public Vector2 ReturnPosition()
    {
        return target.position;
    }
}
