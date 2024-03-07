using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Tackle", menuName = "Data/Tackle")]
public class Tackle : SkillData
{
    public override void Execute(Pokemon user, Pokemon enemy)
    {
        // ���� �����ؾ���...
        // �ش� ������ ���� ��������, Ư�� ���������� ���� ���� �ʿ�
        // Ÿ�Ե� �ʿ��ϳ�
        

        user.Enemy.TakeDamage(user.Damage);
    }
}
