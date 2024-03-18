using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.UIElements;

public enum BattleStates { Start, Selecting, PlayerTurn, EnemyTurn, Win, Lose}
public enum BattleEffect { Normal, HighEffective, LowEffective}
public class BattleManager : MonoBehaviour
{
    private static BattleManager instance;
    public static BattleManager Battle {get => instance;}

    [Header("Pokemon")]
    [SerializeField] Pokemon player;    // 배틀에 필요한 포켓몬 둘
    [SerializeField] Pokemon enemy;

    [Header("UI")]
    [SerializeField] BattleUI playerUI;
    [SerializeField] BattleUI enemyUI;
    [SerializeField] DiaLogUI battleLog;       // 배틀 로그(누가 출현.. 무슨 공격..)
    [SerializeField] PopUpUI selectLog;        // 행동 선택창(싸울지, 도망칠지)
    [SerializeField] PopUpUI skillSlot;        // 스킬 선택창(버튼을 눌러 해당 스킬을 사용)

    [Header("Animation")]
    [SerializeField] PlayerBattleAnimation playerAnim;

    Queue<Pokemon> actionRank;          // 큐에 행동을 담아두고, 스피드 순서로 넣으면 꺼낼 땐 먼저 나오니 이용
    [SerializeField] SetPokemonData data;
    [SerializeField] PokemonData[] enemyData;

    public BattleStates states;

    Coroutine battleRoutine;

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
    // States.Start 시작
    public void InBattle()
    { 
        states = BattleStates.Start;
        StartCoroutine(SettingBattle());
    }
    IEnumerator SettingBattle()
    {   // 배틀 세팅
        playerAnim.gameObject.SetActive(true);
        states = BattleStates.Start;
        actionRank = new Queue<Pokemon>();      // 큐 만들고
        battleLog.gameObject.SetActive(false);  // 로그들은 비활성화
        selectLog.gameObject.SetActive(false);
        skillSlot.gameObject.SetActive(false);
        enemyUI.gameObject.SetActive(true);
        enemy.gameObject.SetActive(true);
        SetDatas();                             // 포켓몬 설정
        yield return StartCoroutine(SetUIs());  // UI 띄우기
    }
    private void SetDatas() // 포켓몬 설정
    {
        // 데이터 설정 및 상대 오브젝트 설정
        enemy.Level = Random.Range(3, 5);
        data.SetPokemon(enemy, enemyData[Random.Range(0, enemyData.Length)]);
        enemy.CurHp = enemy.Hp;
        enemy.Enemy = player;
    }
    IEnumerator SetUIs() // UI 설정
    {
        enemyUI.SetBattleUI(enemy);
        enemyUI.InitHpSlider(enemy.CurHp / (float)enemy.Hp);
        battleLog.gameObject.SetActive(true);   // 배틀 로그 띄워서
        yield return battleLog.DisplayLog($"a wile {enemy.Name} appeared!"); // a wile 적 등장
        playerUI.gameObject.SetActive(true);
        player.gameObject.SetActive(true);
        player.Level = Manager.Game.GetPokemon().Level;
        data.SetPokemon(player, Manager.Game.GetPokemon().PokemonData);
        player.CurHp = Manager.Game.GetPokemon().CurHp;
        player.CurExp = Manager.Game.GetPokemon().CurExp;
        player.GetSkills();
        player.Enemy = enemy;
        playerUI.SetBattleUI(player);           // 배틀 UI설정(체력바, 이름, 레벨)
        playerUI.InitHpSlider(player.CurHp / (float)player.Hp);
        int temp = player.Level * player.Level * player.Level;
        playerUI.InitExpSlider((player.CurExp - temp) / ((float)player.NextExp - temp));
        playerAnim.PlayAnimaion();
        yield return battleLog.DisplayLog($"Go! {player.Name}!");
        selectLog.gameObject.SetActive(true);   // 끝나면 셀렉트 로고 띄우기
    }
    public void DisplayLog(string text) // 로그 천천히 띄우기
    {
        StartCoroutine(battleLog.DisplayLog(text));
    }
    // States.Start 끝

    // States.Selecting 시작
    public void StartBattle() // 배틀 시작 설정(selectLog의 fight버튼 누르면 실행)
    {
        skillSlot.gameObject.SetActive(true);  // 스킬 선택버튼은 띄우고
        selectLog.gameObject.SetActive(false); // 행동 선택은 비활성화
        player.SetBattle();
        states = BattleStates.Selecting;
        StartBattleSelecting();
    }
    private void StartBattleSelecting()
    {   
        battleRoutine = StartCoroutine(BattleSelectingRoutine());
    }
    IEnumerator BattleSelectingRoutine()
    {  
        if (skillSlot.gameObject.active == false)
        {   // 버튼 다시 켜지도록
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

        skillSlot.gameObject.SetActive(false);
        battleLog.gameObject.SetActive(true);
        if (actionRank.Peek().Equals(player))
        {
            states = BattleStates.PlayerTurn;
            yield return PlayerTurn();
        }
        else
        {
            states = BattleStates.EnemyTurn;
            yield return EnemyTurn();
        }
    }
    // 플레이어 선택 루틴기다리기
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
    // States.Selecting 끝

    // States.PlayerTurn 시작 
    IEnumerator PlayerTurn()
    {
        int playerPrevHp = player.CurHp;
        int enemyPrevHp = enemy.CurHp;

        Pokemon nextAction = actionRank.Dequeue();
        int damage = nextAction.Execute();
        bool isDead = nextAction.Enemy.TakeDamage(damage);

        yield return new WaitForSeconds(2.5f);
        if (nextAction.Effective == BattleEffect.HighEffective)
        {
            yield return battleLog.DisplayLog($"It's super effective!");
        }
        else if (nextAction.Effective == BattleEffect.LowEffective)
        {
            yield return battleLog.DisplayLog($"It's not very effective...");
        }
        yield return enemyUI.HpRoutine(enemyPrevHp, enemy.CurHp);
        yield return playerUI.HpRoutine(playerPrevHp, player.CurHp);

        if (isDead) // 상대방이 죽은 상황
        {
            states = BattleStates.Win;
            yield return Win();
            isDead = false;
        }
        else if(actionRank.Count != 0)
        {
            states = BattleStates.EnemyTurn;
            yield return EnemyTurn();
        }
        else
        {
            states = BattleStates.Selecting;
            StartBattleSelecting();
        }
    }
    // States.PlayerTurn 끝

    // States.EnemyTurn 시작
    IEnumerator EnemyTurn()
    {
        // 체력이 줄어드는 모션을 위해 이전에서 목적지까지 갈 수 있도록
        int playerPrevHp = player.CurHp;
        int enemyPrevHp = enemy.CurHp;

        Pokemon nextAction = actionRank.Dequeue();
        int damage = nextAction.Execute();
        bool isDead = nextAction.Enemy.TakeDamage(damage);

        yield return new WaitForSeconds(2.5f);
        if (nextAction.Effective == BattleEffect.HighEffective)
        {
            yield return battleLog.DisplayLog($"It's super effective!");
        }
        else if (nextAction.Effective == BattleEffect.LowEffective)
        {
            yield return battleLog.DisplayLog($"It's not very effective...");
        }
        yield return enemyUI.HpRoutine(enemyPrevHp, enemy.CurHp);
        yield return playerUI.HpRoutine(playerPrevHp, player.CurHp);


        if (isDead) // 플레이어의 포켓몬이 죽은 상황
        {
            states = BattleStates.Lose;
            Lose();
            isDead = false;
        }
        else if (actionRank.Count != 0)
        {
            states = BattleStates.PlayerTurn;
            yield return PlayerTurn();
        }
        else
        {
            states = BattleStates.Selecting;
            StartBattleSelecting();
        }
    }
    // States.EnemyTurn 끝

    IEnumerator Win()
    {
        int getExp = (int)(200 * enemy.Level / 7);

        yield return battleLog.DisplayLog($"{enemy.Name} is Fainted");
        enemy.Die();
        yield return new WaitForSeconds(0.2f);
        enemy.gameObject.SetActive(false);
        yield return battleLog.DisplayLog($"{player.Name} gained {getExp} EXP. Points!");

        int temp = (player.Level * player.Level * player.Level);

        yield return playerUI.ExpRoutine((player.CurExp - temp) / ((float)player.NextExp - temp), (player.CurExp + getExp - temp) / ((float)player.NextExp - temp));
        bool isLevelUp = player.GetExp(getExp);
        if(isLevelUp)
        {
            bool isGetSkill = player.LevelUp();
            playerUI.InitExpSlider(0f);
            yield return battleLog.DisplayLog($"{player.Name} grew to LV. {player.Level}!");

            if(isGetSkill)
            {
                yield return battleLog.DisplayLog($"{player.Name} learend {player.CurrentSkill[player.CurrentSkill.Count - 1].Skilldata.name}!");
            }

        }

        EndBattle();
        Manager.Scene.LoadScene("FieldScene");
    }

    private void Lose()
    {
        // 패배 효과...
        DisplayLog($"My {player.Name} is Fainted");
        player.Die();
        // 다음 포켓몬으로 교체??
    }

    public void EndBattle()
    {
        // 한 쪽이 죽었을 때 실행할 메소드
        battleLog.gameObject.SetActive(false);  
        selectLog.gameObject.SetActive(false);
        skillSlot.gameObject.SetActive(false);
        playerUI.gameObject.SetActive(false);
        enemyUI.gameObject.SetActive(false);
        player.gameObject.SetActive(false);
        enemy.gameObject.SetActive(false);
        playerAnim.gameObject.SetActive(false);

        // + 현재 포켓몬의 정보를 게임 매니저가 가지고 가야함
        Manager.Game.UpdatePokemonData(player);
    }

}
