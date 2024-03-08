using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleManager : MonoBehaviour
{
    private static BattleManager instance;
    public static BattleManager Battle {get => instance;}

    // �����ϱ�?
    [Header("Pokemon")]
    [SerializeField] Pokemon player;    // ��Ʋ�� �ʿ��� ���ϸ� ��
    [SerializeField] Pokemon enemy;

    [Header("UI")]
    [SerializeField] BattleUI playerUI;
    [SerializeField] BattleUI enemyUI;
    [SerializeField] DiaLogUI battleLog;       // ��Ʋ �α�(���� ����.. ���� ����..)
    [SerializeField] PopUpUI selectLog;        // �ൿ ����â
    [SerializeField] PopUpUI skillSlot;        // ��ų ����â

    Queue<Pokemon> actionRank;       // ť�� �ൿ�� ��Ƶΰ�, ���ǵ� ������ ������ ���� �� ���� ������ �̿�
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
    {   // ��Ʋ ����
        SetDatas();
        StartCoroutine(SetUIs());
    }

    private void StartBattleRoutine()
    {   // ��Ʋ ��ƾ ����
        StartCoroutine(BattleRoutine());
    }

    private void EndBattle()
    {
        // ��Ʋ�� ��ó��? �ϴ� ��Ʋ�� �ٽ� �����ϵ���?
        StartBattleRoutine();
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
        EndBattle();
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
        playerUI.SetName(player.Name);
        playerUI.SetLevel(player.Level);
        enemyUI.SetName(enemy.Name);
        enemyUI.SetLevel(enemy.Level);

        battleLog.gameObject.SetActive(true);
        yield return battleLog.DisplayLog($"a wile {enemy.Name} appeared!");

        selectLog.gameObject.SetActive(true);
    }

    public void SetDatas() // ���ϸ� ����
    {
        data.SetPokemon(player); // ������ ����(� ���ϸ����� �Ѱ��ְ�)
        data.SettingData();      // ���ϸ��� �ɷ�ġ ����(����ġ�� ������ ���� ����)
        data.SetPokemon(enemy); 
        data.SettingData();
        player.Enemy = enemy;    // ���� ���� �Ѱ��ְ�
        enemy.Enemy = player;
        enemyActions = enemy.PossessedAction;   // ���� �������� ���¸� ������ �� �ֵ���
    }

    public void StartBattle() // ��Ʋ ���� ����
    {
        Debug.Log("Manger StartBattle");
        skillSlot.gameObject.SetActive(true);  // ��ų ���ù�ư�� ����
        selectLog.gameObject.SetActive(false); // �ൿ ������ ��Ȱ��ȭ
        player.SetBattle();
        enemy.SetBattle();
        StartBattleRoutine();
    }


    public void DisplayLog(string text)
    {
        StartCoroutine(battleLog.DisplayLog(text));
    }
    
}
