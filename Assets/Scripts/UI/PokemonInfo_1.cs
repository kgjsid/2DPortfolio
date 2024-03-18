using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PokemonInfo_1 : MonoBehaviour
{
    [Header("InfoImage")]
    [SerializeField] Image pokemonImage;
    [SerializeField] TMP_Text levelText;
    [SerializeField] TMP_Text nameText;

    [Header("InfoText")]
    [SerializeField] TMP_Text nameText1;
    [SerializeField] TMP_Text typeText;

    public void SetInfo(Pokemon pokemon)
    {
        pokemonImage.sprite = pokemon.PokemonData.enemySprite;
        levelText.text = pokemon.Level.ToString();
        nameText.text = pokemon.Name;
        nameText1.text = pokemon.Name;
    }
}
