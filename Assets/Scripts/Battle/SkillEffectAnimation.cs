using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class SkillEffectAnimation : MonoBehaviour
{
    [SerializeField] Animator animator;
    [SerializeField] List<string> skillName = new List<string>();

    public void SetEffectAnimator(string name)
    {
        skillName.Add(name);
    }

    public void UseEffect(string name)
    {
        if(skillName.Contains(name))
        {
            animator.Play($"{name}");
            StartCoroutine(nonState());
        }
        else
        {
            // �븻�� �ִϸ��̼� ����� �� �ֵ��� ����
            //animator.Play("");
        }
    }
    IEnumerator nonState()
    {
        yield return new WaitForSeconds(2.0f);
        animator.Play("Empty");
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
