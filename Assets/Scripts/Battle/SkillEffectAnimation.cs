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
            // 노말한 애니메이션 재생할 수 있도록 설정
            //animator.Play("");
        }
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
