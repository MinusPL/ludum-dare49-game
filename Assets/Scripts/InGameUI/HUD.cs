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
    public float currentHeight = 0;
    public float currentTimer = 5;
    public float maxTimer = 5;
    public bool activatedTimer = false;

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
        _timerBar.currentTime = currentTimer;
        _timerBar.maxTime = maxTimer;
        _heightText.SetText(String.Format("Current Height: {0}", currentHeight.ToString("0.00", CultureInfo.InvariantCulture)));
    }
}
