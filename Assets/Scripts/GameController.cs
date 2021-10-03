using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour
{
    public static GameController Instance
    {
        get;
        set;
    }

    private int _levelUnlockedFlags = 1;

    private void Awake()
    {
        DontDestroyOnLoad(transform.gameObject);
        Instance = this;
        LoadSave();
    }

    void Start()
    {
        SceneManager.LoadScene(1);
    }

    public bool IsLevelUnlocked(int levelNumber)
    {
        return (_levelUnlockedFlags & (1 << (levelNumber - 1))) != 0;
    }

    public void UnlockLevel(int levelNumber)
    {
        _levelUnlockedFlags |= (1 << (levelNumber - 1));
        SaveData();
    }

    public void LoadSave()
    {
        if(File.Exists(Application.persistentDataPath + "/save01.sav"))
		{
            using (var file = File.Open(Application.persistentDataPath + "/save01.sav", FileMode.Open))
            {
                BinaryReader br = new BinaryReader(file);

                _levelUnlockedFlags = br.ReadInt32();
                float a = (float)br.ReadInt32();
                float b = (float)br.ReadInt32();
                float c = (float)br.ReadInt32();
            }
		}
		else
		{
            SaveData();
		}
    }
    public void SaveData()
    {
        using (var file = File.Open(Application.persistentDataPath + "/save01.sav", FileMode.Create))
        {
            BinaryWriter bw = new BinaryWriter(file);

            bw.Write(_levelUnlockedFlags);
            bw.Write(0.0f);
            bw.Write(0.0f);
            bw.Write(0.0f);
        }
    }
}