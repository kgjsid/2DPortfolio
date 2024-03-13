using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Gust", menuName = ("Data/Gust"))]
public class Gust : SkillData
{
    public override float Execute(Pokemon user, Pokemon enemy)
    {
        float damage = AttackDamage(user, enemy);
        return damage;
        // user.Enemy.TakeDamage(damage);
    }
}
