using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SkillButton : MonoBehaviour
{
    // 1. �ڽ��� �����ϰ� �ִ� ��ų�� �̿��ؼ� ��ư�� ������ �÷��̾�� �����϶�� �ϱ�
    // 2. �Է� ������ ǥ���ϱ� ���� ��Ŀ ǥ��? (>)

    [SerializeField] Button button;
    [SerializeField] Skill skill;
    [SerializeField] Pokemon pokemon;

    private void Start()
    {
        // button.OnPointerEnter
    }

    public void OnClick()
    {
        // pokemon.SetSkill(this.skill);
    }
}
