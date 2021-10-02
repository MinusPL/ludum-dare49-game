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

	public float velocityThreshold = 0.05f;

	private Rigidbody2D rb;

	public float RotateSpeed = 10.0f;

	private float rotationAngle;

	private Bounds bound;

	private bool isMoving = false;

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
		if (follow || isThrown)
		{
			rb.velocity = force * 100;
			isThrown = false;
		}
	}

	public void Update()
	{
		if (follow)
		{
			rotationAngle += Input.mouseScrollDelta.y * RotateSpeed;
			if (rotationAngle > 360f) rotationAngle -= 360f;
			rb.MoveRotation(rotationAngle);
		}

		if (!follow)
		{
			//slow?
			bound = new Bounds(transform.position, Vector3.zero);
			foreach (var c in GetComponentsInChildren<Collider2D>())
			{
				bound.Encapsulate(c.bounds);
			}
		}

		isMoving = rb.velocity.magnitude > velocityThreshold;
	}

	private Vector3 GetMouseAsWorldPoint()
	{
		Vector3 mousePoint = new Vector3(Input.mousePosition.x, Input.mousePosition.y, z);
		return Camera.main.ScreenToWorldPoint(mousePoint);
	}

	public Bounds GetBounds()
	{
		return bound;
	}

	public void OnDrawGizmos()
	{
		Gizmos.color = isMoving ? Color.red : Color.blue;
		Gizmos.DrawWireCube(bound.center, bound.size);
	}

	public bool IsMoving()
	{
		return follow || isMoving;
	}
}
