using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    public float TargetHeight = 0.0f;
    public float StableTime = 5.0f;

    public LevelUIManager levelUIManager;
    public GameObject line;

    private float maxHeight;

    private bool enableWinTimer = false;
    private float winTimer = 0.0f;

    public float aboveDelay = 0.2f;
    private float aboveDelayTimer = 0.0f;

    public Color lineStandardColor = new Color(1.0f,1.0f,1.0f);
    public Color lineAboveColor = new Color(0.0f, 1.0f, 0.0f);


    private bool running = true;

    // Start is called before the first frame update
    void Start()
    {
        maxHeight = 0;
        line.transform.position = new Vector3(0, TargetHeight, -8);
    }

    // Update is called once per frame
    void Update()
    {
        if(winTimer > StableTime)
		{
            Debug.Log("END LEVEL YOU MORON");
            running = false;
		}
        maxHeight = GetMaxHeight();

        line.GetComponent<Renderer>().material.color = enableWinTimer ? lineAboveColor : lineStandardColor;

        levelUIManager.SetCurrentHeight(maxHeight);

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
            }
        }
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
}
