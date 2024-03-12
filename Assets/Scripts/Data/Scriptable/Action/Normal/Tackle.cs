using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Tackle", menuName = "Data/Tackle")]
public class Tackle : SkillData
{
    public override int Execute(Pokemon user, Pokemon enemy)
    {
        int damage = AttackDamage(user, enemy);
        return damage;
    }
}
