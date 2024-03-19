using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NpcOak : DialogNPC
{
    [SerializeField] Collider2D[] pokeBalls;

    public void Start()
    {
        for (int i = 0; i < pokeBalls.Length; i++)
        {
            pokeBalls[i].gameObject.SetActive(false);
        }
    }

    public override void Interact(PlayerInteractor player)
    {
        TalkManager.Talk.ShowUI(id);

        for (int i = 0; i < pokeBalls.Length; i++)
        {
            pokeBalls[i].gameObject.SetActive(true);
        }
    }
}
