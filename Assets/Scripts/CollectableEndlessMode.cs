using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectableEndlessMode : MonoBehaviour
{
    public GameObject blockToSpawn;
    
    private EndlessModeManager manager;

    // Start is called before the first frame update
    void Start()
    {
        manager = GameObject.FindGameObjectWithTag("LevelManager").GetComponent<EndlessModeManager>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

	private void OnMouseDown()
	{
        if (!manager.objectsPaused)
        {
            float z = Camera.main.WorldToScreenPoint(transform.position).z;
            Vector3 offset = transform.position - GetMouseAsWorldPoint(z);
            Vector3 mouseLastPos = GetMouseAsWorldPoint(z);

            var obj = Instantiate(blockToSpawn);
            obj.GetComponent<DraggableEndless>().SetParams(transform.position, offset, mouseLastPos, false, 0.0f);

            Destroy(gameObject);
        }
    }

    private Vector3 GetMouseAsWorldPoint(float z)
    {
        Vector3 mousePoint = new Vector3(Input.mousePosition.x, Input.mousePosition.y, z);
        return Camera.main.ScreenToWorldPoint(mousePoint);
    }
}
