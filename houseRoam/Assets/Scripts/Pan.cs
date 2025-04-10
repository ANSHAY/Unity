using UnityEngine;
using System.Collections;

public class Pan : MonoBehaviour {
	public void rotCam(float rot){
		float r = Camera.main.transform.rotation.eulerAngles.y;
		Debug.Log (r);
		r += rot;
		Debug.Log (r);
		Camera.main.transform.rotation = Quaternion.Euler (0, r, 0);
	}
}
