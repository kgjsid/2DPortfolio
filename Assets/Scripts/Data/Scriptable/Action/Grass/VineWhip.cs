using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "VineWhip", menuName = ("Data/VineWhip"))]
public class VineWhip : SkillData
{
    public override int Execute(Pokemon user, Pokemon enemy)
    {
        int damage = AttackDamage(user, enemy);
        return damage;
        user.Enemy.TakeDamage(damage);
    }
}
