using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class EnCounter : MonoBehaviour
{
    [SerializeField] LayerMask battleGrass;

    public UnityEvent battleStart;
    private void Start()
    {
    }

    public void Check()
    {
        if (!Physics2D.Raycast(transform.position, transform.up * (-1f), 0.5f, battleGrass))
            return;

        Debug.Log("배틀 풀 만남!!");
        int random = Random.Range(0, 100);

        if(random < 90)
        {
            return;
        }
        Debug.Log($"{random} 배틀 씬 전환해야 함");
        battleStart?.Invoke();
        
    }

}
