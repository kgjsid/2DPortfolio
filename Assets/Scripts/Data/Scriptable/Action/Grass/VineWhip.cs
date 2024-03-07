using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "VineWhip", menuName = ("Data/VineWhip"))]
public class VineWhip : SkillData
{
    public override void Execute(Pokemon user, Pokemon enemy)
    {
        int damage = AttackDamage(user, enemy);
        user.Enemy.TakeDamage(damage);
    }
}
