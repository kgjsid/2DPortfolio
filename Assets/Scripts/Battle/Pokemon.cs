using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class Pokemon : MonoBehaviour, IDamagable
{
    [SerializeField] PokemonData data;      // 포켓몬 데이터
    [SerializeField] ActionButton[] buttons;

    public int controlType;     // 컨트롤 타입(0 : 플레이어, 1 : 야생동물?, 2 : 적??)

    // 실제 스탯 / 능력치(Permanent Stats)
    [Header("PokemonStats")]
    [SerializeField] int level;           // 포켓몬 레벨
    [SerializeField] int hp;              // 포켓몬 HP
    [SerializeField] int damage;          // 포켓몬 물리데미지
    [SerializeField] int defence;         // 포켓몬 물리방어력
    [SerializeField] int specialDamage;   // 포켓몬 특수데미지
    [SerializeField] int specialDefence;  // 포켓몬 특수방어력
    [SerializeField] int speed;           // 포켓몬 스피드

    [SerializeField] int curHp;           // 포켓몬 현재 체력
    [SerializeField] int curExp;          // 포켓몬의 현재 경험치
    [SerializeField] int nextExp;         // 다음까지의 경험치??

    [SerializeField] Pokemon enemy;                      // 상대방에 대한 정보
    [SerializeField] Skill currentAction;            // 현재 선택한 액션
    [SerializeField] SpriteRenderer sprite;
    [SerializeField] List<Skill> currentSkills = new List<Skill>();      // 현재 스킬들
    [SerializeField] SkillEffectAnimation effect;
    [SerializeField] Animator animator;

    private float typeValue;
    private BattleEffect effective;

    // 프로퍼티...
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
    {   // 버튼에 대한 초기 설정(스킬 세팅하기)
        if (controlType == 1)
        {
            // 야생동물은 버튼에 연결할 필요 없음 스킬 세팅만
            currentSkills.Clear();
        }
        else
        {
            for (int i = 0; i < buttons.Length; i++)
            {
                buttons[i].SetOwner(this); // 버튼 사용 주체?
                if (currentSkills.Count <= i)
                {
                    continue;
                }
                buttons[i].SetButton(currentSkills[i]); // 버튼에 스킬 세팅하기
                effect.SetEffectAnimator($"{currentSkills[i].Skilldata.name}");
            }
        }
    }

    // 버튼이 눌렸을 때 현재 액션 설정 메소드
    public void SetAction(Skill action)
    {
        if (controlType == 1)
            return;
        currentAction = action;
    }

    // 배틀 매니저에게 사용할 스킬 넘겨주기
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

    // 실행
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
        // 레벨업 루틴
        // 내 스킬데이터 획득하고, 진화하고??
        level++;
        return GetSkills();
    }

    public bool GetSkills()
    {
        for (int i = currentSkills.Count; i < data.skillData.Length; i++)
        {
            if (currentSkills.Count == 4)
                break; // 수정 필요. 4개 이상이면 기술 선택해서 획득해야 함

            foreach(Skill skill in currentSkills)
            {
                // 이미 배운 스킬이라면
                if(skill.Skilldata == data.skillData[i].skill)
                {
                    return false;
                }
            }

            // 새로운 스킬 배우기 InitSkill로 초기화 설정
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
