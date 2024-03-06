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
        // ��Ʋ�� ��ó��?
    }

    IEnumerator BattleRoutine()
    {
        isRoutine = true;
        yield return SelectRoutine();   // �÷��̾� ����

        Debug.Log("�ൿ ����");

        while(actionRank.Count > 0)     // ť�� ��Ƶ� ��� �ൿ ����
        {
            Debug.Log("�ൿ ����");
            BaseAction nextAction = actionRank.Dequeue();
            nextAction.Execute();
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
}
