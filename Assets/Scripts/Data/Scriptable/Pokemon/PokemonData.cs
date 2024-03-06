using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Pokemon", menuName = "Data/Pokemon")]
public class PokemonData : ScriptableObject
{
    // Todo.
    // ���ϸ� ������ ���� ��ü�ϱ�...
    // ������ �Ŵ����� ���� ���� �����ϱ�?
    public string name;
    public int level;
    public int hp;
    public int damage;
    public int controlType;
    public float speed;

    public Pokemon enemy;
    public BaseAction currentAction;

    public List<BaseAction> possessedAction;

    public ActionButton button1;
    public ActionButton button2;
    public ActionButton button3;
    public ActionButton button4;
}
