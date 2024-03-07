using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Tackle", menuName = "Data/Tackle")]
public class Tackle : SkillData
{
    public override void Execute(Pokemon user, Pokemon enemy)
    {
        // 계산식 조정해야함...
        // 해당 공격이 물리 공격인지, 특수 공격인지에 따라 수정 필요
        // 타입도 필요하네
        

        user.Enemy.TakeDamage(user.Damage);
    }
}
