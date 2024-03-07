using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleManager : Singleton<BattleManager>
{
    [SerializeField] Pokemon player;    // 배틀에 필요한 포켓몬 둘
    [SerializeField] Pokemon enemy;

    [SerializeField] BattleUI playerUI;
    [SerializeField] BattleUI enemyUI;
    [SerializeField] BattleSceneFlow battle;

    Queue<Pokemon> actionRank;       // 큐에 행동을 담아두고, 스피드 순서로 넣으면 꺼낼 땐 먼저 나오니 이용
    List<SkillData> enemyActions;
    [SerializeField] SetPokemonData data;

    int playerCurHp;
    int enemyCurHp;

    public void SetBattle()
    {
        battle.BattleUI();
        enemyActions = enemy.PossessedAction; // 상대방이 할 수 있는 행동 저장하기
        SetUIs();
        SetDatas();
        Battle();
    }

    private void OnDisable()
    {
    }

    private void Awake()
    {
        actionRank = new Queue<Pokemon>();
    }

    public void Update()
    {
        /*
        if(!isRoutine)
        {
            Battle();
        }
        */
    }

    private void Battle()
    {
        StartCoroutine(BattleRoutine());
    }

    private void EndBattle()
    {
        // 배틀의 뒤처리? 일단 배틀을 다시 시작하도록?
        Battle();
    }

    public void SetUIs() // UI 설정
    {
        playerUI.SetName(player.Name);
        playerUI.SetLevel(player.Level);
        enemyUI.SetName(enemy.Name);
        enemyUI.SetLevel(enemy.Level);
    }

    public void SetDatas() // 포켓몬 설정
    {
        data.SetPokemon(player);
        data.SettingData();
        data.SetPokemon(enemy);
        data.SettingData();
        player.Enemy = enemy;
        enemy.Enemy = player;
    }

    // 배틀 루틴
    IEnumerator BattleRoutine()
    {   // 1. 행동 선택
        if (player.Speed > enemy.Speed)
        {
            yield return SelectRoutine();   // 1. 플레이어 선택
            EnemyRoutine();                 // 2. 적 선택
        }
        else
        {
            EnemyRoutine();                 // 1. 적 선택
            yield return SelectRoutine();   // 2. 플레이어 선택
        }
        // 2. 행동 실행
        while(actionRank.Count > 0)     // 큐에 담아둔 모든 행동 실행
        {
            playerCurHp = player.Hp;
            enemyCurHp = enemy.Hp;
            Pokemon nextAction = actionRank.Dequeue();
            nextAction.Execute();

            yield return enemyUI.HpRoutine(enemyCurHp, enemy.Hp);
            yield return playerUI.HpRoutine(playerCurHp, player.Hp);
        }
        // 3. 뒤 처리
        player.SetAction(null);
        EndBattle();
    }

    IEnumerator SelectRoutine()
    {
        yield return new WaitUntil(() => (player.GetAction() != null));
        actionRank.Enqueue(player);         // 선택이 끝난 액션은 큐에 넣고
    }

    private void EnemyRoutine() // 상대방 루틴
    {
        // 랜덤한 상대방의 행동 넣기
        actionRank.Enqueue(enemy);
    }
}
