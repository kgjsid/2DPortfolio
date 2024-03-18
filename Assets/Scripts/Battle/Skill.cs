using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Skill
{
    // 실제 객체용 스킬 현재 pp 관리용으로 사용하기
    [SerializeField] SkillData data;

    [SerializeField] int curPP;

    public int CurPP { get => curPP; set { curPP = value; } }
    public SkillData Skilldata { get => data; set { data = value; } }
    public void InitSkill(SkillData skillData)
    {   // 초기화
        this.data = skillData;
        curPP = skillData.maxPP;
    }

    public int ReturnCurPP()
    {   // 현재 PP 리턴
        return curPP;
    }
    public void UseSkill()
    {   // 스킬 사용 시 횟수 하나 줄이기
        curPP -= 1;
    }
}
