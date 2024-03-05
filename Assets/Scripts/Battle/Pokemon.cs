using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pokemon : MonoBehaviour
{
    [SerializeField] int level;
    [SerializeField] int hp;
    [SerializeField] int damage;
    [SerializeField] float speed;

    [SerializeField] Pokemon enemy;
    [SerializeField] PokemonTag pokemonTag;

    public int Hp { get => hp; }
    public float Speed { get => speed; }

    private void TakeDamage(int damage)
    {
        Debug.Log($"{damage}의 공격을 받았다!");
        hp -= damage;
        Debug.Log($"{hp}의 체력 남음");
        if (hp <= 0)
        {
            Destroy(this.gameObject);
        }
    }

    public void AttackEnemy()
    {
        enemy.TakeDamage(damage);
    }

    public void SetSkill()
    {
        // 스킬 데이터 장착? -> 스킬은 무조건 실행 기능 가지고 있기
        
    }

    enum PokemonTag
    {
        player,
        enemy
    }
}
