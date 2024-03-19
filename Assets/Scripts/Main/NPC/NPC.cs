using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class NPC : MonoBehaviour, IInteractable
{
    [SerializeField] protected int id;                      // 고유 ID 
    public abstract void Interact(PlayerInteractor player); // 자식에서 구현해야 할 상호작용 
}
