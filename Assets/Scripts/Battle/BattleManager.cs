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

    private void OnEnable()
    {
        isRoutine = false;
        Battle();
    }

    private void OnDisable()
    {
        EndBattle();
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

    IEnumerator BattleRoutine()
    {
        isRoutine = true;
        yield return SelectRoutine();   // 플레이어 선택

        Debug.Log("행동 시작");

        while(actionRank.Count > 0)     // 큐에 담아둔 모든 행동 실행
        {
            Debug.Log("행동 실행");
            BaseAction nextAction = actionRank.Dequeue();
            nextAction.Execute();
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
}
