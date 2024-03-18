using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class EventEnCounter : MonoBehaviour
{
    [SerializeField] LayerMask eventLayer;

    public UnityEvent enCounterEvent;

    public void Check()
    {
        if (!Physics2D.Raycast(transform.position, transform.up * (-1f), 0.5f, eventLayer))
            return;

        Debug.Log("이벤트 발생!");
        enCounterEvent?.Invoke();
    }
}
