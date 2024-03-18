using System.Collections.Generic;
using UnityEngine;
using static Cinemachine.DocumentationSortingAttribute;

public class GameManager : Singleton<GameManager>
{
    // 플레이어의 포켓몬 관리하기
    public int maxCount = 6;           // 최대 소지 수
    public int curCount = 0;           // 현재 수
    public int curPokemon;
    public List<Pokemon> pokemons = new List<Pokemon>();              // 포켓몬 오브젝트    

    public SetPokemonData setPokemonData;
    [SerializeField] PlayerMove player;
    public Vector3 curPos;

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
        InitPokemonData();
        pokemons[curCount].CurExp = level * level * level;
        pokemons[curCount].gameObject.SetActive(true);
        pokemons[curCount].NextExp = (level + 1) * (level + 1) * (level + 1);
        curCount++;
    }

    public Pokemon GetPokemon()
    {
        if (pokemons.Count == 0)
            return null;

        for(int i = 0; i < pokemons.Count; i++)
        {
            if (pokemons[i].PokemonData != null)
            {
                curPokemon = i;
                return pokemons[curPokemon];
            }
        }

        return null;
    }

    public void RecordPos()
    {
        if (player == null)
        {
            player = FindObjectOfType<PlayerMove>();
        }
        curPos = player.gameObject.transform.position;
    }

    public void UpdatePokemonData(Pokemon pokemon)
    {   // 포켓몬 데이터 설정
        setPokemonData.UpdateDate(pokemons[curPokemon], pokemon);
    }

    private void InitPokemonData()
    {
        setPokemonData.SetPokemon(pokemons[curCount]);
        setPokemonData.SettingData();
        pokemons[curCount].CurHp = pokemons[curCount].Hp;
        pokemons[curCount].GetSkills();
        pokemons[curCount].GetSkills();
        pokemons[curCount].GetSkills();
        pokemons[curCount].GetSkills();
    }
}
