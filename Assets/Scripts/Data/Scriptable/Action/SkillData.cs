using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class SkillData : ScriptableObject
{
    public Pokemon user;
    public Pokemon enemy;

    public string name;     // 기술의 이름
    public string type;     // 기술의 타입(물리, 특수)

    public int power;       // 기술의 위력
    public int hitRate;     // 적중률
    public int curPP;       // 현재 파워 포인트(사용 횟수?)
    public int maxPP;       // 최대 파워 포인트

    public abstract int Execute(Pokemon user, Pokemon enemy); // 스킬들이 반드시 가져야할 실행함수

    public int AttackDamage(Pokemon user, Pokemon enemy)
    {
        // 데미지 공식 : (((레벨 * 0.4) + 2) * 기술의 위력(%) * 유저의 공격(특수) * 0.5 * 0.01) / 상대방의 방어(특수) * (치명타시 2배) * 랜덤수(217-255) / 255
        // 타입 상성과 동일한 속성에서 공격시 1.5를 곱함
        return (int)(((((user.Level * 0.4f) + 2) * power * user.Damage * 0.02f) / enemy.Defence) * (Random.Range(217, 256) / 255f));
    }

    public int SpecialDamage(Pokemon user, Pokemon enemy)
    {
        return (int)(((((user.Level * 0.4f) + 2) * power * user.SpecialDamage * 0.02f) / enemy.SpecialDefence) * (Random.Range(217, 256) / 255f));
    }
}
