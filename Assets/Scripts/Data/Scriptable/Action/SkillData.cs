using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class SkillData : ScriptableObject
{
    /*
     TODO. ��ų�� ���� ���� �߰� �ʿ�
    (���� ���, ��� ȸ��, �̸�...)
     */
    public Pokemon user;
    public Pokemon enemy;

    public string name;     // ����� �̸�
    public string type;     // ����� Ÿ��(����, Ư��)

    public int power;       // ����� ����
    public int hitRate;     // ���߷�
    public int curPP;       // ���� �Ŀ� ����Ʈ(��� Ƚ��?)
    public int maxPP;       // �ִ� �Ŀ� ����Ʈ

    public abstract void Execute(Pokemon user, Pokemon enemy); // ��ų���� �ݵ�� �������� �����Լ�
}
