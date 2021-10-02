using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class Draggable : MonoBehaviour
{
	private Vector3 offset;
	private float z;

	private bool follow = false;
	private bool isThrown = false;

	private Vector3 mouseLastPos;
	private Vector3 mouseCurPos;

	public Vector3 force;

	private Rigidbody2D rb;

	public float RotateSpeed = 10.0f;

	private float rotationAngle;

	private void Start()
	{
		rb = GetComponent<Rigidbody2D>();
		rotationAngle = 0.0f;
	}

	public void OnMouseDown()
	{
		z = Camera.main.WorldToScreenPoint(transform.position).z;
		offset = transform.position - GetMouseAsWorldPoint();
		mouseLastPos = GetMouseAsWorldPoint();
		follow = true;
		//rb.freezeRotation = true;
		rb.gravityScale = 0f;
		rotationAngle = transform.rotation.eulerAngles.z;
	}

	public void OnMouseDrag()
	{
		if (follow) rb.MovePosition(GetMouseAsWorldPoint() + offset);

		mouseCurPos = GetMouseAsWorldPoint();
		force = mouseCurPos - mouseLastPos;
		mouseLastPos = mouseCurPos;
	}

	public void OnMouseUp()
	{
		follow = false;
		isThrown = true;
		//rb.freezeRotation = false;
		rb.gravityScale = 1f;
	}

	public void FixedUpdate()
	{
		if(follow || isThrown)
		{
			rb.velocity = force* 100;
			isThrown = false;
		}
	}

	public void Update()
	{
		if(follow)
		{
			rotationAngle += Input.mouseScrollDelta.y * RotateSpeed;
			if (rotationAngle > 360f) rotationAngle -= 360f;
			rb.MoveRotation(rotationAngle);
		}
	}

	private Vector3 GetMouseAsWorldPoint()
	{
		Vector3 mousePoint = new Vector3(Input.mousePosition.x, Input.mousePosition.y, z);
		return Camera.main.ScreenToWorldPoint(mousePoint);
	}
}
