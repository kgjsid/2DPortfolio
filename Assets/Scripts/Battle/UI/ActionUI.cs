using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ActionUI : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    Button button;
    [SerializeField] TMP_Text text;
    private void Awake()
    {
        button = GetComponent<Button>();
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        text.color = Color.blue;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        text.color = Color.black;
    }

    public void OnFight()
    {
        Manager.Battle.SetBattle();
    }
}
