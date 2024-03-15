using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpenUI : MonoBehaviour
{
    [SerializeField] PopUpUI inventory;
    [SerializeField] PopUpUI detailInfo;

    int count = 0;

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.Escape) && count == 0)
        {
            count++;
            Manager.UI.ShowPopUpUI(inventory);
        }
        else if(Input.GetKeyDown(KeyCode.Escape) && count != 0)
        {
            Manager.UI.ClosePopUpUI();
            count = 0;
        }
    }

}
