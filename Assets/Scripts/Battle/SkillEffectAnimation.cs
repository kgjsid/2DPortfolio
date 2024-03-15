using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class SkillEffectAnimation : MonoBehaviour
{
    [SerializeField] Animator animator;
    [SerializeField] List<string> skillName;

    public void SetEffectAnimator(string name)
    {
        skillName.Add(name);
    }

    public void UseEffect(string name)
    {
        if(skillName.Contains(name))
        {
            animator.Play($"{name}");
        }
        else
        {
            // �븻�� �ִϸ��̼� ����� �� �ֵ��� ����
            //animator.Play("");
        }
    }
    /*
    public void HitEffect()
    {
        // Ÿ�� ����Ʈ
    }

    public void FaintEffect()
    {
        // �״� ����Ʈ?
    }
    */
}
