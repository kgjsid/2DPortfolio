using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class SkillData : ScriptableObject
{
    public Pokemon user;
    public Pokemon enemy;

    public string name;     // ����� �̸�
    public string type;     // ����� Ÿ��(����, Ư��)

    public int power;       // ����� ����
    public int hitRate;     // ���߷�
    public int curPP;       // ���� �Ŀ� ����Ʈ(��� Ƚ��?)
    public int maxPP;       // �ִ� �Ŀ� ����Ʈ

    public abstract void Execute(Pokemon user, Pokemon enemy); // ��ų���� �ݵ�� �������� �����Լ�

    public int AttackDamage(Pokemon user, Pokemon enemy)
    {
        // ������ ���� : (((���� * 0.4) + 2) * ����� ����(%) * ������ ����(Ư��) * 0.5 * 0.01) / ������ ���(Ư��) * (ġ��Ÿ�� 2��) * ������(217-255) / 255
        // Ÿ�� �󼺰� ������ �Ӽ����� ���ݽ� 1.5�� ����
        return (int)(((((user.Level * 0.4f) + 2) * power * user.Damage * 0.02f) / enemy.Defence) * (Random.Range(217, 256) / 255f));
    }

    public int SpecialDamage(Pokemon user, Pokemon enemy)
    {
        return (int)(((((user.Level * 0.4f) + 2) * power * user.SpecialDamage * 0.02f) / enemy.SpecialDefence) * (Random.Range(217, 256) / 255f));
    }
}
