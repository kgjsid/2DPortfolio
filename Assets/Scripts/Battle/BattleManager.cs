using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleManager : MonoBehaviour
{
    private static BattleManager instance;
    public static BattleManager Battle {get => instance;}

    // 수정하기?
    [Header("Pokemon")]
    [SerializeField] Pokemon player;    // 배틀에 필요한 포켓몬 둘
    [SerializeField] Pokemon enemy;

    [Header("UI")]
    [SerializeField] BattleUI playerUI;
    [SerializeField] BattleUI enemyUI;
    [SerializeField] DiaLogUI battleLog;       // 배틀 로그(누가 출현.. 무슨 공격..)
    [SerializeField] PopUpUI selectLog;        // 행동 선택창
    [SerializeField] PopUpUI skillSlot;        // 스킬 선택창

    Queue<Pokemon> actionRank;       // 큐에 행동을 담아두고, 스피드 순서로 넣으면 꺼낼 땐 먼저 나오니 이용
    List<SkillData> enemyActions;
    [SerializeField] SetPokemonData data;

    int playerCurHp;
    int enemyCurHp;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }
    private void Start()
    { 
        actionRank = new Queue<Pokemon>();
        battleLog.gameObject.SetActive(false);
        selectLog.gameObject.SetActive(false);
        skillSlot.gameObject.SetActive(false);
        SetBattle();
    }
    public void SetBattle()
    {   // 배틀 세팅
        SetDatas();
        StartCoroutine(SetUIs());
    }

    private void StartBattleRoutine()
    {   // 배틀 루틴 시작
        StartCoroutine(BattleRoutine());
    }

    private void EndBattle()
    {
        // 배틀의 뒤처리? 일단 배틀을 다시 시작하도록?
        StartBattleRoutine();
    }

    // 배틀 루틴
    IEnumerator BattleRoutine()
    {   // 1. 행동 선택

        if(skillSlot.gameObject.active == false)
        {
            skillSlot.gameObject.SetActive(true);
            player.SetBattle();
        }

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
        skillSlot.gameObject.SetActive(false); // 먼저 스킬은 선택했으니 비활성화
        battleLog.gameObject.SetActive(true);
        while(actionRank.Count > 0)     // 큐에 담아둔 모든 행동 실행
        {
            playerCurHp = player.Hp;
            enemyCurHp = enemy.Hp;
            Pokemon nextAction = actionRank.Dequeue();
            nextAction.Execute();

            yield return new WaitForSeconds(2f);
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

    IEnumerator SetUIs() // UI 설정
    {
        playerUI.SetName(player.Name);
        playerUI.SetLevel(player.Level);
        enemyUI.SetName(enemy.Name);
        enemyUI.SetLevel(enemy.Level);

        battleLog.gameObject.SetActive(true);
        yield return battleLog.DisplayLog($"a wile {enemy.Name} appeared!");

        selectLog.gameObject.SetActive(true);
    }

    public void SetDatas() // 포켓몬 설정
    {
        data.SetPokemon(player); // 데이터 설정(어떤 포켓몬인지 넘겨주고)
        data.SettingData();      // 포켓몬의 능력치 설정(종족치와 레벨에 따라서 설정)
        data.SetPokemon(enemy); 
        data.SettingData();
        player.Enemy = enemy;    // 상대방 정보 넘겨주고
        enemy.Enemy = player;
        enemyActions = enemy.PossessedAction;   // 적은 랜덤으로 상태를 선택할 수 있도록
    }

    public void StartBattle() // 배틀 시작 설정
    {
        Debug.Log("Manger StartBattle");
        skillSlot.gameObject.SetActive(true);  // 스킬 선택버튼은 띄우고
        selectLog.gameObject.SetActive(false); // 행동 선택은 비활성화
        player.SetBattle();
        enemy.SetBattle();
        StartBattleRoutine();
    }


    public void DisplayLog(string text)
    {
        StartCoroutine(battleLog.DisplayLog(text));
    }
    
}
