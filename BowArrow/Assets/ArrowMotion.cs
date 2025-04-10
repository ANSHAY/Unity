using UnityEngine;
using System.Collections;

public class ArrowMotion : MonoBehaviour {
	public bool move = false;
	public float speedX = 10.0;
	public float speedY = 0.0;
	private float angle = 0.0;

	// Use this for initialization
	void Start () {
		gameObject.transform.position.x = -90;
	}
	
	// Update is called once per frame
	void Update () {
		gameObject.transform.rotation.y = Bow.transform.rotation.y -90;
		angle = Target.transform.position.y / 180.0;
		gameObject.transform.rotation.z = Bow.transform.rotation - 90;
		speedY = Mathf.Tan(angle) * speedX;
		if (move) {
			gameObject.transform.position.x += speedX;
			gameObject.transform.position.y += speedY;
			if (gameObject.transform.position.x >= 80 && gameObject.transform.position.x <= 100) {
				if ((gameObject.transform.position.y >= Target.transform.position.y - 10) && (gameObject.transform.position.y <= Target.transform.position.y + 10)) {
					gameObject.transform.position.Set (-90.0, 0.0, 0.0);
					gameObject.transform.rotation = Bow.transform.rotation;
				}				
			}

			if (gameObject.transform.position.x >= 105) {
				gameObject.transform.position.x = -90;
				move = false;
			}
		}
	
	}
}
