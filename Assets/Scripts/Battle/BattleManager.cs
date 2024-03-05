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
        // 1. 한 쪽이 죽기까지 or 종료조건까지 배틀 지속
        // 플레이어의 스킬이 널이 아니라면 턴 진행하기?
        while(player.Hp >= 0 || enemy.Hp >= 0)
        {
            // 먼저 스피드가 빠른 순으로 공격
            
            if(player.Speed > enemy.Speed)
            {
                Debug.Log("플레이어가 더 빠릅니다. 공격순서 플레이어 -> 적");
                attackRank.Enqueue(player);
                attackRank.Enqueue(enemy);
            }
            else
            {
                attackRank.Enqueue(enemy);
                attackRank.Enqueue(player);
            }

            Debug.Log("큐에 넣어둔 데이터로 공격시작");
            while (attackRank.Count > 0)
            {
                Pokemon attacker = attackRank.Dequeue();
                attacker.AttackEnemy();
            }
            Debug.Log("한 턴 끝");
            


        }
        
    }
}
