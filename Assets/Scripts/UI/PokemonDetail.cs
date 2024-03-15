using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PokemonDetail : MonoBehaviour
{
    [SerializeField] PokemonInfo_1 info1;
    [SerializeField] PokemonInfo_2 info2;
    [SerializeField] PokemonInfo_3 info3;

    [SerializeField] Sprite[] typeImage;

    int currentCount = 0;

    private void Start()
    {
        currentCount = 0;
        info1 = GetComponentInChildren<PokemonInfo_1>();
        info2 = GetComponentInChildren<PokemonInfo_2>();
        info3 = GetComponentInChildren<PokemonInfo_3>();

        info1.gameObject.SetActive(true);
        info2.gameObject.SetActive(false);
        info3.gameObject.SetActive(false);

    }
    private void OnNext(InputValue value)
    {
        Vector2 input = value.Get<Vector2>();

        currentCount += (int)input.x;

        currentCount = Mathf.Clamp(currentCount, 0, 2);

        if (currentCount == 0)
        {
            info1.gameObject.SetActive(true);
            info2.gameObject.SetActive(false);
            info3.gameObject.SetActive(false);
        }
        else if (currentCount == 1)
        {
            info2.gameObject.SetActive(true);
            info1.gameObject.SetActive(false);
            info3.gameObject.SetActive(false);
        }
        else if (currentCount == 2)
        {
            info3.gameObject.SetActive(true);
            info1.gameObject.SetActive(false);
            info2.gameObject.SetActive(false);
        }
    }

    [ContextMenu("SetInfo")]
    public void SetInfo(Pokemon pokemon)
    {
        info1.SetInfo(pokemon);
        info2.SetInfo(pokemon);
        info3.SetInfo(pokemon);
    }
}