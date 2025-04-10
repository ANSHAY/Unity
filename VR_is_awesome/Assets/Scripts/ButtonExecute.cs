using UnityEngine;
using UnityEngine.EventSystems;
using System.Collections;
using UnityEngine.UI;

public class ButtonExecute : MonoBehaviour {
	public float timeToSelect = 3.0f;
	private float countDown;
	private GameObject currentButton;
	private Clicker clicker = new Clicker();
	public Text countDownText;
	private int count = 1;
	private bool action = false;

	void Start(){
		//countDownText = transform.Find("Dashboard").Find ("Countdown").GetComponent<Text> ();
	}
	// Update is called once per frame
	void Update () {
		Transform camera = Camera.main.transform;
		RaycastHit hit;
		GameObject hitButton = null;
		PointerEventData data = new PointerEventData (EventSystem.current);

		Ray ray = new Ray (camera.position, camera.rotation * Vector3.forward);
		if (Physics.Raycast (ray, out hit)) {
			if (hit.transform.gameObject.tag == "Button") {
				hitButton = hit.transform.parent.gameObject;
			}
		}
		if (hitButton != currentButton) {
			if (currentButton != null) {
				ExecuteEvents.Execute<IPointerExitHandler> (currentButton, data, ExecuteEvents.pointerExitHandler);
				countDown = timeToSelect;
				action = false;
			}
			currentButton = hitButton;
			if (currentButton != null) {
				ExecuteEvents.Execute<IPointerEnterHandler> (currentButton, data, ExecuteEvents.pointerEnterHandler);
				countDown = timeToSelect;
				count = 1;
			}
		} else {
			if (action) {
				count = 0;
				countDown = 0;
			}
		}

		if (currentButton != null) {
			countDown -= Time.deltaTime * count;
			if (clicker.clicked () || countDown < 0.0f) {
				ExecuteEvents.Execute<IPointerClickHandler> (currentButton, data, ExecuteEvents.pointerClickHandler);
				//countDown = 0;//timeToSelect;
				count = 0;
				action = true;
			}
			countDownText.text = countDown>0?(Mathf.Ceil (countDown).ToString ()):"";
		} else {
			countDownText.text = "";
		}


	}
}
