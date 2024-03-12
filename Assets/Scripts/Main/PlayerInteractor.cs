using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInteractor : MonoBehaviour
{
    // 상호작용 스크립트
    Collider2D[] colliders = new Collider2D[10];
    private void OnInteract(InputValue value)
    {
        int size = Physics2D.OverlapCircleNonAlloc(transform.position, 0.5f, colliders);
        
        for(int i = 0; i < size; i++)
        {
            IInteractable interactable = colliders[i].GetComponent<IInteractable>();

            if(interactable != null)
            {
                interactable.Interact(this);
            }
        }
    }
}
