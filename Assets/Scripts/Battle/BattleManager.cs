using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleManager : Singleton<BattleManager>
{
    [SerializeField] Pokemon player;    // ��Ʋ�� �ʿ��� ���ϸ� ��
    [SerializeField] Pokemon enemy;

    [SerializeField] BattleUI playerUI;
    [SerializeField] BattleUI enemyUI;
    [SerializeField] BattleSceneFlow battle;

    Queue<Pokemon> actionRank;       // ť�� �ൿ�� ��Ƶΰ�, ���ǵ� ������ ������ ���� �� ���� ������ �̿�
    List<SkillData> enemyActions;
    [SerializeField] SetPokemonData data;

    int playerCurHp;
    int enemyCurHp;

    public void SetBattle()
    {
        battle.BattleUI();
        enemyActions = enemy.PossessedAction; // ������ �� �� �ִ� �ൿ �����ϱ�
        SetUIs();
        SetDatas();
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

    public void SetUIs() // UI ����
    {
        playerUI.SetName(player.Name);
        playerUI.SetLevel(player.Level);
        enemyUI.SetName(enemy.Name);
        enemyUI.SetLevel(enemy.Level);
    }

    public void SetDatas() // ���ϸ� ����
    {
        data.SetPokemon(player);
        data.SettingData();
        data.SetPokemon(enemy);
        data.SettingData();
        player.Enemy = enemy;
        enemy.Enemy = player;
    }

    // ��Ʋ ��ƾ
    IEnumerator BattleRoutine()
    {   // 1. �ൿ ����
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
        while(actionRank.Count > 0)     // ť�� ��Ƶ� ��� �ൿ ����
        {
            playerCurHp = player.Hp;
            enemyCurHp = enemy.Hp;
            Pokemon nextAction = actionRank.Dequeue();
            nextAction.Execute();

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
}
