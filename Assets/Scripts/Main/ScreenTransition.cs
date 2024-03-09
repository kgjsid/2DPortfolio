using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScreenTransition : MonoBehaviour
{
    [SerializeField] Transform[] points;
    Vector2 currentPos;
    Vector2 targetPos;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        // �浹�Ͽ��� �� �̵� ��ƾ?
        
    }

    private void Transition()
    {
        float minDistance = Mathf.Infinity;
        foreach(Transform temp in points)
        {
            float distance = Vector2.Distance(temp.position, currentPos);
            if(minDistance > distance)
            {
                minDistance = distance;
                currentPos = temp.position;
            }
        }

        
    }

}
