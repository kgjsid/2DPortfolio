using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI; 

public class BattleUI : MonoBehaviour
{
    [SerializeField] TMP_Text levelText;
    [SerializeField] TMP_Text nameText;
    [SerializeField] Slider hpSlider;

    [SerializeField] Pokemon pokemon;
    int maxHp;

    private void Start()
    {
        maxHp = pokemon.Hp;
    }

    public void SetLevel(int level)
    {
        levelText.text = level.ToString();
    }

    public void SetName(string name)
    {
        nameText.text = name;
    }

    public void SethpSlider(int hp)
    {
        hpSlider.value = hp / maxHp;
    }
}
