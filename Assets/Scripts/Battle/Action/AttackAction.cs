using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackAction : BaseAction
{
    // 어택 실행.
    public override void Execute()
    {
        Debug.Log("어택 액션");
        owner.Enemy.TakeDamage(owner.Damage);
    }
}
