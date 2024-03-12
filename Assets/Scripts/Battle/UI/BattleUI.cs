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
    [SerializeField] Slider hpSlider;
    [SerializeField] Image fillImage;

    [SerializeField] Pokemon pokemon;
    int maxHp;

    private void Start()
    {
        maxHp = pokemon.Hp;
        hpSlider.value = 1f;
    }
    /*
    public void SetUIs()
    {
    }
    */
    public void SetBattleUI(Pokemon pokemon)
    {
        levelText.text = pokemon.Level.ToString();
        nameText.text = pokemon.name;
    }

    public void SethpSlider()
    {
        if(hpSlider.value < 0.5f)
        {
            fillImage.color = Color.yellow;
        }

        if(hpSlider.value < 0.2f)
        {
            fillImage.color = Color.red;
        }
    }

    public IEnumerator HpRoutine(int curHp, int targetHp)
    {
        float rate = 0f;

        if (curHp == targetHp)
        {
            yield return null;
        }
        else
        {
            while (rate < 1f)
            {
                rate += 0.1f; 
                hpSlider.value = Mathf.Lerp(curHp, targetHp, rate) / maxHp;
                SethpSlider();
                yield return new WaitForSeconds(0.1f);
            }
        }
    }
}
