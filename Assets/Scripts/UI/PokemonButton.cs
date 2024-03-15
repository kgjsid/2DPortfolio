using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class PokemonButton : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    [SerializeField] Button button;
    [SerializeField] Sprite normalImage;        // 기본 이미지
    [SerializeField] Sprite highlightImage;     // 하이라이트 이미지
    [SerializeField] PokemonDataUI dataUI;

    // 포켓몬 데이터 표기하기
    [SerializeField] TMP_Text nameText;         // 이름   
    [SerializeField] TMP_Text nameLevel;        // 레벨
    [SerializeField] TMP_Text curHpText;        // 현재 체력
    [SerializeField] TMP_Text maxHpText;        // 최대 체력
    [SerializeField] Image hpImage;             // hp이미지 바
    [SerializeField] Image icon;             // 포켓몬 아이콘
    [SerializeField] Pokemon pokemon;           // 표시해야 할 포켓몬?

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
        this.pokemon = pokemon;
        nameText.text = pokemon.Name;
        nameLevel.text = pokemon.Level.ToString();
        curHpText.text = pokemon.CurHp.ToString();
        maxHpText.text = pokemon.Hp.ToString();
        imageRoutine = StartCoroutine(ChangeImage(pokemon));
        hpImage.rectTransform.localScale = new Vector3((float)pokemon.CurHp / pokemon.Hp, 1, 1);
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

    public void OnClick()
    {
        dataUI.ShowDetail(this.pokemon);
    }
}
