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
        Debug.Log($"{damage}�� ������ �޾Ҵ�!");
        hp -= damage;
        Debug.Log($"{hp}�� ü�� ����");
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
        // ��ų ������ ����? -> ��ų�� ������ ���� ��� ������ �ֱ�
        
    }

    enum PokemonTag
    {
        player,
        enemy
    }
}
