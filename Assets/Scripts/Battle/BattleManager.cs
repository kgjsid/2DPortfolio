using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleManager : Singleton<BattleManager>
{
    [SerializeField] Pokemon player;    // 배틀에 필요한 포켓몬 둘
    [SerializeField] Pokemon enemy;

    [SerializeField] BattleUI playerUI;
    [SerializeField] BattleUI enemyUI;

    Queue<Pokemon> actionRank;       // 큐에 행동을 담아두고, 스피드 순서로 넣으면 꺼낼 땐 먼저 나오니 이용
    List<SkillData> enemyActions;
    [SerializeField] SetPokemonData data;

    int playerCurHp;
    int enemyCurHp;

    private void OnEnable()
    {
        enemyActions = enemy.PossessedAction; // 상대방이 할 수 있는 행동 저장하기
        SetUIs();
        data.SetPokemon(player);
        data.SettingData();
        data.SetPokemon(enemy);
        data.SettingData();
        player.Enemy = enemy;
        enemy.Enemy = player;
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

    public void SetUIs()
    {
        playerUI.SetName(player.Name);
        playerUI.SetLevel(player.Level);
        enemyUI.SetName(enemy.Name);
        enemyUI.SetLevel(enemy.Level);
    }

    IEnumerator BattleRoutine()
    { 
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
        Debug.Log("행동 시작");

        while(actionRank.Count > 0)     // 큐에 담아둔 모든 행동 실행
        {
            Debug.Log("행동 실행");
            playerCurHp = player.Hp;
            enemyCurHp = enemy.Hp;
            Pokemon nextAction = actionRank.Dequeue();
            nextAction.Execute();

            Debug.Log("적 체력 루틴시작");
            yield return enemyUI.HpRoutine(enemyCurHp, enemy.Hp);
            Debug.Log("적 체력 루틴끝");
            yield return playerUI.HpRoutine(playerCurHp, player.Hp);
            Debug.Log("아군 체력 루틴 끝");

        }

        Debug.Log("행동 끝");
        player.SetAction(null);
        EndBattle();
    }

    IEnumerator SelectRoutine()
    {
        Debug.Log("플레이어의 선택을 기다립니다.");
        yield return new WaitUntil(() => (player.GetAction() != null));
        actionRank.Enqueue(player);         // 선택이 끝난 액션은 큐에 넣고
        Debug.Log("선택 끝");
    }

    private void EnemyRoutine() // 상대방 루틴
    {
        // 랜덤한 상대방의 행동 넣기
        Debug.Log("상대방이 행동을 정하고 있습니다...");
        actionRank.Enqueue(enemy);
        Debug.Log("선택 완료");
    }
}
