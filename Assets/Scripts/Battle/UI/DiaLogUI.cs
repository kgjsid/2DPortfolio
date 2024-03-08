using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class DiaLogUI : MonoBehaviour
{
    [SerializeField] TMP_Text battleLog;

    private void OnEnable()
    {
        battleLog.text = "";
    }

    public IEnumerator DisplayLog(string text)
    {
        battleLog.text = "";
        foreach (char c in text)
        { // stringbuilder ÀÌ¿כ
            battleLog.text = $"{battleLog.text}{c}";

            yield return new WaitForSeconds(0.1f);
        }
    }
}
