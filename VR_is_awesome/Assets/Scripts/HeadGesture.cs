using UnityEngine;
using System.Collections;

public class HeadGesture : MonoBehaviour {
	public bool isFacingDown = false;
	public bool intentDashboard = false;	// in place of isMovingDown
	private float sweepRate=100f;
	private float previousCameraAngle;
	// Update is called once per frame
	void Start(){
		previousCameraAngle = CameraAngelFromGround ();
	}
	void Update () {
		isFacingDown = DetectFacingDown ();	
		intentDashboard = DetectIntent ();
	}

	private bool DetectFacingDown(){
		return (CameraAngelFromGround () < 60.0);
	}

	private float CameraAngelFromGround(){
		return Vector3.Angle (Vector3.down, Camera.main.transform.rotation * Vector3.forward);
	}

	private bool DetectIntent(){			/// in place of DetectMovingDown
		float currentAngle = CameraAngelFromGround ();
		float movingRate = (previousCameraAngle - currentAngle) / Time.deltaTime;
		previousCameraAngle = currentAngle;
		return  movingRate >= sweepRate;
	}
}
