using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PokemonEventNPC : NPC
{
    [SerializeField] PokemonData[] pokemonDatas;
    [SerializeField] int level;
    [SerializeField] string text;
    public override void Interact(PlayerInteractor player)
    {
        Manager.UI.ShowPopUpUI(npcDialog);

        Manager.Game.SetPokemon(pokemonDatas[Random.Range(0, pokemonDatas.Length)], level);
    }
}
