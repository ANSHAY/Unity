using UnityEngine;
using System.Collections;

[System.Serializable]
public class TileStyle{
	public int number;
	public Sprite img;
}

public class TileStyleHolder : MonoBehaviour {

	public static TileStyleHolder Instance;

	public TileStyle[] TileStyles;

	void Awake(){
		Instance = this;
	}
}
