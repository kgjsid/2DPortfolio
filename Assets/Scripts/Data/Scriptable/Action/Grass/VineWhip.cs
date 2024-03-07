using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "VineWhip", menuName = ("Data/VineWhip"))]
public class VineWhip : SkillData
{
    public override void Execute(Pokemon user, Pokemon enemy)
    {
        user.Enemy.TakeDamage(user.Damage);
    }
}
