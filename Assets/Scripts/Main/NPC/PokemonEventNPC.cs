using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PokemonEventNPC : NPC
{
    // 상호작용 시 포켓몬을 주는 NPC
    [SerializeField] PokemonData[] pokemonDatas;
    [SerializeField] int level;

    public UnityEvent popUp;
    public override void Interact(PlayerInteractor player)
    {
        TalkManager.Talk.ShowUI(id);
        popUp?.Invoke();
        Manager.Game.SetPokemon(pokemonDatas[Random.Range(0, pokemonDatas.Length)], level);
    }
}
