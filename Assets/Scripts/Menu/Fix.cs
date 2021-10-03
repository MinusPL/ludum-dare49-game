using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Fix : MonoBehaviour
{
    public List<AnimatedMenuOption> texts = new List<AnimatedMenuOption>();
    // Start is called before the first frame update

    public void OnEnable()
    {
        foreach(AnimatedMenuOption t in texts)
        {
            t.OnEnable();
        }
    }
}
