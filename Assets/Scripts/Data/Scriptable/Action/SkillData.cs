using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Skill", menuName = "Data/Skill")]
public abstract class SkillData : ScriptableObject
{
    /*
     TODO. 스킬에 대한 정보 추가 필요
    (공격 계수, 사용 회수, 이름...)
     */
    public abstract void Execute();
}
