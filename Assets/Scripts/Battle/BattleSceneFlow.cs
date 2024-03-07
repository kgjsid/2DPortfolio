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

    // ��� UI �˾�â�� �갡 �����ұ�?
    // ��Ʋ ����
    // ��Ʋ ��
    // ��Ʋ ������ �����ϱ�

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
