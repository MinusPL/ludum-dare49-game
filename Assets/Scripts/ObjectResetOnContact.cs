using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectResetOnContact : MonoBehaviour
{
    public GameObject respawnPoint;

    // Start is called before the first frame update
    void Start()
    {
    }

    private void OnTriggerStay2D(Collider2D c)
    {
        GameObject obj = c.gameObject;

        if(!obj.GetComponent<Renderer>().isVisible &&
            (transform.position.x < Camera.main.transform.position.x - Camera.main.orthographicSize ||
            transform.position.x > Camera.main.transform.position.x + Camera.main.orthographicSize))
		{
            obj.transform.position = respawnPoint.transform.position;
            obj.GetComponent<Rigidbody2D>().velocity = new Vector2(0.0f, 0.0f);
		}
    }
}

