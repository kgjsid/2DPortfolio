using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

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

    Queue<Pokemon> actionRank;          // 큐에 행동을 담아두고, 스피드 순서로 넣으면 꺼낼 땐 먼저 나오니 이용
    [SerializeField] SetPokemonData data;

    int playerCurHp;
    int enemyCurHp;
    bool isLog;

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
    private void Start()
    { 
        actionRank = new Queue<Pokemon>();
        battleLog.gameObject.SetActive(false);
        selectLog.gameObject.SetActive(false);
        skillSlot.gameObject.SetActive(false);
        SettingBattle();
    }

    public void SettingBattle()
    {   // 배틀 세팅
        SetDatas();
        StartCoroutine(SetUIs());
        StartBattle();
    }

    private void StartBattleRoutine()
    {   // 배틀 루틴 시작
        battleRoutine = StartCoroutine(BattleRoutine());
    }

    public void EndBattle()
    {
        // 한 쪽이 죽었을 때 실행할 메소드
        StopCoroutine(battleRoutine);

        if(enemy.CurHp == 0)
        {
            DisplayLog($"{enemy.name} is Fainted");
        }

        Manager.Scene.LoadScene("FieldScene");
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
        StartBattleRoutine();
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
        isLog = true;
        playerUI.SetBattleUI(player);
        enemyUI.SetBattleUI(enemy);

        battleLog.gameObject.SetActive(true);
        yield return battleLog.DisplayLog($"a wile {enemy.Name} appeared!");
        isLog = false;
        selectLog.gameObject.SetActive(true);
    }

    public void SetDatas() // 포켓몬 설정
    {
        player.Level = Manager.Game.GetPokemon().Level;
        data.SetPokemon(player, Manager.Game.GetPokemon().PokemonData);
        data.SetPokemon(enemy, Manager.Game.GetPokemon().PokemonData); 
        player.Enemy = enemy; 
        enemy.Enemy = player;
    }

    public void StartBattle() // 배틀 시작 설정
    {
        skillSlot.gameObject.SetActive(true);  // 스킬 선택버튼은 띄우고
        selectLog.gameObject.SetActive(false); // 행동 선택은 비활성화
        player.SetBattle();
        StartBattleRoutine();
    }

    public void DisplayLog(string text)
    {
        StartCoroutine(battleLog.DisplayLog(text));
    }


    /*
    public enum State { SetBattle, PlayerChoice, EnemyChoice, BattleState, EndBattle, size}
    private class StateMachine
    {
        private State curState;
        private BaseState[] states;

        public void Init(BattleManager owner)
        {
            states = new BaseState[(int)State.size];

            states[(int)State.SetBattle] = new SetBattle(this, owner);
            states[(int)State.PlayerChoice] = new PlayerChoice(this, owner);
            states[(int)State.EnemyChoice] = new EnemyChoice(this, owner);
            states[(int)State.BattleState] = new BattleState(this, owner);
            states[(int)State.EndBattle] = new EndBattle(this, owner);

            curState = State.SetBattle;
            states[(int)curState].Enter();
        }

        public void Update()
        {
            states[(int)curState].Update();
            states[(int)curState].Transition();
        }

        public void ChangeState(State nextState)
        {
            states[(int)curState].Exit();
            curState = nextState;
            states[(int)curState].Enter();
        }
    }
    private class BaseState
    {
        protected StateMachine fsm;
        protected BattleManager owner;

        public BaseState(StateMachine fsm, BattleManager owner)
        {
            this.fsm = fsm;
            this.owner = owner;
        }

        public virtual void Enter() { }
        public virtual void Update() { }
        public virtual void Transition() { }
        public virtual void Exit() { }
    }

    private class SetBattle : BaseState
    {
        public SetBattle(StateMachine fsm, BattleManager owner) : base(fsm, owner) { }

        // 배틀 시작 시 설정
        // 포켓몬과 UI, 그리고 데이터 설정
        public override void Enter()
        {
            // -> 야생의 ㅇㅇㅇ이 나타났다
            // -> 플레이어 애니메이션 재생도 넣어주면 좋을 듯
            owner.SettingBattle();
        }

        public override void Transition()
        {
            // 야생의 ㅇㅇㅇ이 나타났다가 끝나면
            if(owner.isLog == false)
            {   // 선택 단계로 갈 때 속도가 빠른 것 부터 먼저 선택
                if (owner.player.Speed >= owner.enemy.Speed)
                {
                    fsm.ChangeState(State.PlayerChoice);
                }
                else
                {
                    fsm.ChangeState(State.EnemyChoice);
                }
            }
        }

        public override void Exit()
        {   // 배틀 시작 설정
            owner.StartBattle();
        }
    }

    private class PlayerChoice : BaseState
    {
        public PlayerChoice(StateMachine fsm, BattleManager owner) : base(fsm, owner) { }

        public override void Update()
        {
            if(owner.player.GetAction() != null)
            {
                owner.actionRank.Enqueue(owner.player);
                this.Transition();
            }
        }

        public override void Transition()
        {
            if(owner.actionRank.Count == 1 && owner.player.GetAction() != null)
            {
                fsm.ChangeState(State.EnemyChoice);
            }
            else
            {
                fsm.ChangeState(State.BattleState);
            }
        }
    }

    private class EnemyChoice : BaseState
    {
        public EnemyChoice(StateMachine fsm, BattleManager owner) : base(fsm, owner) { }

        public override void Enter()
        {
            owner.actionRank.Enqueue(owner.enemy);
        }

        public override void Transition()
        {
            if(owner.actionRank.Count == 1)
            {
                fsm.ChangeState(State.PlayerChoice);
            }
            else
            {
                fsm.ChangeState(State.BattleState);
            }
        }

    }

    private class BattleState : BaseState
    {
        public BattleState(StateMachine fsm, BattleManager owner) : base(fsm, owner) { }

        public override void Enter()
        {
            owner.skillSlot.gameObject.SetActive(false); // 먼저 스킬은 선택했으니 비활성화
            owner.battleLog.gameObject.SetActive(true);
        }

        public override void Update()
        {
            while (owner.actionRank.Count > 0)     // 큐에 담아둔 모든 행동 실행
            {
                Pokemon nextAction = owner.actionRank.Dequeue();
                nextAction.Execute();

                yield return new WaitForSeconds(2f);
                yield return owner.enemyUI.HpRoutine(owner.enemyCurHp, owner.enemy.Hp);
                yield return owner.playerUI.HpRoutine(owner.playerCurHp, owner.player.Hp);
            }
        }
    }

    private class EndBattle : BaseState
    {
        public EndBattle(StateMachine fsm, BattleManager owner) : base(fsm, owner) { }

    }
    */
}
