using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class PokemonButton : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    [SerializeField] Button button;
    [SerializeField] Sprite normalImage;        // �⺻ �̹���
    [SerializeField] Sprite highlightImage;     // ���̶���Ʈ �̹���

    // ���ϸ� ������ ǥ���ϱ�
    [SerializeField] TMP_Text nameText;         // �̸�   
    [SerializeField] TMP_Text nameLevel;        // ����
    [SerializeField] TMP_Text curHpText;        // ���� ü��
    [SerializeField] TMP_Text maxHpText;        // �ִ� ü��
    [SerializeField] Image hpImage;             // hp�̹��� ��
    [SerializeField] Image icon;             // ���ϸ� ������

    Coroutine imageRoutine;

    private void Start()
    {
        button = GetComponent<Button>();
        button.image.sprite = normalImage;
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        button.image.sprite = highlightImage;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        button.image.sprite = normalImage;
    }

    public void SetData(Pokemon pokemon)
    {
        nameText.text = pokemon.Name;
        nameLevel.text = pokemon.Level.ToString();
        curHpText.text = pokemon.Hp.ToString();
        maxHpText.text = pokemon.Hp.ToString();
        imageRoutine = StartCoroutine(ChangeImage(pokemon));
    }

    IEnumerator ChangeImage(Pokemon pokemon)
    {
        while(true)
        {
            icon.sprite = pokemon.PokemonData.iconImage1;
            yield return new WaitForSecondsRealtime(0.1f);
            icon.sprite = pokemon.PokemonData.iconImage2;
            yield return new WaitForSecondsRealtime(0.1f);
        }
    }
}