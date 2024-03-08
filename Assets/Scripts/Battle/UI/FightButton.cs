using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class FightButton : ActionUI
{
    public UnityEvent OnClick;

    public void FightClick()
    {
        Debug.Log("On Click");
        OnClick?.Invoke();
    }
}
