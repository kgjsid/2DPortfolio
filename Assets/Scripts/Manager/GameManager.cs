using System.Collections.Generic;
using UnityEngine;

public class GameManager : Singleton<GameManager>
{
    // 플레이어의 포켓몬 관리하기
    public int maxCount = 6;           // 최대 소지 수
    public int curCount = 0;    // 현재 수

    public List<Pokemon> pokemons = new List<Pokemon>();              // 포켓몬 오브젝트

    public SetPokemonData setPokemonData;

    private void Start()
    {
        for(int i = 0; i < pokemons.Count; i++)
        {
            pokemons[i].gameObject.SetActive(false);
        }
        curCount = 0;
    }

    public void SetPokemon(PokemonData newPokemon, int level)
    {
        if (curCount == maxCount)
        {
            Debug.Log("최대 소지중!");
            return;
        }
        pokemons[curCount].Level = level;
        pokemons[curCount].PokemonData = newPokemon;
        setPokemonData.SetPokemon(pokemons[curCount]);
        setPokemonData.SettingData();
        pokemons[curCount].gameObject.SetActive(true);
        curCount++;
    }

    public Pokemon GetPokemon()
    {
        if (pokemons.Count == 0)
            return null;

        return pokemons[0];
    }
}
