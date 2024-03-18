using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Skill
{
    // ���� ��ü�� ��ų ���� pp ���������� ����ϱ�
    [SerializeField] SkillData data;

    [SerializeField] int curPP;

    public int CurPP { get => curPP; set { curPP = value; } }
    public SkillData Skilldata { get => data; set { data = value; } }
    public void InitSkill(SkillData skillData)
    {   // �ʱ�ȭ
        this.data = skillData;
        curPP = skillData.maxPP;
    }

    public int ReturnCurPP()
    {   // ���� PP ����
        return curPP;
    }
    public void UseSkill()
    {   // ��ų ��� �� Ƚ�� �ϳ� ���̱�
        curPP -= 1;
    }
}
