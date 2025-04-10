using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class LookMoveTo : MonoBehaviour {
	public GameObject ground;
	public Transform infoBubble;
	private Text infoText;
	// Update is called once per frame
	void Start(){
		infoBubble = transform.Find ("InfoBubble");
		if (infoBubble != null) {
			infoText = infoBubble.Find ("Text").GetComponent<Text> ();
		}
	}
	void Update () {
		Transform camera = Camera.main.transform;
		Ray ray;
		RaycastHit[] hits;
		GameObject hitObject;

		Debug.DrawRay (camera.transform.position, camera.transform.rotation * Vector3.forward * 100.0f);

		ray = new Ray (camera.position, camera.rotation * Vector3.forward);
		hits = Physics.RaycastAll (ray);
		for(int i=0; i<hits.Length;i++){
			hitObject = hits[i].collider.gameObject;
			if (hitObject == ground) {
				if (infoBubble != null) {
					infoText.text = "Come at "+hits[i].point.x.ToString("F2")+","+hits[i].point.z.ToString("F2");
					infoBubble.LookAt (camera.position);
					infoBubble.Rotate (0.0f, 180.0f, 0.0f);
				}
				//Debug.Log ("Hit (x,y,z): " + hits[i].point.ToString("F2"));
				transform.position = hits[i].point;
			}
		}			
	}
}
