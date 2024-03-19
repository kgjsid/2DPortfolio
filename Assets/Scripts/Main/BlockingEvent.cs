using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlockingEvent : MonoBehaviour
{
    public void Update()
    {
        if(Manager.Game.curCount != 0)
        {
            gameObject.SetActive(false);
        }
    }
}
