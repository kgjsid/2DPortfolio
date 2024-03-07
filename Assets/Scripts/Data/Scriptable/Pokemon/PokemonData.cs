using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Pokemon", menuName = "Data/Pokemon")]
public class PokemonData : ScriptableObject
{
    public string name;         // 포켓몬의 이름
    public string type;         // 포켓몬 타입(풀, 불...)
    public Sprite sprite;       // 포켓몬 이미지

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
}
