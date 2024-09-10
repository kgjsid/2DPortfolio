using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CancelButton : MonoBehaviour
{
    [SerializeField] PokemonDataUI dataUI;

    public void ClickCancelButton()
    {
        dataUI.gameObject.SetActive(false);
        Manager.Game.Player.enabled = true;
    }
}
