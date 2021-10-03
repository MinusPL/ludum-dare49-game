using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[System.Serializable]
public class DialogEvent : UnityEvent<int, bool> { }
[System.Serializable]
public class PauseEvent : UnityEvent<bool, bool> { }

[System.Serializable]
public class HeightEvent : UnityEvent<float> { }

[System.Serializable]
public class TimerEvent : UnityEvent<float, float, bool> { }

public class LevelManager : MonoBehaviour
{
    public float TargetHeight = 0.0f;
    public float StableTime = 5.0f;
    public GameObject line;

    private float maxHeight;

    public DialogEvent dialogEvent;
    public PauseEvent pauseEvent;
    public TimerEvent timerEvent;

    public HeightEvent heightEvent;

    private bool enableWinTimer = false;
    private float winTimer = 0.0f;

    public float aboveDelay = 0.2f;
    private float aboveDelayTimer = 0.0f;

    public int startDialogID = 0;

    public Color lineStandardColor = new Color(1.0f,1.0f,1.0f);
    public Color lineAboveColor = new Color(0.0f, 1.0f, 0.0f);

    public int unlockLevel = -1;


    public bool running = true;
    private bool menuShown = false;

    // Start is called before the first frame update
    void Start()
    {
        if(dialogEvent == null) dialogEvent = new DialogEvent();
        if(pauseEvent == null) pauseEvent = new PauseEvent();
        if(heightEvent == null) heightEvent = new HeightEvent();
        if(timerEvent == null) timerEvent = new TimerEvent();


        maxHeight = 0;
        line.transform.position = new Vector3(0, TargetHeight, 9);
        //levelUIManager = GetComponent<LevelUIManager>();

        Invoke("LateStart", 0.5f);
    }

    // Update is called once per frame
    void Update()
    {
        if(winTimer > StableTime)
		{
            Debug.Log("END LEVEL YOU MORON");
            if(unlockLevel != -1)
			{
                GameController.Instance.UnlockLevel(unlockLevel);
			}
            running = false;
		}
        maxHeight = GetMaxHeight();

        heightEvent.Invoke(maxHeight);

        line.GetComponent<Renderer>().material.color = enableWinTimer ? lineAboveColor : lineStandardColor;

        //levelUIManager.SetCurrentHeight(maxHeight);

        if (running)
        {
            if (!enableWinTimer)
            {
                if (maxHeight >= TargetHeight)
                {
                    if (aboveDelayTimer >= aboveDelay)
                    {
                        enableWinTimer = true;
                        aboveDelayTimer = 0.0f;
                    }
                    else
					{
                        aboveDelayTimer += Time.deltaTime;
					}
                }
            }
            else
            {
                if (maxHeight < TargetHeight)
                {
                    enableWinTimer = false;
                    winTimer = 0.0f;
                }
                else
                {
                    winTimer += Time.deltaTime;
                }
                timerEvent.Invoke(StableTime, StableTime - winTimer, enableWinTimer);
            }
        }

        if (Input.GetKeyDown(KeyCode.Escape)) PauseMenu();
    }

    public void StartDialog(int id)
	{
        pauseEvent.Invoke(true, false);
        dialogEvent.Invoke(id, true);
	}

    public void PauseLevel(bool pause)
	{
        pauseEvent.Invoke(pause, false);
        running = !pause;
	}

    private void LateStart()
	{
        StartDialog(startDialogID);
	}

    private float GetMaxHeight()
	{
        var objects = GameObject.FindGameObjectsWithTag("BuildingBlock");
        float maxHeight = 0.0f;
        foreach (var obj in objects)
        {
            if(!obj.GetComponent<Draggable>().IsMoving())
                maxHeight = maxHeight < obj.GetComponent<Draggable>().GetBounds().max.y ? obj.GetComponent<Draggable>().GetBounds().max.y : maxHeight;
        }
        return maxHeight;
    }

    private void PauseMenu()
	{
        menuShown = !menuShown;
        running = !menuShown;
        pauseEvent.Invoke(menuShown, true);
    }
}
