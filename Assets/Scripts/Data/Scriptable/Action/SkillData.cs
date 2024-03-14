using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class SkillData : ScriptableObject
{
    public Pokemon user;
    public Pokemon enemy;

    public string name;     // ����� �̸�
    public Type type;     // ����� Ÿ��(����, Ư��)

    public int power;       // ����� ����
    public int hitRate;     // ���߷�
    public int curPP;       // ���� �Ŀ� ����Ʈ(��� Ƚ��?)
    public int maxPP;       // �ִ� �Ŀ� ����Ʈ
    public abstract float Execute(Pokemon user, Pokemon enemy); // ��ų���� �ݵ�� �������� �����Լ�

    // ex) 0,1 -> ������ �븻(0), ���� �Ҳ�(1)
    // ex) 0,3 -> ������ �븻(0), ���� Ǯ(4)
    public float[,] TypeValue =
    {
        {  1f,   1f,   1f,   1f,   1f,   1f,   1f,   1f,   1f,   1f,   1f,   1f, 0.5f,   0f,   1f},
        {  1f, 0.5f, 0.5f,   2f,   1f,   2f,   1f,   1f, 0.5f,   1f,   1f,   2f, 0.5f,   1f, 0.5f},
        {  1f,   2f, 0.5f, 0.5f,   1f,   1f,   1f,   1f,   2f,   1f,   1f,   1f,   2f,   1f, 0.5f},
        {  1f, 0.5f,   2f, 0.5f,   1f,   1f,   1f, 0.5f,   2f, 0.5f,   1f,   1f,   2f,   1f, 0.5f},
        {  1f,   1f,   2f, 0.5f, 0.5f,   1f,   1f,   1f,   0f,   2f,   1f,   1f,   1f,   1f, 0.5f},
        {  1f,   1f, 0.5f,   2f,   1f, 0.5f,   1f,   1f,   2f,   2f,   1f,   1f,   1f,   1f,   2f},
        {  2f,   1f,   1f,   1f,   1f,   2f,   1f, 0.5f,   1f, 0.5f, 0.5f, 0.5f,   2f,   0f,   1f},
        {  1f,   1f,   1f,   2f,   1f,   1f,   1f, 0.5f, 0.5f,   1f,   1f,   2f, 0.5f, 0.5f,   1f},
        {  1f,   2f,   1f, 0.5f,   2f,   1f,   1f,   2f,   1f,   0f,   1f, 0.5f,   2f,   1f,   1f},
        {  1f,   1f,   1f,   2f, 0.5f,   1f,   2f,   1f,   1f,   1f,   1f,   2f, 0.5f,   1f,   1f},
        {  1f,   1f,   1f,   1f,   1f,   1f,   2f,   2f,   1f,   1f, 0.5f,   1f,   1f,   1f,   1f},
        {  1f, 0.5f,   1f,   2f,   1f,   1f, 0.5f,   2f,   1f, 0.5f,   2f,   1f,   1f, 0.5f,   1f},
        {  1f,   2f,   1f,   1f,   1f,   2f, 0.5f,   1f, 0.5f,   2f,   1f,   2f,   1f,   1f,   1f},
        {  0f,   1f,   1f,   1f,   1f,   1f,   1f,   1f,   1f,   1f,   0f,   1f,   1f,   2f,   1f},
        {  1f,   1f,   1f,   1f,   1f,   1f,   1f,   1f,   1f,   1f,   1f,   1f,   1f,   1f,   2f}
    }
    ;

    public float EqualAttack(Pokemon user)
    {
        return user.PokemonData.type == type ? 1.5f : 1f; 
    }

    public float AttackDamage(Pokemon user, Pokemon enemy)
    {
        // ������ ���� : (((���� * 0.4) + 2) * ����� ����(%) * ������ ����(Ư��) * 0.5 * 0.01) / ������ ���(Ư��) * (ġ��Ÿ�� 2��) * ������(217-255) / 255
        // Ÿ�� �󼺰� ������ �Ӽ����� ���ݽ� 1.5�� ����
        return (((((user.Level * 0.4f) + 2) * power * user.Damage * 0.02f) / enemy.Defence) * (Random.Range(217, 256) / 255f));
    }

    public float SpecialDamage(Pokemon user, Pokemon enemy)
    {
        return (((((user.Level * 0.4f) + 2) * power * user.SpecialDamage * 0.02f) / enemy.SpecialDefence) * (Random.Range(217, 256) / 255f));
    }
}

public enum Type // ���ϸ� Ÿ��
{
    Normal,
    Fire,
    Water,
    Grass,
    Electric,
    Ice,
    Fighting,
    Poison,
    Ground,
    Flying,
    Psychic,
    Bug,
    Rock,
    Ghost,
    Dragon
}