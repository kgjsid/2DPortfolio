using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RunButton : ActionUI
{
    public void RunButtonClick()
    {
        BattleManager.Battle.DisplayLog($"Got away safely!");
        BattleManager.Battle.EndBattle();
    }
}
