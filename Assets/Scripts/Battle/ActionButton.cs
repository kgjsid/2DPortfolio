using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ActionButton : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    // ��ũ���ͺ� ������Ʈ Ȱ���Ͽ� ��ų �����ϱ�
    [SerializeField] Button button;
    [SerializeField] Skill skill;
    [SerializeField] Pokemon owner;
    [SerializeField] TMP_Text text;

    [SerializeField] SkillInfoUI skillInfoUI;

    private void OnEnable()
    {
        // �����Ͽ��� �� �ؽ�Ʈ�� --, ��ư�� ��Ȱ��ȭ
        button = GetComponent<Button>();
        text.text = "--";
        button.interactable = false;
        text.color = Color.black;
    }

    // ��ư �����Լ�
    // ��ư�� �̺�Ʈ �����صΰ�, �ؽ�Ʈ �̸� �ٲٱ�
    public void SetButton(Skill skill)
    {
        this.skill = skill;
        text.text = skill.Skilldata.name;
        button.interactable = true;
    }

    public void OnClick()
    {   // Ŭ�� �̺�Ʈ
        if (skill != null)
        {   // ���ϸ��� ���� �ൿ���� ��ų �־��ֱ�
            owner.SetAction(skill);
        }
    }

    public void SetOwner(Pokemon owner)
    {   // ��� ��ü ã��
        this.owner = owner;
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        text.color = Color.blue;
        if (skill != null)
            skillInfoUI.ShowSkillInfo(skill);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        text.color = Color.black;
    }
}
