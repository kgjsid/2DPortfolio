using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu(fileName = "Pokemon", menuName = "Data/Pokemon")]
public class PokemonData : ScriptableObject
{
    public string name;         // 포켓몬의 이름
    public Type type;           // 포켓몬 타입(풀, 불...)
    public Sprite sprite;       // 포켓몬 이미지(뒷면)
    public Sprite enemySprite;  // 적 이미지(앞면)

    public Sprite iconImage1;   // 아이콘 이미지
    public Sprite iconImage2;

    // 종족치(Individual Value)
    public int H; // 체력
    public int AP; // 어택 포인트
    public int AD; // 어택 방어력
    public int SP; // 특수 포인트
    public int SD; // 특수 방어력
    public int S;  // 스피드

    // 능력치는 종족치와 계산식으로
    // HP -> (H * 2) * (level / 100.0f) + 10 + level 
    // 개체값과 노력치에 대한 부분은 찾을 수 없고, 숨겨진 요소...

    public List<SkillData> possessedAction;    // 소지한 액션들, 스킬들

    // 이제는 포켓몬이 스킬을 가져야 할 때
    public GetSkillData[] skillData;

    public PokemonData nextPokemon;

    [Serializable]
    public struct GetSkillData
    {
        // 획득 레벨과 스킬?
        // 레벨이 되면 스킬을 얻는다??
        public int level;
        public SkillData skill;
    }

}