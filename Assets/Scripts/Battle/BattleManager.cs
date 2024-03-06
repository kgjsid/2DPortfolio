using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleManager : MonoBehaviour
{
    [SerializeField] Pokemon player;    // ��Ʋ�� �ʿ��� ���ϸ� ��
    [SerializeField] Pokemon enemy;

    [SerializeField] BattleUI playerUI;
    [SerializeField] BattleUI enemyUI;

    Queue<BaseAction> actionRank;       // ť�� �ൿ�� ��Ƶΰ�, ���ǵ� ������ ������ ���� �� ���� ������ �̿�

    [SerializeField] bool isRoutine;
    List<BaseAction> enemyActions;

    int playerCurHp;
    int enemyCurHp;

    private void OnEnable()
    {
        isRoutine = false;
        enemyActions = enemy.PossessedAction; // ������ �� �� �ִ� �ൿ �����ϱ�
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
        // ��Ʋ�� ��ó��?
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
            yield return SelectRoutine();   // �÷��̾� ����
            EnemyRoutine();
        }
        else
        {
            EnemyRoutine();
            yield return SelectRoutine();   // �÷��̾� ����
        }
        Debug.Log("�ൿ ����");

        while(actionRank.Count > 0)     // ť�� ��Ƶ� ��� �ൿ ����
        {
            Debug.Log("�ൿ ����");
            playerCurHp = player.Hp;
            enemyCurHp = enemy.Hp;
            BaseAction nextAction = actionRank.Dequeue();
            nextAction.Execute();

            Debug.Log("�� ü�� ��ƾ����");
            yield return enemyUI.HpRoutine(enemyCurHp, enemy.Hp);
            Debug.Log("�� ü�� ��ƾ��");
            yield return playerUI.HpRoutine(playerCurHp, player.Hp);
            Debug.Log("�Ʊ� ü�� ��ƾ ��");

        }

        Debug.Log("�ൿ ��");
        player.SetAction(null);
        isRoutine = false;
    }

    IEnumerator SelectRoutine()
    {
        Debug.Log("�÷��̾��� ������ ��ٸ��ϴ�.");
        yield return new WaitUntil(() => (player.GetAction() != null));
        actionRank.Enqueue(player.GetAction());         // ������ ���� �׼��� ť�� �ְ�
        Debug.Log("���� ��");
    }

    private void EnemyRoutine() // ���� ��ƾ
    {
        // ������ ������ �ൿ �ֱ�
        Debug.Log("������ �ൿ�� ���ϰ� �ֽ��ϴ�...");
        actionRank.Enqueue(enemyActions[Random.Range(0, enemyActions.Count - 1)]);
        Debug.Log("���� �Ϸ�");
    }
}
