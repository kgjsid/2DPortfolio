using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Pokemon", menuName = "Data/Pokemon")]
public class PokemonData : ScriptableObject
{
    public string name;         // ���ϸ��� �̸�
    public string type;         // ���ϸ� Ÿ��(Ǯ, ��...)
    public Sprite sprite;       // ���ϸ� �̹���

    // ����ġ(Individual Value)
    public int H; // ü��
    public int AP; // ���� ����Ʈ
    public int AD; // ���� ����
    public int SP; // Ư�� ����Ʈ
    public int SD; // Ư�� ����
    public int S;  // ���ǵ�

    // �ɷ�ġ�� ����ġ�� ��������
    // HP -> (H * 2) * (level / 100.0f) + 10 + level 
    // ��ü���� ���ġ�� ���� �κ��� ã�� �� ����, ������ ���...

    public List<SkillData> possessedAction;    // ������ �׼ǵ�, ��ų��
}
