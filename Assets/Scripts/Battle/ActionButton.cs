using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ActionButton : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    // 스크립터블 오브젝트 활용하여 스킬 장착하기
    [SerializeField] Button button;
    [SerializeField] SkillData skill;
    [SerializeField] Pokemon owner;
    [SerializeField] TMP_Text text;

    private void OnEnable()
    {
        // 시작하였을 때 텍스트는 --, 버튼은 비활성화
        button = GetComponent<Button>();
        text.text = "--";
        button.interactable = false;
        text.color = Color.black;
    }

    // 버튼 세팅함수
    // 버튼에 이벤트 연결해두고, 텍스트 이름 바꾸기
    public void SetButton(SkillData skill)
    {
        this.skill = skill;
        text.text = skill.name;
        button.interactable = true;
    }

    public void OnClick()
    {   // 클릭 이벤트
        if (skill != null)
        {   // 포켓몬의 현재 행동으로 스킬 넣어주기
            owner.SetAction(skill);
        }
    }

    public void SetOwner(Pokemon owner)
    {   // 사용 주체 찾기
        this.owner = owner;
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        text.color = Color.blue;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        text.color = Color.black;
    }
}
