using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class SkillInfoUI : MonoBehaviour
{
    [SerializeField] TMP_Text curPPText;
    [SerializeField] TMP_Text maxPPText;
    [SerializeField] TMP_Text typeText;

    public void ShowSkillInfo(Skill skill)
    {
        curPPText.text = skill.CurPP.ToString();
        maxPPText.text = skill.Skilldata.maxPP.ToString();
        typeText.text = skill.Skilldata.type.ToString();
    }
}
