using UnityEngine;
using System.Collections;

public class CursorPositioner : MonoBehaviour {
	private float defaultPos;
	// Use this for initialization
	void Start () {
		defaultPos = transform.localPosition.z;
	}
	
	// Update is called once per frame
	void Update () {
		Transform camera = Camera.main.transform;
		Ray ray = new Ray(camera.position, camera.rotation*Vector3.forward);
		RaycastHit hit;
		if (Physics.Raycast (ray, out hit)) {
			if (hit.distance < defaultPos) {
				transform.localPosition = new Vector3(0,0,hit.distance);
			} else {
				transform.localPosition = new Vector3(0,0,defaultPos);
			}
		}
	}
}
