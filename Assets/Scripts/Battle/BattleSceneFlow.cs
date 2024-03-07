using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class BattleSceneFlow : MonoBehaviour
{
    [SerializeField] PopUpUI dialog;
    [SerializeField] PopUpUI skillSlot;
    [SerializeField] BattleUI playerUI;
    [SerializeField] BattleUI enemyUI;

    Queue<Pokemon> actionRank;

    // 모든 UI 팝업창은 얘가 관리할까?
    // 배틀 돌입
    // 배틀 중
    // 배틀 끝으로 관리하기

    private void Awake()
    {
        Manager.UI.ShowPopUpUI(dialog);
    }

    public void BattleUI()
    {
        Manager.UI.ShowPopUpUI(skillSlot);
    }

    public void EnCounter()
    {
    }

    public void BattleRead()
    {

    }

    public void BattleChoice()
    {

    }

    public void BattleRunning()
    {

    }

    public void BattleEnd()
    {

    }

}
