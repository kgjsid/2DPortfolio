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
        Time.timeScale = 0f;
        float time = 0f;

        while(time < 0.5f)
        {
            time += Time.unscaledDeltaTime * 12;
            fade.color = new Color(0, 0, 0, time * 2);

            yield return new WaitForSecondsRealtime(0.1f);
        }

        Time.timeScale = 1f;

        while(time > 0f)
        {
            time -= Time.unscaledDeltaTime * 6;
            fade.color = new Color(0, 0, 0, time * 2);

            yield return new WaitForSecondsRealtime(0.1f);
        }
    }
}
