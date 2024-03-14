using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Ember", menuName = ("Data/Ember"))]
public class Ember : SkillData
{
    public override float Execute(Pokemon user, Pokemon enemy)
    {
        float damage = AttackDamage(user, enemy) * EqualAttack(user);
        return damage;
        // user.Enemy.TakeDamage(damage);
    }
}
