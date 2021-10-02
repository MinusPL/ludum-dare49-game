using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TimerBar : MonoBehaviour
{
    public float currentTime = 5;
    public float maxTime = 5;
    public bool activated = false;
    private Image _timerBar;
    // Start is called before the first frame update
    void Start()
    {
        _timerBar = GetComponent<Image>();
    }

    // Update is called once per frame
    void Update()
    {
        _timerBar.color = activated ? Color.green : Color.red;
        _timerBar.fillAmount = currentTime / maxTime;
    }
}
