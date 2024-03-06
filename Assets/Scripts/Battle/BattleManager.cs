using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleManager : MonoBehaviour
{
    [SerializeField] Pokemon player;    // 배틀에 필요한 포켓몬 둘
    [SerializeField] Pokemon enemy;

    [SerializeField] BattleUI playerUI;
    [SerializeField] BattleUI enemyUI;

    Queue<BaseAction> actionRank;       // 큐에 행동을 담아두고, 스피드 순서로 넣으면 꺼낼 땐 먼저 나오니 이용

    [SerializeField] bool isRoutine;
    List<BaseAction> enemyActions;

    int playerCurHp;
    int enemyCurHp;

    private void OnEnable()
    {
        isRoutine = false;
        enemyActions = enemy.PossessedAction; // 상대방이 할 수 있는 행동 저장하기
        SetUIs();
        Battle();
    }

    private void OnDisable()
    {
    }

    private void Awake()
    {
        actionRank = new Queue<BaseAction>();
    }

    public void Update()
    {
        if(!isRoutine)
        {
            Battle();
        }
    }

    private void Battle()
    {
        StartCoroutine(BattleRoutine());
    }

    private void EndBattle()
    {
        // 배틀의 뒤처리?
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
        isRoutine = true;

        if (player.Speed > enemy.Speed)
        {
            yield return SelectRoutine();   // 플레이어 선택
            EnemyRoutine();
        }
        else
        {
            EnemyRoutine();
            yield return SelectRoutine();   // 플레이어 선택
        }
        Debug.Log("행동 시작");

        while(actionRank.Count > 0)     // 큐에 담아둔 모든 행동 실행
        {
            Debug.Log("행동 실행");
            playerCurHp = player.Hp;
            enemyCurHp = enemy.Hp;
            BaseAction nextAction = actionRank.Dequeue();
            nextAction.Execute();

            Debug.Log("적 체력 루틴시작");
            yield return enemyUI.HpRoutine(enemyCurHp, enemy.Hp);
            Debug.Log("적 체력 루틴끝");
            yield return playerUI.HpRoutine(playerCurHp, player.Hp);
            Debug.Log("아군 체력 루틴 끝");

        }

        Debug.Log("행동 끝");
        player.SetAction(null);
        isRoutine = false;
    }

    IEnumerator SelectRoutine()
    {
        Debug.Log("플레이어의 선택을 기다립니다.");
        yield return new WaitUntil(() => (player.GetAction() != null));
        actionRank.Enqueue(player.GetAction());         // 선택이 끝난 액션은 큐에 넣고
        Debug.Log("선택 끝");
    }

    private void EnemyRoutine() // 상대방 루틴
    {
        // 랜덤한 상대방의 행동 넣기
        Debug.Log("상대방이 행동을 정하고 있습니다...");
        actionRank.Enqueue(enemyActions[Random.Range(0, enemyActions.Count - 1)]);
        Debug.Log("선택 완료");
    }
}
