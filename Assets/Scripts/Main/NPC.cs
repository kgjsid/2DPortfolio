using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPC : MonoBehaviour, IInteractable
{
    [SerializeField] PokemonData[] pokemonDatas;
    public void Interact(PlayerInteractor player)
    {
        int level = Random.Range(1, 10);

        Manager.Game.SetPokemon(pokemonDatas[Random.Range(0, pokemonDatas.Length)] , level);

        Debug.Log("Æ÷ÄÏ¸ó È¹µæ!!");
    }
}
