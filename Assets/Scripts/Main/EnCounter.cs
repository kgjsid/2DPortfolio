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

        int random = Random.Range(0, 100);

        if(random < 90)
        {
            return;
        }
        battleStart?.Invoke();
        
    }

}
