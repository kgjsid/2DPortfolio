using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Pokemon : MonoBehaviour, IDamagable
{
    [SerializeField] int level;
    [SerializeField] int hp;
    [SerializeField] int damage;
    [SerializeField] float speed;
    [SerializeField] int controlType;

    [SerializeField] Pokemon enemy;
    [SerializeField] BaseAction currentAction;
    [SerializeField] ActionButton button1;
    [SerializeField] ActionButton button2;
    [SerializeField] ActionButton button3;
    [SerializeField] ActionButton button4;

    public int Hp { get => hp; }
    public float Speed { get => speed; }
    public int Damage { get => damage; }
    public Pokemon Enemy { get => enemy; }

    private void Start()
    {
        if (controlType == 1)
            return;
        
        button1.SetOwner(this);
        button2.SetOwner(this);
    }

    public void SetAction(BaseAction action)
    {
        if (controlType == 1)
            return;
        currentAction = action;
    }

    public BaseAction GetAction()
    {
        if (currentAction == null || controlType == 1)
            return null;

        return currentAction;
    }
    public void TakeDamage(int damage)
    {
        Debug.Log($"{damage}의 공격을 받았다!");
        hp -= damage;
        Debug.Log($"{hp}의 체력 남음");
        if (hp <= 0)
        {
            Destroy(this.gameObject);
        }
    }
}
