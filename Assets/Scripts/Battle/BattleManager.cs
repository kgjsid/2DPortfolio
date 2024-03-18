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
    [SerializeField] Pokemon player;    // ��Ʋ�� �ʿ��� ���ϸ� ��
    [SerializeField] Pokemon enemy;

    [Header("UI")]
    [SerializeField] BattleUI playerUI;
    [SerializeField] BattleUI enemyUI;
    [SerializeField] DiaLogUI battleLog;       // ��Ʋ �α�(���� ����.. ���� ����..)
    [SerializeField] PopUpUI selectLog;        // �ൿ ����â(�ο���, ����ĥ��)
    [SerializeField] PopUpUI skillSlot;        // ��ų ����â(��ư�� ���� �ش� ��ų�� ���)

    [Header("Animation")]
    [SerializeField] PlayerBattleAnimation playerAnim;

    Queue<Pokemon> actionRank;          // ť�� �ൿ�� ��Ƶΰ�, ���ǵ� ������ ������ ���� �� ���� ������ �̿�
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
    // States.Start ����
    public void InBattle()
    { 
        states = BattleStates.Start;
        StartCoroutine(SettingBattle());
    }
    IEnumerator SettingBattle()
    {   // ��Ʋ ����
        playerAnim.gameObject.SetActive(true);
        states = BattleStates.Start;
        actionRank = new Queue<Pokemon>();      // ť �����
        battleLog.gameObject.SetActive(false);  // �α׵��� ��Ȱ��ȭ
        selectLog.gameObject.SetActive(false);
        skillSlot.gameObject.SetActive(false);
        enemyUI.gameObject.SetActive(true);
        enemy.gameObject.SetActive(true);
        SetDatas();                             // ���ϸ� ����
        yield return StartCoroutine(SetUIs());  // UI ����
    }
    private void SetDatas() // ���ϸ� ����
    {
        // ������ ���� �� ��� ������Ʈ ����
        enemy.Level = Random.Range(3, 5);
        data.SetPokemon(enemy, enemyData[Random.Range(0, enemyData.Length)]);
        enemy.CurHp = enemy.Hp;
        enemy.Enemy = player;
    }
    IEnumerator SetUIs() // UI ����
    {
        enemyUI.SetBattleUI(enemy);
        enemyUI.InitHpSlider(enemy.CurHp / (float)enemy.Hp);
        battleLog.gameObject.SetActive(true);   // ��Ʋ �α� �����
        yield return battleLog.DisplayLog($"a wile {enemy.Name} appeared!"); // a wile �� ����
        playerUI.gameObject.SetActive(true);
        player.gameObject.SetActive(true);
        player.Level = Manager.Game.GetPokemon().Level;
        data.SetPokemon(player, Manager.Game.GetPokemon().PokemonData);
        player.CurHp = Manager.Game.GetPokemon().CurHp;
        player.CurExp = Manager.Game.GetPokemon().CurExp;
        player.GetSkills();
        player.Enemy = enemy;
        playerUI.SetBattleUI(player);           // ��Ʋ UI����(ü�¹�, �̸�, ����)
        playerUI.InitHpSlider(player.CurHp / (float)player.Hp);
        int temp = player.Level * player.Level * player.Level;
        playerUI.InitExpSlider((player.CurExp - temp) / ((float)player.NextExp - temp));
        playerAnim.PlayAnimaion();
        yield return battleLog.DisplayLog($"Go! {player.Name}!");
        selectLog.gameObject.SetActive(true);   // ������ ����Ʈ �ΰ� ����
    }
    public void DisplayLog(string text) // �α� õõ�� ����
    {
        StartCoroutine(battleLog.DisplayLog(text));
    }
    // States.Start ��

    // States.Selecting ����
    public void StartBattle() // ��Ʋ ���� ����(selectLog�� fight��ư ������ ����)
    {
        skillSlot.gameObject.SetActive(true);  // ��ų ���ù�ư�� ����
        selectLog.gameObject.SetActive(false); // �ൿ ������ ��Ȱ��ȭ
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
        {   // ��ư �ٽ� ��������
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
    // �÷��̾� ���� ��ƾ��ٸ���
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
    // States.Selecting ��

    // States.PlayerTurn ���� 
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

        if (isDead) // ������ ���� ��Ȳ
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
    // States.PlayerTurn ��

    // States.EnemyTurn ����
    IEnumerator EnemyTurn()
    {
        // ü���� �پ��� ����� ���� �������� ���������� �� �� �ֵ���
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


        if (isDead) // �÷��̾��� ���ϸ��� ���� ��Ȳ
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
    // States.EnemyTurn ��

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
        // �й� ȿ��...
        DisplayLog($"My {player.Name} is Fainted");
        player.Die();
        // ���� ���ϸ����� ��ü??
    }

    public void EndBattle()
    {
        // �� ���� �׾��� �� ������ �޼ҵ�
        battleLog.gameObject.SetActive(false);  
        selectLog.gameObject.SetActive(false);
        skillSlot.gameObject.SetActive(false);
        playerUI.gameObject.SetActive(false);
        enemyUI.gameObject.SetActive(false);
        player.gameObject.SetActive(false);
        enemy.gameObject.SetActive(false);
        playerAnim.gameObject.SetActive(false);

        // + ���� ���ϸ��� ������ ���� �Ŵ����� ������ ������
        Manager.Game.UpdatePokemonData(player);
    }

}
