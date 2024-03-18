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

    public bool UseEffect(string name)
    {
        if(skillName.Contains(name))
        {
            animator.Play($"{name}");
            StartCoroutine(nonState());
            return true;
        }

        return false;
    }
    IEnumerator nonState()
    {
        yield return new WaitForSeconds(2.0f);
        animator.Play("Empty");
    }
}
