using System.Collections.Generic;
using UnityEngine;

public class GameManager : Singleton<GameManager>
{
    // 플레이어의 포켓몬 관리하기
    public int maxCount = 6;           // 최대 소지 수
    public int curCount = 0;    // 현재 수

    public List<Pokemon> pokemons = new List<Pokemon>();              // 포켓몬 오브젝트

    public SetPokemonData setPokemonData;
    public PlayerMove player;
    public Vector3 curPos;

    private void Start()
    {
        if(player == null)
        {
            player = FindObjectOfType<PlayerMove>();
        }
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

    public void RecordPos()
    {
        curPos = player.gameObject.transform.position;
    }

    public void UpdatePokemonData(Pokemon pokemon)
    {   // 포켓몬 데이터 설정
        setPokemonData.SetPokemon(pokemon);
    }
}
