using JetBrains.Annotations;
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

    public void CaptureAnimation()
    {
        animator.SetTrigger("CapturePokemon");
    }

    public void EndAnimation()
    {
        BattleManager.Battle.SettingPlayerPokemon();
    }

    public void CaptureSuccees()
    {
        BattleManager.Battle.CaptureSuccess();
    }
}
