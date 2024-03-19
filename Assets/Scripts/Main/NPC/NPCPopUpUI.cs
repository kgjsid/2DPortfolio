using System.Collections;
using System.Collections.Generic;
using System.Text;
using TMPro;
using UnityEngine;

public class NPCPopUpUI : PopUpUI
{
    [SerializeField] TMP_Text dialogText;

    StringBuilder dialogBuilder;
    private void OnEnable()
    {
        dialogText.text = "";
    }

    public void Display(string text)
    {
        Debug.Log($"Display");
        StartCoroutine(DisplayLog(text));
    }

    public IEnumerator DisplayLog(string text)
    {
        dialogBuilder = new StringBuilder();
        foreach (char c in text)
        { // stringbuilder ÀÌ¿כ
            dialogBuilder.Append(c);
            dialogText.text = dialogBuilder.ToString();

            yield return new WaitForSecondsRealtime(0.1f);
        }
    }
}
