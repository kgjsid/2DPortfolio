using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackAction : BaseAction
{
    // ���� ����.
    public override void Execute()
    {
        Debug.Log("���� �׼�");
        owner.Enemy.TakeDamage(owner.Damage);
    }
}
