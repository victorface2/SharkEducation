using UnityEngine;
using System.Collections;

public class ScoreChanger : MonoBehaviour {

	public void UpdateScore(int score){
		gameObject.GetComponent<TextMesh>().text = "Score: " + score;
	}
}
