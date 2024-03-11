using System.Collections.Generic;
using UnityEngine;

public class GameManager : Singleton<GameManager>
{
    // �÷��̾��� ���ϸ� �����ϱ�
    public int maxCount = 6;           // �ִ� ���� ��
    public int curCount;    // ���� ��

    public List<PokemonData> pokemonDatas = new List<PokemonData>();  // ���ϸ� ������
    public List<Pokemon> pokemons = new List<Pokemon>();              // ���ϸ� ������Ʈ
    public List<int> pokemonsLevel = new List<int>();                 // ���ϸ� ����

    public void SetPokemon(PokemonData newPokemon)
    {
        if (pokemons.Count == maxCount)
        {
            Debug.Log("�ִ� ������!");
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
