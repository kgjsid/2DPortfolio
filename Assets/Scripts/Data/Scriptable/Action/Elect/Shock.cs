using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Shock", menuName = "Data/Shock")]
public class Shock : SkillData
{
    public override float Execute(Pokemon user, Pokemon enemy)
    {
        float damage = AttackDamage(user, enemy) * EqualAttack(user);
        return damage;
    }

}
