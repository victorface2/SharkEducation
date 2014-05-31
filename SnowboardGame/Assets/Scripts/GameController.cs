using UnityEngine;
using System.Collections;

public class GameController : MonoBehaviour {
	
	private bool gameOver;

	//ramps
	public float lastRampIndex;
	public int rampIndex = 0;
	public GameObject[] ramps;

	//Sharks
	public float lastSharkIndex;
	public int sharkIndex = 0;
	public GameObject[] sharks;

	//Cubes
	public float lastCubeIndex;
	public int cubeIndex = 0;
	public GameObject[] cubes;

	private int score;
	public TextMesh scoreText;
	public TextMesh resetText;
	public int cubeScoreValue;

	public GameObject water;
	
	void Start () {
		gameOver = false;
		resetText.text = "";
		score = 0;
		UpdateScore ();
	}
	
	// Update is called once per frame
	void Update () {
		if (gameOver) {
			resetText.text = "Press R to restart (W for whale mode)";
		}
		if (Input.GetKeyDown (KeyCode.R)) {
			Application.LoadLevel (Application.loadedLevel);
		}
	}

	void UpdateScore() {
		scoreText.text =  "Score: " + score;
	}
	
	public void AddScore() {
		score += cubeScoreValue;
		UpdateScore ();
	}

	public void turnWaterOff() {
		water.SetActive (false);
	}

	public void turnWaterOn() {
		water.SetActive (true);
	}
	
	public void setGameOver() {
		gameOver = true;
	}

	public bool isGameOver() {
		return gameOver;
	}

	public void addRamp(GameObject obj){
		ramps[rampIndex++] = obj;
	}
	
	public void resetRamps(){
		for(int i = 0; i <ramps.Length; i++){
			Destroy(ramps[i]);
		}
		rampIndex = 0;
	}

	public void addShark(GameObject obj){
		sharks[sharkIndex++] = obj;
	}
	
	public void resetSharks(){
		for(int i = 0; i <sharks.Length; i++){
			Destroy(sharks[i]);
		}
		sharkIndex = 0;
	}

	public void addCube(GameObject obj){
		cubes[cubeIndex++] = obj;
	}
	
	public void resetCubes(){
		for(int i = 0; i <cubes.Length; i++){
			Destroy(cubes[i]);
		}
		cubeIndex = 0;
	}
}
