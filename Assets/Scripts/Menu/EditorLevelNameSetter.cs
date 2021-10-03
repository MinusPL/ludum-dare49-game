using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class EditorLevelNameSetter : MonoBehaviour
{
    [ContextMenu("Set Grid Names")]
    public void SetNames()
    {
        /*var panels = GetComponentsInChildren<TMP_Text>();
        int i = 1;
        foreach (var panel in panels)
        {
            panel.SetText((i++).ToString());
            var parent = panel.transform.parent.GetComponentInParent<LevelSelect>();
            parent.levelNumber = 1;
        }*/
        var levelButtons = GetComponentsInChildren<LevelSelect>();
        int i = 1;
        foreach (var levelButton in levelButtons)
        {
            levelButton.levelNumber = i;
            levelButton.GetComponentInChildren<TMP_Text>().SetText((i++).ToString());
        }
    }
}
