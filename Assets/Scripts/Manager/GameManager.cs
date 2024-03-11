using System.Collections.Generic;
using UnityEngine;

public class GameManager : Singleton<GameManager>
{
    // 플레이어의 포켓몬 관리하기
    public int maxCount = 6;           // 최대 소지 수
    public int curCount;    // 현재 수

    public List<PokemonData> pokemonDatas = new List<PokemonData>();  // 포켓몬 데이터
    public List<Pokemon> pokemons = new List<Pokemon>();              // 포켓몬 오브젝트
    public List<int> pokemonsLevel = new List<int>();                 // 포켓몬 레벨

    public void SetPokemon(PokemonData newPokemon)
    {
        if (pokemons.Count == maxCount)
        {
            Debug.Log("최대 소지중!");
            return;
        }
        curCount++;
        pokemons[curCount].gameObject.SetActive(true);
        pokemonDatas.Add(newPokemon);
    }

    public Pokemon GetPokemon()
    {
        if (pokemons.Count == 0)
            return null;

        pokemons[0].Level = pokemonsLevel[0];

        return pokemons[0];
    }
}
