using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Tackle", menuName = "Data/Tackle")]
public class Tackle : SkillData
{
    public override float Execute(Pokemon user, Pokemon enemy)
    {
        float damage = AttackDamage(user, enemy) * EqualAttack(user);
        return damage;
    }
}
