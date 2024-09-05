using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class OpenUI : MonoBehaviour
{
    [SerializeField] PokemonDataUI inventory;
    [SerializeField] PopUpUI detailInfo;

    int count = 0;

    /*private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape) && count == 0)
        {
            count++;
            Manager.UI.ShowPopUpUI(inventory);
        }
        else if (Input.GetKeyDown(KeyCode.Escape) && count != 0)
        {
            Manager.UI.ClosePopUpUI();
            count = 0;
        }
    }*/

    // 재수정 필요. 왜 저렇게 만들었을까...
    private void OnShow(InputValue value)
    {
        bool isOpen = inventory.gameObject.activeSelf;

        inventory.gameObject.SetActive(!isOpen);
        Manager.Game.Player.enabled = isOpen;
    }

}
