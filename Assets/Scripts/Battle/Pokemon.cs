using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class Pokemon : MonoBehaviour, IDamagable
{
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

    [SerializeField] int curHp;           // ���ϸ� ���� ü��
    [SerializeField] int curExp;          // ���ϸ��� ���� ����ġ
    [SerializeField] int nextExp;         // ���������� ����ġ??

    [SerializeField] Pokemon enemy;                      // ���濡 ���� ����
    [SerializeField] Skill currentAction;            // ���� ������ �׼�
    [SerializeField] SpriteRenderer sprite;
    [SerializeField] List<Skill> currentSkills = new List<Skill>();      // ���� ��ų��
    [SerializeField] SkillEffectAnimation effect;
    [SerializeField] Animator animator;

    private float typeValue;
    private BattleEffect effective;

    // ������Ƽ...
    public int Hp { get => hp; set => hp = value; }
    public int CurHp { get => curHp; set => curHp = value; }
    public int Speed { get => speed; set => speed = value; }
    public int Damage { get => damage; set => damage = value; }
    public int SpecialDamage { get => specialDamage; set => specialDamage = value; }
    public int Defence { get => defence; set => defence = value; }
    public int SpecialDefence { get => specialDefence; set => specialDefence = value; }
    public Pokemon Enemy { get => enemy; set => enemy = value; }
    public string Name { get => data.name; }
    public int Level { get => level; set => level = value; }
    public List<Skill> CurrentSkill { get => currentSkills; }
    public PokemonData PokemonData { get => data; set => data = value; }
    public SpriteRenderer Sprite { get => sprite; }
    public Skill CurAction { get => currentAction; }
    public BattleEffect Effective { get => effective; }
    public int CurExp { get => curExp; set => curExp = value; }
    public int NextExp { get => nextExp; set => nextExp = value; }

    private void OnEnable()
    {
        if(controlType == 1)
        {
            animator.Play("Idle");
        }
    }

    public void SetBattle()
    {
        SetButtons();
        nextExp = (level + 1) * (level + 1) * (level + 1);
    }
    private void SetButtons()
    {   // ��ư�� ���� �ʱ� ����(��ų �����ϱ�)
        if (controlType == 1)
        {
            // �߻������� ��ư�� ������ �ʿ� ���� ��ų ���ø�
            currentSkills.Clear();
        }
        else
        {
            for (int i = 0; i < buttons.Length; i++)
            {
                buttons[i].SetOwner(this); // ��ư ��� ��ü?
                if (currentSkills.Count <= i)
                {
                    continue;
                }
                buttons[i].SetButton(currentSkills[i]); // ��ư�� ��ų �����ϱ�
                effect.SetEffectAnimator($"{currentSkills[i].Skilldata.name}");
            }
        }
    }

    // ��ư�� ������ �� ���� �׼� ���� �޼ҵ�
    public void SetAction(Skill action)
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

        return currentAction.Skilldata;
    }
    public bool TakeDamage(int damage)
    {
        curHp -= damage;
        animator.Play("Hit");
        if (curHp <= 0)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    // ����
    public int Execute()
    {
        if(controlType == 1)
        {
            currentAction = currentSkills[Random.Range(0, currentSkills.Count)];
        }
        BattleManager.Battle.DisplayLog($"{Name} used {currentAction.Skilldata.name}!");
        float typeValue = currentAction.Skilldata.TypeValue[(int)currentAction.Skilldata.type, (int)enemy.data.type];
        if(typeValue > 1f)
        {
            effective = BattleEffect.HighEffective;
        }
        else if(typeValue < 1f)
        {
            effective = BattleEffect.LowEffective;
        }
        else
        {
            effective = BattleEffect.Normal;
        }
        int damage = (int)(currentAction.Skilldata.Execute(this, enemy) * typeValue);
        currentAction.CurPP--;
        bool isAnim = effect.UseEffect($"{currentAction.Skilldata.name}");
        if (isAnim) animator.Play("Attack");
        if (controlType == 1)
            animator.Play("Attack");
        currentAction = null;
        return damage > 1 ? damage : 1;
    }

    public bool GetExp(int exp)
    {
        curExp += exp;

        if (curExp >= (level + 1) * (level + 1) * (level + 1))
            return true;
        else
            return false;
    }

    public bool LevelUp()
    {
        // ������ ��ƾ
        // �� ��ų������ ȹ���ϰ�, ��ȭ�ϰ�??
        level++;
        return GetSkills();
    }

    public bool GetSkills()
    {
        for (int i = currentSkills.Count; i < data.skillData.Length; i++)
        {
            if (currentSkills.Count == 4)
                break; // ���� �ʿ�. 4�� �̻��̸� ��� �����ؼ� ȹ���ؾ� ��

            foreach(Skill skill in currentSkills)
            {
                // �̹� ��� ��ų�̶��
                if(skill.Skilldata == data.skillData[i].skill)
                {
                    return false;
                }
            }

            // ���ο� ��ų ���� InitSkill�� �ʱ�ȭ ����
            if (level >= data.skillData[i].level)
            {
                currentSkills.Add(new Skill());
                currentSkills[i].InitSkill(data.skillData[i].skill);
                // currentSkills.Add(data.skillData[i].skill);
                return true;
            }
        }
        return false;
    }

    public void Die()
    {
        animator.Play("Faint");
    }
}
