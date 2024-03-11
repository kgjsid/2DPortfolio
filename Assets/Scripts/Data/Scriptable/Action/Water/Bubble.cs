using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Bubble", menuName = "Data/Bubble")]
public class Bubble : SkillData
{
    public override void Execute(Pokemon user, Pokemon enemy)
    {
        int damage = SpecialDamage(user, enemy);
        user.Enemy.TakeDamage(damage);
    }
}
