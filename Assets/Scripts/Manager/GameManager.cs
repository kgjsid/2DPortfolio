using System.Collections.Generic;
using UnityEngine;

public class GameManager : Singleton<GameManager>
{
    // �÷��̾��� ���ϸ� �����ϱ�
    public int maxCount = 6;           // �ִ� ���� ��
    public int curCount = 0;           // ���� ��
    public int curPokemon;
    public List<Pokemon> pokemons = new List<Pokemon>();              // ���ϸ� ������Ʈ

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
            Debug.Log("�ִ� ������!");
            return;
        }
        pokemons[curCount].Level = level;
        pokemons[curCount].PokemonData = newPokemon;
        setPokemonData.SetPokemon(pokemons[curCount]);
        setPokemonData.SettingData();
        pokemons[curCount].GetSkills();
        pokemons[curCount].CurExp = level * level * level;
        pokemons[curCount].CurHp = pokemons[curCount].Hp;
        pokemons[curCount].gameObject.SetActive(true);
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
    {   // ���ϸ� ������ ����
        setPokemonData.UpdateDate(pokemons[curPokemon], pokemon);
    }
}
