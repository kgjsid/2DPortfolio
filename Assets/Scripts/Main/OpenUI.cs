using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpenUI : MonoBehaviour
{
    [SerializeField] PopUpUI inventory;

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.Escape))
        {
            Manager.UI.ShowPopUpUI(inventory);
        }

        if(Input.GetKeyDown(KeyCode.P))
        {
            Manager.UI.ClosePopUpUI();
        }
    }
}
