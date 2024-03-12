using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetPokemonData : MonoBehaviour
{
    // 포켓몬의 레벨과 종족값에 따라 스탯 설정 스크립트
    // 스탯은 인터넷 참고....
    private Pokemon pokemon;

    public void SetPokemon(Pokemon pokemon, PokemonData data)
    {
        this.pokemon = pokemon;
        pokemon.PokemonData = data;
        SettingData();
    }
    // 포켓몬 장착
    public void SetPokemon(Pokemon pokemon)
    {
        this.pokemon = pokemon;
        SettingData();
    }
    // 실제 데이터 넣어주기
    public void SettingData()
    {
        SettingHpData();
        SettingOtherData();
        SettingImage();
    }

    private void SettingHpData()
    {
        pokemon.Hp = (int)((pokemon.PokemonData.H * 2) * 0.01f * pokemon.Level) + 10 + pokemon.Level;
        pokemon.CurHp = pokemon.Hp;
    }

    private void SettingOtherData()
    {
        pokemon.Speed = (int)((pokemon.PokemonData.S * 2) * 0.01f * pokemon.Level + 5);
        pokemon.Damage = (int)((pokemon.PokemonData.AP * 2) * 0.01f * pokemon.Level + 5);
        pokemon.Defence = (int)((pokemon.PokemonData.AD * 2) * 0.01f * pokemon.Level + 5);
        pokemon.SpecialDamage = (int)((pokemon.PokemonData.SP * 2) * 0.01f * pokemon.Level + 5);
        pokemon.SpecialDefence = (int)((pokemon.PokemonData.SP * 2) * 0.01f * pokemon.Level + 5);
    }
    private void SettingImage()
    {
        pokemon.Sprite.sprite = pokemon.PokemonData.sprite;
    }
}
