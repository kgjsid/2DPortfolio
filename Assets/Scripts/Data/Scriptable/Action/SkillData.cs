using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Skill", menuName = "Data/Skill")]
public abstract class SkillData : ScriptableObject
{
    /*
     TODO. ��ų�� ���� ���� �߰� �ʿ�
    (���� ���, ��� ȸ��, �̸�...)
     */
    public abstract void Execute();
}
