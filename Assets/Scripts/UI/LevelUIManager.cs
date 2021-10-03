using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class LevelUIManager : MonoBehaviour
{
    public TextMeshProUGUI heightText;
    public Dialog dialog;
    // Start is called before the first frame update
    void Start()
    {
        heightText.text = $"Height: {0.0f}";
    }

    // Not used so far.
    void Update()
    {

    }
        
    public void SetCurrentHeight(float height)
	{
        heightText.text = $"Height: {height}";
    }
}
