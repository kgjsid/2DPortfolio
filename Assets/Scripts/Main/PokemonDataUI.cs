using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PokemonDataUI : PopUpUI
{
    [SerializeField] PokemonButton[] buttons = new PokemonButton[6];
    [SerializeField] PokemonDetail detail;
    [SerializeField] SetPokemonData set;

    private void Start()
    {
        for(int i = 0; i < buttons.Length; i++)
        {
            buttons[i].gameObject.SetActive(false);
        }
        for(int i = 0; i < Manager.Game.curCount; i++)
        {
            buttons[i].gameObject.SetActive(true);
            set.SetPokemon(Manager.Game.pokemons[i], Manager.Game.pokemons[i].PokemonData);
            set.SettingData();
            buttons[i].SetData(Manager.Game.pokemons[i]);
        }
    }

    public void ShowDetail(Pokemon pokemon)
    {
        detail.gameObject.SetActive(true);
        detail.SetInfo(pokemon);
    }
}
