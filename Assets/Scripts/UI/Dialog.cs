using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.Events;

[System.Serializable]
public class DialogUnpause : UnityEvent<bool> { }

public class Dialog : MonoBehaviour
{
    public TextMeshProUGUI textObj;
    public GameObject marker;

    public StringDatabase database;

    public DialogUnpause dialogUnpause;

    public float letterTime = 0.4f;
    private float letterTimer = 0.0f;

    private bool typeText = false;

    private string textToSet;
    private string currentText;
    private int textPos = 0;

    StringData currentData;

    public Image img;
    
    // Start is called before the first frame update
    void Start()
    {
        marker.SetActive(false);
        if (dialogUnpause == null) dialogUnpause = new DialogUnpause();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            if (typeText)
            {
                typeText = false;
                currentText = textToSet;
                textObj.text = currentText;
                marker.SetActive(true);
            }
            else
            {
                if (currentData != null && currentData.nextId != -1)
                {
                    SetText(currentData.nextId, true);
                }
                else
                {
                    gameObject.SetActive(false);
                    dialogUnpause.Invoke(false);
                }
            }
        }

        if (typeText)
		{
            if(letterTimer >= letterTime)
			{
                if (textPos > textToSet.Length - 1)
                {
                    typeText = false;
                    marker.SetActive(true);
                }
                else
                {
                    if (textToSet[textPos] != '<')
                        currentText += $"{textToSet[textPos++]}";
                    else
                    {
                        while (textToSet[textPos] != '>')
                        {
                            currentText += $"{textToSet[textPos++]}";
                        }
                        currentText += $"{textToSet[textPos++]}";
                    }
                }
                textObj.text = currentText;
                letterTimer = 0.0f;
            }
            letterTimer += Time.deltaTime;
		}
    }

    public void SetText(string text, bool typing)
	{
        gameObject.SetActive(true);
        if (!typing)
        {
            currentText = text;
            marker.SetActive(true);
        }
        else
        {
            textToSet = text;
            typeText = true;
            currentText = "";
            textPos = 0;
            letterTimer = letterTime;
            marker.SetActive(false);
        }
        textObj.text = currentText;
    }

    public void SetText(int id, bool typing)
	{
        currentData = database.GetStringData(id);
        if (currentData == null) currentData = database.GetStringData(0);
        img.sprite = currentData.image;
        switch(currentData.imageSize)
		{
            case 0:
                img.rectTransform.sizeDelta = new Vector2(312, 312);
                break;
            case 1:
                img.rectTransform.sizeDelta = new Vector2(256, 256);
                break;
		}
        SetText(currentData.data, typing);
	}
}
