using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Cinemachine.DocumentationSortingAttribute;

public class DialogNPC : NPC
{
    // �ܼ� ��ȭ�� NPC
    public override void Interact(PlayerInteractor player)
    {
        TalkManager.Talk.ShowUI(id);
    }
}
