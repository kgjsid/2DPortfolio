using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using System.Text;

public class DiaLogUI : MonoBehaviour
{
    [SerializeField] TMP_Text battleLog;

    StringBuilder battleLogText;
    private void OnEnable()
    {
        battleLog.text = "";
    }

    public IEnumerator DisplayLog(string text)
    {
        battleLogText = new StringBuilder();
        foreach (char c in text)
        { // stringbuilder ÀÌ¿כ
            battleLogText.Append(c);
            battleLog.text = $"{battleLogText.ToString()}";

            yield return new WaitForSeconds(0.1f);
        }
    }
}
