using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleScene : BaseScene
{
    [SerializeField] Pokemon player;
    [SerializeField] Pokemon enemy;
    public override IEnumerator LoadingRoutine()
    {
        yield return null;
    }
}
