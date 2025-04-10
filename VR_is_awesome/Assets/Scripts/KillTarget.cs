using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class KillTarget : MonoBehaviour {
	public GameObject target;
	public GameObject killEffect;
	public ParticleSystem hitEffect;
	public float timeToSelect = 3.0f;
	public int score;
	private float countDown;
	public Text scoreText;

	public Transform healthBar;
	private Transform healthBarColor;
	private float health;

	// Use this for initialization
	void Start () {
		score = 0;
		countDown = timeToSelect;
		hitEffect.enableEmission = false;
		scoreText.text = "Score : 0";

		healthBarColor = healthBar.Find ("HealthBarColor");
		HealthBarHandle ();
	}
	
	// Update is called once per frame
	void Update () {
		Transform camera = Camera.main.transform;
		Ray ray = new Ray (camera.position, camera.rotation * Vector3.forward);
		RaycastHit hit;

		if (Physics.Raycast (ray, out hit) && hit.collider.gameObject == target) {
			if (countDown > 0.0f) {
				countDown -= Time.deltaTime;
				hitEffect.transform.position = hit.point;
				hitEffect.enableEmission = true;
			} else {
				Instantiate (killEffect, target.transform.position, target.transform.rotation);
				countDown = timeToSelect;
				score += 1;
				scoreText.text = "Score : " + score;
				hitEffect.enableEmission = false;
				SetRandomPosition ();
			}
		} else {
			countDown = timeToSelect;
			hitEffect.enableEmission = false;
		}
		HealthBarHandle ();
		healthBar.LookAt (camera.position);
		healthBar.Rotate (0.0f, 180.0f, 0.0f);
	}

	void HealthBarHandle(){
		health = countDown/timeToSelect;
		healthBarColor.GetComponent<RectTransform> ().localScale = new Vector3 (health, 1, 1);
		if (health >= 1.0f) {
			healthBar.localPosition = new Vector3 (0, 100, 0);
		} else {
			healthBar.localPosition = new Vector3 (0, 1.66f, 0);
		}
		if (health < 0.4f) {
			healthBarColor.GetComponent<Image> ().color = Color.red;
		} else {
			healthBarColor.GetComponent<Image> ().color = Color.green;
		}
	}

	void SetRandomPosition(){
		float x = Random.Range (-5.0f, 5.0f);
		float z = Random.Range (-5.0f, 5.0f);
		target.transform.position = new Vector3(x, 0.0f, z);
	}
}
