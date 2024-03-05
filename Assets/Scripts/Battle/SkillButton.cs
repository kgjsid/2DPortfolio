using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SkillButton : MonoBehaviour
{
    // 1. 자신이 장착하고 있는 스킬을 이용해서 버튼이 눌리면 플레이어에게 장착하라고 하기
    // 2. 입력 들어오면 표시하기 위한 마커 표시? (>)

    [SerializeField] Button button;
    [SerializeField] Skill skill;
    [SerializeField] Pokemon pokemon;

    private void Start()
    {
        // button.OnPointerEnter
    }

    public void OnClick()
    {
        // pokemon.SetSkill(this.skill);
    }
}
