using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPC : MonoBehaviour, IInteractable
{
    [SerializeField] protected PopUpUI npcDialog;
    public virtual void Interact(PlayerInteractor player)
    {

    }
}
