using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class ScoreTracker : MonoBehaviour {
	private int score;
	public static ScoreTracker Instance;
	public Text ScoreText;
	public Text HighScoreText;

	public int Score{
		set{
			score = value;
			ScoreText.text = score.ToString ();
			if (score > PlayerPrefs.GetInt ("HighScore")) {
				PlayerPrefs.SetInt ("HighScore", score);
				HighScoreText.text = score.ToString ();
			}
		}
		get{
			return score;
		}
	}

	void Awake(){
		Instance = this;

		if (!PlayerPrefs.HasKey ("HighScore")) {
			PlayerPrefs.SetInt ("HighScore", 0);
		}

		ScoreText.text = "0";
		HighScoreText.text = PlayerPrefs.GetInt("HighScore").ToString();
	}

}
