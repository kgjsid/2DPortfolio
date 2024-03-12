using System.Collections.Generic;
using UnityEngine;

public class GameManager : Singleton<GameManager>
{
    // �÷��̾��� ���ϸ� �����ϱ�
    public int maxCount = 6;           // �ִ� ���� ��
    public int curCount = 0;    // ���� ��

    public List<Pokemon> pokemons = new List<Pokemon>();              // ���ϸ� ������Ʈ

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
            Debug.Log("�ִ� ������!");
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
