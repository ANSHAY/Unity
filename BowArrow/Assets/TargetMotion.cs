using UnityEngine;
using System.Collections;

public class TargetMotion : MonoBehaviour {

	public float speed = 5;
	// Use this for initialization
	void Start () {
		gameObject.transform.position.y = -49;
	}
	
	// Update is called once per frame
	void Update () {
		if (gameObject.transform.position.y >= 50 || gameObject.transform.position.y <= -50) {
			speed *= -1;			
		}
		gameObject.transform.position.y += speed;	
	}
}
