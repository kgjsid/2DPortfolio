using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Pokemon : MonoBehaviour, IDamagable
{
    [SerializeField] string name;           // �̸�
    [SerializeField] int level;             // ����
    [SerializeField] int hp;                // HP
    [SerializeField] int damage;            // Damage
    [SerializeField] int controlType;       // 0�̸� player, 1�̸� enemy?
    [SerializeField] float speed;           // Speed

    [SerializeField] Pokemon enemy;                     // ���� ���ϸ�
    [SerializeField] BaseAction currentAction;          // ���� �� ���ϸ��� �׼�
    [SerializeField] List<BaseAction> possessedAction;  // �� �� �ִ� �ൿ��(max 4)
    [SerializeField] ActionButton[] buttons;            // ��ư�� �Ҵ��� �ൿ��
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
            buttons[i].SetOwner(this);                  //��ư�� ������ ����

            if (possessedAction.Count <= i)
                return;

            buttons[i].SetAction(possessedAction[i]);   // �� ��ư�� �׼� ����
        }
    }

    // ��ư�� ������ �� ���� �׼� ����
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
        Debug.Log($"{damage}�� ������ �޾Ҵ�!");
        hp -= damage;
        Debug.Log($"{hp}�� ü�� ����");
        if (hp <= 0)
        {
            Destroy(this.gameObject);
        }
    }
}
