using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FadeIn : MonoBehaviour
{
    [SerializeField] Image fade;

    public void ChangeImage()
    {
        StartCoroutine(Fade());
    }

    IEnumerator Fade()
    {
        float time = 0f;

        while(time < 0.5f)
        {
            time += Time.deltaTime * 3;
            fade.color = new Color(0, 0, 0, time * 2);

            yield return null;
        }

        while(time > 0f)
        {
            time -= Time.deltaTime;
            fade.color = new Color(0, 0, 0, time * 2);

            yield return null;
        }
    }
}
