using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPC : MonoBehaviour, IInteractable
{
    [SerializeField] PokemonData[] pokemonDatas;
    [SerializeField] PopUpUI npcDialog;
    public void Interact(PlayerInteractor player)
    {
        Manager.UI.ShowPopUpUI(npcDialog);

        int level = Random.Range(5, 15);
        level = 7;

        Manager.Game.SetPokemon(pokemonDatas[Random.Range(0, pokemonDatas.Length)] , level);

    }
}
