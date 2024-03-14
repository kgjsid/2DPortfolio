using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI; 

public class BattleUI : MonoBehaviour
{
    // -> 전투 시작 시 대화창?(ㅇㅇㅇ This appered) -> fight, run -> 스킬 사용창으로 넘어가는 형식
    // 스킬 계수 + 사용 횟수 적용하여 UI 완료하고, 배틀씬 마무리하기 
    [SerializeField] TMP_Text levelText;
    [SerializeField] TMP_Text nameText;
    [SerializeField] Image hpSlider;
    [SerializeField] Image expImage;

    [SerializeField] Pokemon pokemon;

    [SerializeField] Sprite red;
    [SerializeField] Sprite yellow;
    [SerializeField] Sprite green;

    int maxHp;

    private void Start()
    {
        maxHp = pokemon.Hp;
        hpSlider.rectTransform.localScale = Vector3.one;
    }

    public void SetBattleUI(Pokemon pokemon)
    {
        levelText.text = pokemon.Level.ToString();
        nameText.text = pokemon.PokemonData.name;
        maxHp = pokemon.Hp;
    }

    public void InitHpSlider(float value)
    {
        hpSlider.rectTransform.localScale = new Vector3(value, 1f, 1f);
        SethpSlider();
    }

    public void InitExpSlider(float value)
    {
        if (value <= 0f)
            value = 0f;
        else if (value >= 1f)
            value = 1f;
        expImage.rectTransform.localScale = new Vector3(value, 1f, 1f);
    }

    public void SethpSlider()
    {
        if(hpSlider.rectTransform.localScale.x > 0.5f)
        {
            hpSlider.sprite = green;
        }

        if(hpSlider.rectTransform.localScale.x < 0.5f)
        {
            hpSlider.sprite = yellow;
        }

        if(hpSlider.rectTransform.localScale.x < 0.2f)
        {
            hpSlider.sprite = red;
        }
    }

    public IEnumerator HpRoutine(int curHp, int targetHp)
    {
        float rate = 0f;
        if(targetHp < 0)
        {
            targetHp = 0;
        }
        if (curHp == targetHp)
        {
            yield return null;
        }
        else
        {
            while (rate < 1f)
            {
                rate += 0.1f;
                hpSlider.rectTransform.localScale = new Vector3(Mathf.Lerp(curHp, targetHp, rate) / maxHp, 1f, 1f);
                SethpSlider();
                yield return new WaitForSeconds(0.1f);
            }
        }
    }

    public IEnumerator ExpRoutine(float curExp, float targetExp)
    {
        float rate = 0f;

        if (curExp > 1f)
            curExp = 1f;

        if (curExp == targetExp)
        {
            yield return null;
        }
        else
        {
            while (rate < 1f)
            {
                rate += 0.1f;
                expImage.rectTransform.localScale = new Vector3(Mathf.Lerp(curExp, targetExp, rate), 1f, 1f);
                SethpSlider();
                yield return new WaitForSeconds(0.1f);
            }
        }
    }
}
