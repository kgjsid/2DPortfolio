using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PokemonEventNPC : NPC
{
    // ��ȣ�ۿ� �� ���ϸ��� �ִ� NPC
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
