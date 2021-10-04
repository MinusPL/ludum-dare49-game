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

    private float masterVolume = 1.0f;
    private float soundVolume = 1.0f;
    private float musicVolume = 1.0f;

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
                masterVolume = br.ReadSingle();
                musicVolume = br.ReadSingle();
                soundVolume = br.ReadSingle();
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
            bw.Write(masterVolume);
            bw.Write(musicVolume);
            bw.Write(soundVolume);
        }
    }

    public void SetVolume(int slider, float val)
	{
        switch(slider)
		{
            case 0:
                masterVolume = val;
                break;
            case 1:
                musicVolume = val;
                break;
            case 2:
                soundVolume = val;
                break;
        }
	}

    public float GetVolume(int slider)
	{
        switch (slider)
        {
            case 0:
                return masterVolume;
            case 1:
                return musicVolume;
            case 2:
                return soundVolume;
        }
        return 0;
    }
}