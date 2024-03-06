using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Pokemon : MonoBehaviour, IDamagable
{
    [SerializeField] string name;           // 이름
    [SerializeField] int level;             // 레벨
    [SerializeField] int hp;                // HP
    [SerializeField] int damage;            // Damage
    [SerializeField] int controlType;       // 0이면 player, 1이면 enemy?
    [SerializeField] float speed;           // Speed

    [SerializeField] Pokemon enemy;                     // 상대방 포켓몬
    [SerializeField] BaseAction currentAction;          // 현재 내 포켓몬의 액션
    [SerializeField] List<BaseAction> possessedAction;  // 할 수 있는 행동들(max 4)
    [SerializeField] ActionButton[] buttons;            // 버튼에 할당할 행동들
    enum ControlType
    {
        player,
        enemy
    }

    public int Hp { get => hp; }
    public float Speed { get => speed; }
    public int Damage { get => damage; }
    public Pokemon Enemy { get => enemy; }
    public string Name { get => name; }
    public int Level { get => level; }
    public List<BaseAction> PossessedAction { get => possessedAction; }

    private void Start()
    {

        if (controlType == 1)
        {
            foreach (BaseAction action in PossessedAction)
            {
                action.SetOwner(this);
            }
        }
        
        for(int i = 0; i < buttons.Length; i++)
        {
            buttons[i].SetOwner(this);                  //버튼의 주인은 나다

            if (possessedAction.Count <= i)
                return;

            buttons[i].SetAction(possessedAction[i]);   // 각 버튼에 액션 장착
        }
    }

    // 버튼이 눌렸을 때 현재 액션 설정
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
