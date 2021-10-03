using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New String Data", menuName = "Data/String", order = 1)]
public class StringData : ScriptableObject
{
	public int id = 0;
	public int nextId = -1;
	public Sprite image;
	[Header("0 for People, 1 for items")]
	public int imageSize;
	[TextArea(25,25)]
	public string data = "";
}
