using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class StringDatabase : MonoBehaviour
{
    [SerializeField]
    private List<StringData> strings;

    // Start is called before the first frame update
    void Start()
    {
        
        var loaded = Resources.LoadAll("String", typeof(StringData));
        strings = new List<StringData>();
        foreach(var e in loaded)
		{
            strings.Add((StringData)e);
		}
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public StringData GetStringData(int id)
	{
        return strings.FirstOrDefault<StringData>(s => s.id == id);
	}
}
