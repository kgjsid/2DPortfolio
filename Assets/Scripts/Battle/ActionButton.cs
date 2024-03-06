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
    [SerializeField] BaseAction action;
    [SerializeField] Pokemon pokemon;
    [SerializeField] TMP_Text text;

    private void Start()
    {
        // Data���� action���� �� �������� ���� ��
        // pokemon�� ���� ������
    }

    public void OnClick()
    {
        pokemon.SetAction(this.action);
        action.SetOwner(pokemon);
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
