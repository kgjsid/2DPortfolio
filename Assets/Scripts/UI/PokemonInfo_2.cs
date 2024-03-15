using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PokemonInfo_2 : MonoBehaviour
{
    [Header("InfoImage")]
    [SerializeField] Image pokemonImage;
    [SerializeField] TMP_Text levelText;
    [SerializeField] TMP_Text nameText;

    [Header("InfoText")]
    [SerializeField] TMP_Text CurHpText;
    [SerializeField] TMP_Text MaxHpText;
    [SerializeField] TMP_Text AttackText;
    [SerializeField] TMP_Text DefenceText;
    [SerializeField] TMP_Text SpAttackText;
    [SerializeField] TMP_Text SpDefenceText;
    [SerializeField] TMP_Text SpeedText;
    [SerializeField] TMP_Text TotalExp;
    [SerializeField] TMP_Text NextExp;

    public void SetInfo(Pokemon pokemon)
    {
        pokemonImage.sprite = pokemon.PokemonData.enemySprite;
        levelText.text = pokemon.Level.ToString();
        nameText.text = pokemon.Name;

        CurHpText.text = pokemon.CurHp.ToString();
        MaxHpText.text = pokemon.Hp.ToString();
        AttackText.text = pokemon.Damage.ToString();
        DefenceText.text = pokemon.Defence.ToString();
        SpDefenceText.text = pokemon.SpecialDefence.ToString();
        SpeedText.text = pokemon.Speed.ToString();
        TotalExp.text = pokemon.CurExp.ToString();
        int temp = pokemon.NextExp - pokemon.CurExp;
        NextExp.text = temp.ToString();
    }
}
