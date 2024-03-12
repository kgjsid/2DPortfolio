using System.Collections;
using System.Collections.Generic;
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

    [SerializeField] Pokemon enemy;                      // 상대방에 대한 정보
    [SerializeField] SkillData currentAction;            // 현재 선택한 액션
    [SerializeField] SpriteRenderer sprite;

    public UnityEvent OnDied;

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
    public List<SkillData> PossessedAction { get => data.possessedAction; }
    public PokemonData PokemonData { get => data; set => data = value; }
    public SpriteRenderer Sprite { get => sprite; }
    

    public void SetBattle()
    {
        SetButtons();
    }
    private void SetButtons()
    {   // 버튼에 대한 초기 설정(스킬 세팅하기)
        if (controlType == 1)
        {
            // 야생동물은 버튼에 연결할 필요 없음 스킬 세팅만
        }
        else
        {
            for (int i = 0; i < buttons.Length; i++)
            {
                buttons[i].SetOwner(this); // 버튼 사용 주체?
                if (PossessedAction.Count <= i)
                {
                    continue;
                }
                buttons[i].SetButton(PossessedAction[i]); // 버튼에 스킬 세팅하기
            }
        }
    }

    // 버튼이 눌렸을 때 현재 액션 설정 메소드
    public void SetAction(SkillData action)
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

        return currentAction;
    }
    public void TakeDamage(int damage)
    {
        hp -= damage;
        if (hp <= 0)
        {
            hp = 0;
            OnDied?.Invoke();
        }
    }

    // 실행
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
