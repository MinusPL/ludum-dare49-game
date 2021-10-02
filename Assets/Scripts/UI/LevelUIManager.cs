using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class LevelUIManager : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        GetComponent<TextMeshProUGUI>().text = $"Height: {0.0f}";
    }

    // Update is called once per frame
    void Update()
    {

    }
        
    public void SetCurrentHeight(float height)
	{
        GetComponent<TextMeshProUGUI>().text = $"Height: {height}";

    }
}
