using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraManager : MonoBehaviour
{
	public int boundary = 50;
	public float cameraSpeed = 5.0f;
	public float fastSpeed = 20.0f;
	public float upperLimit = 10000.0f;

	private bool paused = false;

	private void Start()
	{

	}

	void Update()
    {
		if (!paused)
		{
			float speed = Input.GetKey(KeyCode.LeftShift) ? fastSpeed : cameraSpeed;
			if (Input.mousePosition.y > Screen.height - boundary) transform.position += new Vector3(0.0f, speed * Time.deltaTime, 0.0f);
			if (Input.mousePosition.y < 0 + boundary) transform.position -= new Vector3(0.0f, speed * Time.deltaTime, 0.0f);

			if (transform.position.y < Camera.main.orthographicSize) transform.position = new Vector3(transform.position.x, Camera.main.orthographicSize, transform.position.z);
			if (transform.position.y > upperLimit) transform.position = new Vector3(transform.position.x, upperLimit, transform.position.z);
		}
	}

	public void SetPaused(bool pause, bool menu)
	{
		paused = pause;
	}
}
