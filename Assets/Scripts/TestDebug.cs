using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestDebug : MonoBehaviour
{
    [SerializeField] SpriteRenderer spriteRender;
    [SerializeField] Sprite sprite1;
    [SerializeField] Sprite sprite2;

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.A))
        {
            spriteRender.sprite = sprite1;
        }
        if (Input.GetKeyDown(KeyCode.S))
        {
            spriteRender.sprite = sprite2;
        }
    }
}
