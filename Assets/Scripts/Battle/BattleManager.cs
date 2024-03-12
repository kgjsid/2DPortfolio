using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class BattleManager : MonoBehaviour
{
    private static BattleManager instance;
    public static BattleManager Battle {get => instance;}

    [Header("Pokemon")]
    [SerializeField] Pokemon player;    // ��Ʋ�� �ʿ��� ���ϸ� ��
    [SerializeField] Pokemon enemy;

    [Header("UI")]
    [SerializeField] BattleUI playerUI;
    [SerializeField] BattleUI enemyUI;
    [SerializeField] DiaLogUI battleLog;       // ��Ʋ �α�(���� ����.. ���� ����..)
    [SerializeField] PopUpUI selectLog;        // �ൿ ����â(�ο���, ����ĥ��)
    [SerializeField] PopUpUI skillSlot;        // ��ų ����â(��ư�� ���� �ش� ��ų�� ���)

    Queue<Pokemon> actionRank;          // ť�� �ൿ�� ��Ƶΰ�, ���ǵ� ������ ������ ���� �� ���� ������ �̿�
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
    {   // ��Ʋ ����
        SetDatas();
        StartCoroutine(SetUIs());
        StartBattle();
    }

    private void StartBattleRoutine()
    {   // ��Ʋ ��ƾ ����
        battleRoutine = StartCoroutine(BattleRoutine());
    }

    public void EndBattle()
    {
        // �� ���� �׾��� �� ������ �޼ҵ�
        StopCoroutine(battleRoutine);

        if(enemy.CurHp == 0)
        {
            DisplayLog($"{enemy.name} is Fainted");
        }

        Manager.Scene.LoadScene("FieldScene");
    }
    
    // ��Ʋ ��ƾ
    IEnumerator BattleRoutine()
    {   // 1. �ൿ ����

        if(skillSlot.gameObject.active == false)
        {
            skillSlot.gameObject.SetActive(true);
            player.SetBattle();
        }

        if (player.Speed > enemy.Speed)
        {
            yield return SelectRoutine();   // 1. �÷��̾� ����
            EnemyRoutine();                 // 2. �� ����
        }
        else
        {
            EnemyRoutine();                 // 1. �� ����
            yield return SelectRoutine();   // 2. �÷��̾� ����
        }
        // 2. �ൿ ����
        skillSlot.gameObject.SetActive(false); // ���� ��ų�� ���������� ��Ȱ��ȭ
        battleLog.gameObject.SetActive(true);
        while(actionRank.Count > 0)     // ť�� ��Ƶ� ��� �ൿ ����
        {
            playerCurHp = player.Hp;
            enemyCurHp = enemy.Hp;
            Pokemon nextAction = actionRank.Dequeue();
            nextAction.Execute();

            yield return new WaitForSeconds(2f);
            yield return enemyUI.HpRoutine(enemyCurHp, enemy.Hp);
            yield return playerUI.HpRoutine(playerCurHp, player.Hp);
        }
        // 3. �� ó��
        player.SetAction(null);
        StartBattleRoutine();
    }

    IEnumerator SelectRoutine()
    {
        yield return new WaitUntil(() => (player.GetAction() != null));
        actionRank.Enqueue(player);         // ������ ���� �׼��� ť�� �ְ�
    }

    private void EnemyRoutine() // ���� ��ƾ
    {
        // ������ ������ �ൿ �ֱ�
        actionRank.Enqueue(enemy);
    }

    IEnumerator SetUIs() // UI ����
    {
        isLog = true;
        playerUI.SetBattleUI(player);
        enemyUI.SetBattleUI(enemy);

        battleLog.gameObject.SetActive(true);
        yield return battleLog.DisplayLog($"a wile {enemy.Name} appeared!");
        isLog = false;
        selectLog.gameObject.SetActive(true);
    }

    public void SetDatas() // ���ϸ� ����
    {
        player.Level = Manager.Game.GetPokemon().Level;
        data.SetPokemon(player, Manager.Game.GetPokemon().PokemonData);
        data.SetPokemon(enemy, Manager.Game.GetPokemon().PokemonData); 
        player.Enemy = enemy; 
        enemy.Enemy = player;
    }

    public void StartBattle() // ��Ʋ ���� ����
    {
        skillSlot.gameObject.SetActive(true);  // ��ų ���ù�ư�� ����
        selectLog.gameObject.SetActive(false); // �ൿ ������ ��Ȱ��ȭ
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

        // ��Ʋ ���� �� ����
        // ���ϸ�� UI, �׸��� ������ ����
        public override void Enter()
        {
            // -> �߻��� �������� ��Ÿ����
            // -> �÷��̾� �ִϸ��̼� ����� �־��ָ� ���� ��
            owner.SettingBattle();
        }

        public override void Transition()
        {
            // �߻��� �������� ��Ÿ���ٰ� ������
            if(owner.isLog == false)
            {   // ���� �ܰ�� �� �� �ӵ��� ���� �� ���� ���� ����
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
        {   // ��Ʋ ���� ����
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
            owner.skillSlot.gameObject.SetActive(false); // ���� ��ų�� ���������� ��Ȱ��ȭ
            owner.battleLog.gameObject.SetActive(true);
        }

        public override void Update()
        {
            while (owner.actionRank.Count > 0)     // ť�� ��Ƶ� ��� �ൿ ����
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
