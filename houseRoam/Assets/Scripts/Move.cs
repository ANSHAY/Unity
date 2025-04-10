using UnityEngine;
using System.Collections;

public class Move : MonoBehaviour {

	public void Forward(int step=1){
		Camera.main.transform.position += new Vector3(0,0,step);
	}

	public void right(int step=1){
		Camera.main.transform.position += new Vector3 (step, 0, 0);
	}
}
