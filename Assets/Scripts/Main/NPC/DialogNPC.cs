using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Cinemachine.DocumentationSortingAttribute;

public class DialogNPC : NPC
{
    // 단순 대화용 NPC
    public override void Interact(PlayerInteractor player)
    {
        TalkManager.Talk.ShowUI(id);
    }
}
