using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class VolumeSlider : MonoBehaviour
{
    public string volumeName;

    public AudioMixer mixer;

    public int sliderNum = 0;

	public void Awake()
	{
		GetComponent<Slider>().value = GameController.Instance.GetVolume(sliderNum);
	}

	public void SetVolume(float value)
	{
        mixer.SetFloat(volumeName, Mathf.Log10(value) * 20.0f);
		GameController.Instance.SetVolume(sliderNum, value);
	}
}

