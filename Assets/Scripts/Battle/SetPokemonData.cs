using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetPokemonData : MonoBehaviour
{
    // ���ϸ��� ������ �������� ���� ���� ���� ��ũ��Ʈ
    // ������ ���ͳ� ����....
    private Pokemon pokemon;

    public void SetPokemon(Pokemon pokemon)
    {
        this.pokemon = pokemon;
    }

    public void SettingData()
    {
        SettingHpData();
        SettingOtherData();
    }

    private void SettingHpData()
    {
        pokemon.Hp = (int)((pokemon.PokemonData.H * 2) * 0.01f * pokemon.Level) + 10 + pokemon.Level;
    }

    private void SettingOtherData()
    {
        pokemon.Speed = (int)((pokemon.PokemonData.S * 2) * 0.01f * pokemon.Level + 5);
        pokemon.Damage = (int)((pokemon.PokemonData.AP * 2) * 0.01f * pokemon.Level + 5);
        pokemon.Defence = (int)((pokemon.PokemonData.AD * 2) * 0.01f * pokemon.Level + 5);
        pokemon.SpecialDamage = (int)((pokemon.PokemonData.SP * 2) * 0.01f * pokemon.Level + 5);
        pokemon.SpecialDefence = (int)((pokemon.PokemonData.SP * 2) * 0.01f * pokemon.Level + 5);
    }
}