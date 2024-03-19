using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CaptureButton : ActionUI
{
    public void CaptureClick()
    {
        BattleManager.Battle.StartCapture();
    }
}
