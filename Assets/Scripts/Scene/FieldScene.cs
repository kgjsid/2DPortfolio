using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FieldScene : BaseScene
{
    [SerializeField] PlayerMove player;
    public void LoadScene()
    {
        Manager.Scene.LoadScene("BattleScene");
    }
    public override IEnumerator LoadingRoutine()
    {
        player = FindObjectOfType<PlayerMove>();

        player.transform.position = Manager.Game.curPos;
        player.SetCurrentPos();
        player.SetNextPos();
        Debug.Log($"º¯°æµÈ {player.transform.position}");
        yield return null;
    }
}
