using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealAction : BaseAction
{
    // �� ����
    public override void Execute()
    {
        owner.TakeDamage(-10);
    }
}
