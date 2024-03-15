using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBattleAnimation : MonoBehaviour
{
    [SerializeField] Animator animator;

    [ContextMenu("play")]
    public void PlayAnimaion()
    {
        animator.SetTrigger("throw");
    }
}
