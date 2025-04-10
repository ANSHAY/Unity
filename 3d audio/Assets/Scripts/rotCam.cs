using UnityEngine;
using System.Collections;

public class rotCam : MonoBehaviour {

	public void rotateCameraY(int ind){
		float rot = 0f;
		switch(ind){
		case 0:
			rot = 135f;
			break;
		case 1:
			rot = 90f;
			break;
		case 2:
			rot = 60f;
			break;
		case 3:
			rot = 30f;
			break;
		case 4:
			rot = 0f;
			break;
		case 5:
			rot = 330f;
			break;
		case 6:
			rot = 300f;
			break;
		case 7:
			rot = 270f;
			break;
		case 8:
			rot = 225f;
			break;
		}
		Camera.main.transform.rotation = Quaternion.Euler (0, rot, 0);
	}
}
