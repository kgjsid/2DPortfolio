using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ActionButton : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    // 1. 자신이 장착하고 있는 스킬을 이용해서 버튼이 눌리면 플레이어에게 장착하라고 하기
    // 2. 입력 들어오면 표시하기 위한 마커 표시? (>)
    [SerializeField] BaseAction action;
    [SerializeField] Pokemon pokemon;
    [SerializeField] TMP_Text text;

    private void Start()
    {
        // Data에서 action정보 다 가져오면 좋을 듯
        // pokemon에 대한 참조도
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
