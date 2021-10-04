using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeirdCloudController : MonoBehaviour
{
    private LevelManager manager;
    public int DialogAfterClicking;
    public GameObject cloudBlock;

    public Renderer entityRenderer;

    public float speed = 5.0f;

    public int state = 0;

    // Start is called before the first frame update
    void Start()
    {
        manager = GameObject.FindGameObjectWithTag("LevelManager").GetComponent<LevelManager>();
    }

    // Update is called once per frame
    void Update()
    {
        switch (state)
        {
            case 0:
                transform.position += Vector3.right * speed * Time.deltaTime;
                if (transform.position.x > Camera.main.transform.position.x && !entityRenderer.isVisible) state = 1;
                break;
            case 1:
                float rY = Random.Range(0, Screen.height);
                float rX = Random.Range(5.0f, 10.0f);
                Vector3 newPos = Camera.main.ScreenToWorldPoint(new Vector3(0, rY));
                newPos += new Vector3(-rX, 0.0f, 0.0f);
                transform.position = newPos;
                state = 0;
                break;
        }
    }

	private void OnMouseDown()
	{
        float z = Camera.main.WorldToScreenPoint(transform.position).z;
        Vector3 offset = transform.position - GetMouseAsWorldPoint(z);
        Vector3 mouseLastPos = GetMouseAsWorldPoint(z);

        var obj = Instantiate(cloudBlock);
        obj.GetComponent<Draggable>().SetParams(transform.position, offset, mouseLastPos, false, 0.0f);

        manager.StartDialog(DialogAfterClicking);

        Destroy(gameObject);
    }

    private Vector3 GetMouseAsWorldPoint(float z)
    {
        Vector3 mousePoint = new Vector3(Input.mousePosition.x, Input.mousePosition.y, z);
        return Camera.main.ScreenToWorldPoint(mousePoint);
    }
}
