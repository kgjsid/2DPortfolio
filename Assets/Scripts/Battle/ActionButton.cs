using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ActionButton : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    // 1. �ڽ��� �����ϰ� �ִ� ��ų�� �̿��ؼ� ��ư�� ������ �÷��̾�� �����϶�� �ϱ�
    // 2. �Է� ������ ǥ���ϱ� ���� ��Ŀ ǥ��? (>)
    [SerializeField] Button button;
    [SerializeField] BaseAction action;
    [SerializeField] Pokemon pokemon;
    [SerializeField] TMP_Text text;

    private void Start()
    {
        button = GetComponent<Button>();
        // Data���� action���� �� �������� ���� ��
        // pokemon�� ���� ������
        text.text = "--";
        button.interactable = false;
    }

    public void SetAction(BaseAction action)
    {
        this.action = action;
        text.text = action.ActionName;
        button.interactable = true;
    }

    public void OnClick()
    {
        if (action != null)
        {
            pokemon.SetAction(action);
            action.SetOwner(pokemon);
        }
    }

    public void SetOwner(Pokemon pokemon)
    {
        this.pokemon = pokemon;
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
