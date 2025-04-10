using UnityEngine;
using System.Collections;

public class DestroyTimeout : MonoBehaviour {
	private float timer = 5.0f;
	// Use this for initialization
	void Start () {
		Destroy (gameObject, timer);		
	}
}
