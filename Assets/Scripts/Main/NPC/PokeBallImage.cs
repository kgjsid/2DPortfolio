using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;

public class PokeBallImage : MonoBehaviour
{
    [SerializeField] PopUpUI popUpUI;

    public void PopUpImage()
    {
        popUpUI.gameObject.SetActive(true);
    }
}
