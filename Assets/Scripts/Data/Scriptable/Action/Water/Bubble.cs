using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Bubble", menuName = "Data/Bubble")]
public class Bubble : SkillData
{
    public override float Execute(Pokemon user, Pokemon enemy)
    {
        float damage = SpecialDamage(user, enemy);
        return damage;
        // user.Enemy.TakeDamage(damage);
    }
}
