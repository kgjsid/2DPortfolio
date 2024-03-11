using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FieldScene : BaseScene
{
    public void LoadScene()
    {
        Manager.Scene.LoadScene("BattleScene");
    }
    public override IEnumerator LoadingRoutine()
    {
        yield return null;
    }
}
