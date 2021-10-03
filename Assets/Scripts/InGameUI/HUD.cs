using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using TMPro;
using UnityEditor.IMGUI.Controls;
using UnityEngine;

public class HUD : MonoBehaviour
{
    // Start is called before the first frame update
    private float currentHeight = 0;

    private TimerBar _timerBar;
    private TMP_Text _heightText;
    
    private void Start()
    {
        _timerBar = GetComponentInChildren<TimerBar>();
        _heightText = GetComponentInChildren<TMP_Text>();
    }

    // Update is called once per frame
    void Update()
    {
        _heightText.SetText(String.Format("Current Height: {0}", currentHeight.ToString("0.00", CultureInfo.InvariantCulture)));
    }

    public void SetHeight(float height)
	{
        currentHeight = height;
    }

    public void SetTimer(float maxTime, float curTime, bool activated)
	{
        _timerBar.currentTime = curTime;
        _timerBar.maxTime = maxTime;
        _timerBar.activated = activated;
    }
}
