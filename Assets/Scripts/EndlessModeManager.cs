using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[System.Serializable]
public class EndlessModeManager : MonoBehaviour
{
    private float maxHeight;

    public DialogEvent dialogEvent;
    public PauseEvent pauseEvent;
    public TimerEvent timerEvent;
    public LevelWonEvent wonEvent;

    public HeightEvent heightEvent;

    public int startDialogID = 0;
    private bool menuShown = false;

    public GameObject spawner;
    public GameObject[] objectsToSpawn;

    // Start is called before the first frame update
    void Start()
    {
        if(dialogEvent == null) dialogEvent = new DialogEvent();
        if(pauseEvent == null) pauseEvent = new PauseEvent();
        if(heightEvent == null) heightEvent = new HeightEvent();
        if(timerEvent == null) timerEvent = new TimerEvent();
        if(wonEvent == null) wonEvent = new LevelWonEvent();


        maxHeight = 0;
        //levelUIManager = GetComponent<LevelUIManager>();

        Invoke("LateStart", 0.5f);
    }

    // Update is called once per frame
    void Update()
    {
        maxHeight = GetMaxHeight();

        heightEvent.Invoke(maxHeight);

        if (spawner.transform.childCount == 0)
        {
            if (objectsToSpawn.Length != 0)
            {
                Instantiate(objectsToSpawn[Random.Range(0, objectsToSpawn.Length)], spawner.transform.position,
                    Quaternion.identity, spawner.transform);
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
        pauseEvent.Invoke(menuShown, true);
    }
}
