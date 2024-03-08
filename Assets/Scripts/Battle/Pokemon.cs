using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Pokemon : MonoBehaviour, IDamagable
{
    // TODO.
    // �ڵ� �ھ���...
    // ����� �ϼ��Ǿ� ������ ��� �� ���� �� ��ũ���ͺ�� ��ü�ؾ� ��
    [SerializeField] PokemonData data;      // ���ϸ� ������
    [SerializeField] ActionButton[] buttons;

    public int controlType;     // ��Ʈ�� Ÿ��(0 : �÷��̾�, 1 : �߻�����?, 2 : ��??)

    // ���� ���� / �ɷ�ġ(Permanent Stats)
    [Header("PokemonStats")]
    [SerializeField] int level;           // ���ϸ� ����
    [SerializeField] int hp;              // ���ϸ� HP
    [SerializeField] int damage;          // ���ϸ� ����������
    [SerializeField] int defence;         // ���ϸ� ��������
    [SerializeField] int specialDamage;   // ���ϸ� Ư��������
    [SerializeField] int specialDefence;  // ���ϸ� Ư������
    [SerializeField] int speed;           // ���ϸ� ���ǵ�

    [SerializeField] Pokemon enemy;                      // ���濡 ���� ����
    [SerializeField] SkillData currentAction;            // ���� ������ �׼�

    // ������Ƽ...
    public int Hp { get => hp; set => hp = value; }
    public int Speed { get => speed; set => speed = value; }
    public int Damage { get => damage; set => damage = value; }
    public int SpecialDamage { get => specialDamage; set => specialDamage = value; }
    public int Defence { get => defence; set => defence = value; }
    public int SpecialDefence { get => specialDefence; set => specialDefence = value; }
    public Pokemon Enemy { get => enemy; set => enemy = value; }
    public string Name { get => data.name; }
    public int Level { get => level; }
    public List<SkillData> PossessedAction { get => data.possessedAction; }
    public PokemonData PokemonData { get => data; }

    public void SetBattle()
    {
        SetButtons();
    }
    private void SetButtons()
    {   // ��ư�� ���� �ʱ� ����(��ų �����ϱ�)
        if (controlType == 1)
        {
            // �߻������� ��ư�� ������ �ʿ� ���� ��ų ���ø�
        }
        else
        {
            for (int i = 0; i < buttons.Length; i++)
            {
                buttons[i].SetOwner(this); // ��ư ��� ��ü?
                if (PossessedAction.Count <= i)
                {
                    continue;
                }
                buttons[i].SetButton(PossessedAction[i]); // ��ư�� ��ų �����ϱ�
            }
        }
    }

    // ��ư�� ������ �� ���� �׼� ���� �޼ҵ�
    public void SetAction(SkillData action)
    {
        if (controlType == 1)
            return;
        currentAction = action;
    }

    // ��Ʋ �Ŵ������� ����� ��ų �Ѱ��ֱ�
    public SkillData GetAction()
    {
        if (currentAction == null || controlType == 1)
            return null;

        return currentAction;
    }
    public void TakeDamage(int damage)
    {
        hp -= damage;
        Debug.Log($"{hp}�� ü�� ����");
        if (hp <= 0)
        {
            Destroy(this.gameObject);
        }
    }

    // ����
    public void Execute()
    {
        if(controlType == 1)
        {
            currentAction = PossessedAction[Random.Range(0, PossessedAction.Count)];
        }
        BattleManager.Battle.DisplayLog($"{Name} used {currentAction.name}!");
        currentAction.Execute(this, enemy);
        currentAction = null;
    }
}
