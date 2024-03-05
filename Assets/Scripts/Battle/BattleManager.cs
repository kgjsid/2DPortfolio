using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleManager : MonoBehaviour
{
    [SerializeField] Pokemon player;
    [SerializeField] Pokemon enemy;

    Queue<Pokemon> attackRank;

    private void OnEnable()
    {
        Battle();
    }

    private void OnDisable()
    {
        
    }

    private void Awake()
    {
        attackRank = new Queue<Pokemon>();
    }

    private void Battle()
    {
        // 1. �� ���� �ױ���� or �������Ǳ��� ��Ʋ ����
        // �÷��̾��� ��ų�� ���� �ƴ϶�� �� �����ϱ�?
        while(player.Hp >= 0 || enemy.Hp >= 0)
        {
            // ���� ���ǵ尡 ���� ������ ����
            
            if(player.Speed > enemy.Speed)
            {
                Debug.Log("�÷��̾ �� �����ϴ�. ���ݼ��� �÷��̾� -> ��");
                attackRank.Enqueue(player);
                attackRank.Enqueue(enemy);
            }
            else
            {
                attackRank.Enqueue(enemy);
                attackRank.Enqueue(player);
            }

            Debug.Log("ť�� �־�� �����ͷ� ���ݽ���");
            while (attackRank.Count > 0)
            {
                Pokemon attacker = attackRank.Dequeue();
                attacker.AttackEnemy();
            }
            Debug.Log("�� �� ��");
            


        }
        
    }
}
