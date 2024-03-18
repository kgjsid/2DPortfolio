using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PokemonInfo_3 : MonoBehaviour
{
    [Header("InfoImage")]
    [SerializeField] Image pokemonImage;
    [SerializeField] TMP_Text levelText;
    [SerializeField] TMP_Text nameText;
    [SerializeField] Sprite[] sprites;

    [Header("InfoText")]
    [SerializeField] SkillSetUI[] skillSets;
    
    public void SetInfo(Pokemon pokemon)
    {
        pokemonImage.sprite = pokemon.PokemonData.enemySprite;
        levelText.text = pokemon.Level.ToString();
        nameText.text = pokemon.Name;

        for (int i = 0; i < skillSets.Length; i++)
        {
            skillSets[i].SlashText.gameObject.SetActive(false);
            skillSets[i].ppText.gameObject.SetActive(false);
            skillSets[i].SkillName.gameObject.SetActive(false);
            skillSets[i].CurPPText.gameObject.SetActive(false);
            skillSets[i].MaxPPText.gameObject.SetActive(false);
            skillSets[i].SkillName.gameObject.SetActive(false);
            skillSets[i].SkillImage.gameObject.SetActive(false);
        }

        for(int i = 0; i < pokemon.CurrentSkill.Count; i++)
        {
            skillSets[i].SlashText.gameObject.SetActive(true);
            skillSets[i].ppText.gameObject.SetActive(true);
            skillSets[i].SkillName.gameObject.SetActive(true);
            skillSets[i].CurPPText.gameObject.SetActive(true);
            skillSets[i].SkillName.gameObject.SetActive(true);
            skillSets[i].MaxPPText.gameObject.SetActive(true);
            skillSets[i].SkillImage.gameObject.SetActive(true);

            skillSets[i].SkillName.text = pokemon.CurrentSkill[i].Skilldata.name;
            skillSets[i].CurPPText.text = pokemon.CurrentSkill[i].CurPP.ToString();
            skillSets[i].MaxPPText.text = pokemon.CurrentSkill[i].Skilldata.maxPP.ToString();
            skillSets[i].SkillImage.sprite = sprites[(int)pokemon.CurrentSkill[i].Skilldata.type];
        }
    }
}

[Serializable]
public struct SkillSetUI
{
    public TMP_Text SkillName;
    public TMP_Text CurPPText;
    public TMP_Text MaxPPText;
    public Image SkillImage;
    public TMP_Text SlashText;
    public TMP_Text ppText;
}
