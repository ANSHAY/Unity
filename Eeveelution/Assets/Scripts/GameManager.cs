using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

public enum GameState{
	Playing,
	GameOver,
	WaitingForMove
}

public class GameManager : MonoBehaviour {

	public Tile[,] AllTiles = new Tile[4, 4];
	private List <Tile[]> columns = new List <Tile[]> ();
	private List <Tile[]> rows = new List <Tile[]> ();
	private List<Tile> EmptyTiles = new List<Tile> ();
	private bool moveMade;
	private bool[] LineMoveComplete = new bool[4]{true, true, true, true};

	public GameState State;
	[Range(0f, 2f)]
	public float delay;
	public Text GameOverScoreText;
	public Text YouWonScoreText;
	public GameObject GameOverPanel;
	public GameObject YouWonPanel;

	// Use this for initialization
	void Start () {
		Tile[] AllTylesOneDim = GameObject.FindObjectsOfType<Tile> ();
		foreach (Tile t in AllTylesOneDim) {
			t.Number = 0;
			AllTiles [t.indRow, t.indCol] = t;
			EmptyTiles.Add (t);
		}

		columns.Add (new Tile[]{AllTiles[0,0], AllTiles[1,0], AllTiles[2,0], AllTiles[3,0]});
		columns.Add (new Tile[]{AllTiles[0,1], AllTiles[1,1], AllTiles[2,1], AllTiles[3,1]});
		columns.Add (new Tile[]{AllTiles[0,2], AllTiles[1,2], AllTiles[2,2], AllTiles[3,2]});
		columns.Add (new Tile[]{AllTiles[0,3], AllTiles[1,3], AllTiles[2,3], AllTiles[3,3]});

		rows.Add (new Tile[]{AllTiles[0,0], AllTiles[0,1], AllTiles[0,2], AllTiles[0,3]});
		rows.Add (new Tile[]{AllTiles[1,0], AllTiles[1,1], AllTiles[1,2], AllTiles[1,3]});
		rows.Add (new Tile[]{AllTiles[2,0], AllTiles[2,1], AllTiles[2,2], AllTiles[2,3]});
		rows.Add (new Tile[]{AllTiles[3,0], AllTiles[3,1], AllTiles[3,2], AllTiles[3,3]});

		Generate ();
		Generate ();
	}

	public void DisableYouWonPanel(){
		State = GameState.Playing;
		YouWonPanel.SetActive (false);
	}

	private void YouWon(){
		State = GameState.GameOver;
		YouWonScoreText.text = ScoreTracker.Instance.Score.ToString ();
		YouWonPanel.SetActive (true);
	}

	private void GameOver(){
		State = GameState.GameOver;
		GameOverScoreText.text = ScoreTracker.Instance.Score.ToString ();
		GameOverPanel.SetActive (true);
	}

	bool canMove(){
		if (EmptyTiles.Count > 0) {
			return true;
		}
		//check columns
		for (int i = 0; i < columns.Count; i++) {
			for (int j = 0; j < rows.Count - 1; j++) {
				if (AllTiles [j, i].Number == AllTiles [j + 1, i].Number) {
					return true;
				}
			}
		}
		//check rows
		for (int i = 0; i < rows.Count; i++) {
			for (int j = 0; j < columns.Count - 1; j++) {
				if (AllTiles [i, j].Number == AllTiles [i, j+1].Number) {
					return true;
				}
			}
		}
		return false;
	}

	public void NewGameButtonHandler(){
		Application.LoadLevel (Application.loadedLevel);
	}

	bool MakeOneMoveDownIndex(Tile[] ListOfTiles){
		for (int i = 0; i < ListOfTiles.Length - 1; i++) {
			// Move
			if (ListOfTiles [i].Number == 0 && ListOfTiles [i + 1].Number > 0) {
				ListOfTiles [i].Number = ListOfTiles [i + 1].Number;
				ListOfTiles [i + 1].Number = 0;
				return true;
			}
			// Merge
			if (ListOfTiles [i].Number != 0 &&
				ListOfTiles [i].Number == ListOfTiles [i + 1].Number &&
				ListOfTiles[i].mergedThisTurn == false && ListOfTiles[i+1].mergedThisTurn == false){
				ListOfTiles [i].Number *= 2;
				ListOfTiles [i + 1].Number = 0;
				ListOfTiles [i].mergedThisTurn = true;
				ListOfTiles [i].PlayMergeAnimation (ListOfTiles [i].Number);
				ScoreTracker.Instance.Score += ListOfTiles [i].Number;
				if (ListOfTiles [i].Number == 2048) {
					YouWon ();
				}
				return true;
			}
		}
		return false;
	}

	bool MakeOneMoveUpIndex(Tile[] ListOfTiles){
		for (int i = ListOfTiles.Length - 1; i > 0; i--) {
			// Move
			if (ListOfTiles [i].Number == 0 && ListOfTiles [i - 1].Number > 0) {
				ListOfTiles [i].Number = ListOfTiles [i - 1].Number;
				ListOfTiles [i - 1].Number = 0;
				return true;
			}
			// Merge
			if (ListOfTiles [i].Number != 0 &&
				ListOfTiles [i].Number == ListOfTiles [i - 1].Number &&
				ListOfTiles[i].mergedThisTurn == false && ListOfTiles[i-1].mergedThisTurn == false){
				ListOfTiles [i].Number *= 2;
				ListOfTiles [i - 1].Number = 0;
				ListOfTiles [i].mergedThisTurn = true;
				ListOfTiles [i].PlayMergeAnimation (ListOfTiles [i].Number);
				ScoreTracker.Instance.Score += ListOfTiles [i].Number;
				if (ListOfTiles [i].Number == 2048) {
					YouWon ();
				}return true;
			}
		}	
		return false;
	}

	void Generate(){
		if (EmptyTiles.Count > 0) {
			int indexForNewNumber = Random.Range (0, EmptyTiles.Count);
			int randomNum = Random.Range (0, 20);
			if (randomNum == 0) {
				EmptyTiles [indexForNewNumber].Number = 4;
			}
			else {
				EmptyTiles [indexForNewNumber].Number = 2;
			}
			EmptyTiles [indexForNewNumber].PlayAppearAnimation ();
			EmptyTiles.RemoveAt (indexForNewNumber);
		}
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetKeyDown (KeyCode.G)) {
			Generate ();
		}	
	}

	private void ResetMergeStatus(){
		foreach (Tile t in AllTiles) {
			t.mergedThisTurn = false;
		}
	}

	private void UpdateEmptyTiles(){
		EmptyTiles.Clear ();
		foreach (Tile t in AllTiles) {
			if (t.Number == 0){
				EmptyTiles.Add (t);
			}
		}
	}

	IEnumerator MakeOneMoveDownIndexCoroutine(Tile[] line, int index){
		LineMoveComplete [index] = false;
		while (MakeOneMoveDownIndex (line)) {
			moveMade = true;
			yield return new WaitForSeconds (delay);
		}
		LineMoveComplete [index] = true;
	}

	IEnumerator MakeOneMoveUpIndexCoroutine(Tile[] line, int index){
		LineMoveComplete [index] = false;
		while (MakeOneMoveUpIndex (line)) {
			moveMade = true;
			yield return new WaitForSeconds (delay);
		}
		LineMoveComplete [index] = true;
	}

	IEnumerator MoveCoroutine(MoveDirection md){
		State = GameState.WaitingForMove;
		switch (md) {
		case MoveDirection.Down:
			for (int i = 0; i < columns.Count; i++) {
				StartCoroutine (MakeOneMoveUpIndexCoroutine (columns [i], i));
			}
			break;
		case MoveDirection.Left:
			for (int i = 0; i < rows.Count; i++){
				StartCoroutine (MakeOneMoveDownIndexCoroutine (rows [i], i));
			}
			break;
		case MoveDirection.Right:
			for (int i = 0; i < rows.Count; i++){
				StartCoroutine (MakeOneMoveUpIndexCoroutine (rows [i], i));
			}
			break;
		case MoveDirection.Up:
			for (int i = 0; i < columns.Count; i++){
				StartCoroutine (MakeOneMoveDownIndexCoroutine (columns [i], i));
			}
			break;
		}

		while(!(LineMoveComplete[0] && LineMoveComplete[1] && LineMoveComplete[2] && LineMoveComplete[3])){
			yield return null;
		}

		if (moveMade == true) {
			UpdateEmptyTiles ();
			Generate ();
		}
		if (!canMove ()) {
			GameOver ();
		}
		State = GameState.Playing;
		StopAllCoroutines ();
	}

	public void Move(MoveDirection md){
		Debug.Log (md.ToString () + " move");
		moveMade = false;
		ResetMergeStatus ();
		if (delay > 0) {
			StartCoroutine (MoveCoroutine (md));
		} else {
			for (int i = 0; i < columns.Count; i++) {
				switch (md) {
				case MoveDirection.Down:
					while (MakeOneMoveUpIndex (columns [i])) {
						moveMade = true;
					}

					break;
				case MoveDirection.Left:
					while (MakeOneMoveDownIndex (rows [i])) {
						moveMade = true;
					}
			
					break;
				case MoveDirection.Right:
					while (MakeOneMoveUpIndex (rows [i])) {
						moveMade = true;
					}
			
					break;
				case MoveDirection.Up:
					while (MakeOneMoveDownIndex (columns [i])) {
						moveMade = true;
					}
					break;
				}
			}

			if (moveMade == true) {
				UpdateEmptyTiles ();
				Generate ();
			}
			if (!canMove ()) {
				State = GameState.GameOver;
				GameOver ();
			} else {
				State = GameState.Playing;
			}
		}
	}
}
