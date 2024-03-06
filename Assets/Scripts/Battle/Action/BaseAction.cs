using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseAction : MonoBehaviour
{
    // 나중에 자식에서 강제하는 추상클래스로 제작
    protected Pokemon owner;
    [SerializeField] private string actionName;

    public string ActionName { get => actionName; }
    public virtual void Execute() // 추상함수로 만들거 1
    {
        Debug.Log("자식에서 정의되지 않음");
        Debug.Log("실행함수");
    }

    // 현재 스킬을 들고 있는 객체
    public void SetOwner(Pokemon owner)
    {
        this.owner = owner;
    }
}
