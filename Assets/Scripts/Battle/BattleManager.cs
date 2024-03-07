using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleManager : Singleton<BattleManager>
{
    [SerializeField] Pokemon player;    // ��Ʋ�� �ʿ��� ���ϸ� ��
    [SerializeField] Pokemon enemy;

    [SerializeField] BattleUI playerUI;
    [SerializeField] BattleUI enemyUI;

    Queue<Pokemon> actionRank;       // ť�� �ൿ�� ��Ƶΰ�, ���ǵ� ������ ������ ���� �� ���� ������ �̿�
    List<SkillData> enemyActions;
    [SerializeField] SetPokemonData data;

    int playerCurHp;
    int enemyCurHp;

    private void OnEnable()
    {
        enemyActions = enemy.PossessedAction; // ������ �� �� �ִ� �ൿ �����ϱ�
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
        // ��Ʋ�� ��ó��? �ϴ� ��Ʋ�� �ٽ� �����ϵ���?
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
            yield return SelectRoutine();   // 1. �÷��̾� ����
            EnemyRoutine();                 // 2. �� ����
        }
        else
        {
            EnemyRoutine();                 // 1. �� ����
            yield return SelectRoutine();   // 2. �÷��̾� ����
        }
        Debug.Log("�ൿ ����");

        while(actionRank.Count > 0)     // ť�� ��Ƶ� ��� �ൿ ����
        {
            Debug.Log("�ൿ ����");
            playerCurHp = player.Hp;
            enemyCurHp = enemy.Hp;
            Pokemon nextAction = actionRank.Dequeue();
            nextAction.Execute();

            Debug.Log("�� ü�� ��ƾ����");
            yield return enemyUI.HpRoutine(enemyCurHp, enemy.Hp);
            Debug.Log("�� ü�� ��ƾ��");
            yield return playerUI.HpRoutine(playerCurHp, player.Hp);
            Debug.Log("�Ʊ� ü�� ��ƾ ��");

        }

        Debug.Log("�ൿ ��");
        player.SetAction(null);
        EndBattle();
    }

    IEnumerator SelectRoutine()
    {
        Debug.Log("�÷��̾��� ������ ��ٸ��ϴ�.");
        yield return new WaitUntil(() => (player.GetAction() != null));
        actionRank.Enqueue(player);         // ������ ���� �׼��� ť�� �ְ�
        Debug.Log("���� ��");
    }

    private void EnemyRoutine() // ���� ��ƾ
    {
        // ������ ������ �ൿ �ֱ�
        Debug.Log("������ �ൿ�� ���ϰ� �ֽ��ϴ�...");
        actionRank.Enqueue(enemy);
        Debug.Log("���� �Ϸ�");
    }
}
