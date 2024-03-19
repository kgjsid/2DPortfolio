using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class NPC : MonoBehaviour, IInteractable
{
    [SerializeField] protected int id;                      // ���� ID 
    public abstract void Interact(PlayerInteractor player); // �ڽĿ��� �����ؾ� �� ��ȣ�ۿ� 
}
