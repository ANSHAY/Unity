using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Tile : MonoBehaviour {


	public bool mergedThisTurn = false;
	public int indRow, indCol;

	public int Number {
		get {
			return number;
		}
		set {
			number = value;
			if (number >= 0){
				ApplyStyle (number);
			}
		}
	}
	private int number;

	//private Text TileText;
	private Image TileImage;
	public Animator anim;

	void Awake(){
		anim = GetComponent<Animator> ();
		TileImage = transform.Find ("NumberedCell").GetComponent<Image> ();
	}

	public void PlayAppearAnimation(){
		anim.SetTrigger ("Appear");
	}

	public void PlayMergeAnimation(int index){
		anim.SetTrigger ("Merge"); //+index.ToString());
	}

	void ApplyStyleFromHolder(int index){
		TileImage.sprite = TileStyleHolder.Instance.TileStyles [index].img;
	}

	void ApplyStyle(int num){
		switch (num) {
		case 0:
			ApplyStyleFromHolder (0);
			break;
		case 2:
			ApplyStyleFromHolder (1);
			break;
		case 4:
			ApplyStyleFromHolder (2);
			break;
		case 8:
			ApplyStyleFromHolder (3);
			break;
		case 16:
			ApplyStyleFromHolder (4);
			break;
		case 32:
			ApplyStyleFromHolder (5);
			break;
		case 64:
			ApplyStyleFromHolder (6);
			break;
		case 128:
			ApplyStyleFromHolder (7);
			break;
		case 256:
			ApplyStyleFromHolder (8);
			break;
		case 512:
			ApplyStyleFromHolder (9);
			break;
		case 1024:
			ApplyStyleFromHolder (10);
			break;
		case 2048:
			ApplyStyleFromHolder (11);
			break;
		case 4096:
			ApplyStyleFromHolder (12);
			break;
		default:
			Debug.LogError ("oooo... wrong number");
			break;
		}
	}

	private void setVisible(){
		//TileText.enabled = false;
		TileImage.enabled = true;
	}

	private void setEmpty(){
		//TileText.enabled = false;
		TileImage.enabled = true;
	}

	void start(){
	}

	void update(){
	}
}
