using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleScene : BaseScene
{
    [SerializeField] Pokemon player;
    [SerializeField] Pokemon enemy;

    public void LoadScene()
    {
        Manager.Scene.LoadScene("FieldScene");
    }

    public override IEnumerator LoadingRoutine()
    {
        yield return null;
    }
}
