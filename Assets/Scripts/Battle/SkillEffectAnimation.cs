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
            // 노말한 애니메이션 재생할 수 있도록 설정
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
        // 타격 이펙트
    }

    public void FaintEffect()
    {
        // 죽는 이펙트?
    }
    */
}
