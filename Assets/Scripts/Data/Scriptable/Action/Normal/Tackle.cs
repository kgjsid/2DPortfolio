using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Tackle", menuName = "Data/Tackle")]
public class Tackle : SkillData
{
    public override void Execute(Pokemon user, Pokemon enemy)
    {
        int damage = AttackDamage(user, enemy);
        user.Enemy.TakeDamage(damage);
    }
}
