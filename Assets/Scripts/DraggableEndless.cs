using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class DraggableEndless : MonoBehaviour
{
	private Vector3 offset;
	private float z;

	private bool follow = false;
	private bool isThrown = false;

	private Vector3 mouseLastPos;
	private Vector3 mouseCurPos;

	public Vector3 force;

	public float velocityThreshold = 0.1f;

	private Rigidbody2D rb;

	public float RotateSpeed = 5.0f;

	private float rotationAngle;

	private Bounds bound;

	private bool isMoving = false;

	private EndlessModeManager manager;

	private float scrollDelta = 0.0f;

	private void Awake()
	{
		rb = GetComponent<Rigidbody2D>();
		manager = GameObject.FindGameObjectWithTag("LevelManager").GetComponent<EndlessModeManager>();
		rotationAngle = 0.0f;
	}

	public void OnMouseDown()
	{
		if (!manager.objectsPaused)
		{
			z = Camera.main.WorldToScreenPoint(transform.position).z;
			offset = transform.position - GetMouseAsWorldPoint();
			mouseLastPos = GetMouseAsWorldPoint();
			follow = true;
			//rb.freezeRotation = true;
			rb.gravityScale = 0f;
			rotationAngle = transform.rotation.eulerAngles.z;
		}
	}

	public void OnMouseDrag()
	{
		if (!manager.objectsPaused)
		{
			mouseCurPos = GetMouseAsWorldPoint();
			force = mouseCurPos - mouseLastPos;
			mouseLastPos = mouseCurPos;
		}
	}

	public void OnMouseUp()
	{
		if (!manager.objectsPaused)
		{
			follow = false;
			isThrown = true;
			//rb.freezeRotation = false;
			rb.gravityScale = 1f;
		}
	}

	public void FixedUpdate()
	{
		if (!manager.objectsPaused)
		{
			if (follow || isThrown)
			{
				rb.MovePosition(GetMouseAsWorldPoint() + offset);
				rb.velocity = force * 100;
				isThrown = false;

				rb.MoveRotation(rotationAngle);
			}
		}
	}

	public void Update()
	{
		if (!manager.objectsPaused)
		{
			if (follow)
			{
				if (scrollDelta != 0)
				{
					rotationAngle += Input.mouseScrollDelta.y * RotateSpeed;
					if (rotationAngle > 360f) rotationAngle -= 360f;
					scrollDelta = 0.0f;
				}
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

	public void OnGUI()
	{
		if (Event.current.type == EventType.ScrollWheel)
		{
			scrollDelta = Event.current.delta.y;
			Debug.Log(Event.current.delta);
		}
	}
	public void SetParams(Vector3 position, Vector3 offsetParam, Vector3 mouseLP, bool followParam, float zParam)
	{
		transform.position = new Vector3(position.x, position.y, zParam);
		z = zParam;
		offset = offsetParam;
		mouseLastPos = mouseLP;
		follow = followParam;
		if(followParam) rb.gravityScale = 0f;
		rotationAngle = transform.rotation.eulerAngles.z;
	}
}
