using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class SkillData : ScriptableObject
{
    /*
     TODO. 스킬에 대한 정보 추가 필요
    (공격 계수, 사용 회수, 이름...)
     */
    public Pokemon user;
    public Pokemon enemy;

    public string name;     // 기술의 이름
    public string type;     // 기술의 타입(물리, 특수)

    public int power;       // 기술의 위력
    public int hitRate;     // 적중률
    public int curPP;       // 현재 파워 포인트(사용 횟수?)
    public int maxPP;       // 최대 파워 포인트

    public abstract void Execute(Pokemon user, Pokemon enemy); // 스킬들이 반드시 가져야할 실행함수
}
