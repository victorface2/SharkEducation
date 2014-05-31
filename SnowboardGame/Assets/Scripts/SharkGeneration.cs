using UnityEngine;
using System.Collections;

public class SharkGeneration : MonoBehaviour {
	private GameController gameController;
	public GameObject shark;
	public int numSingleSharks;
	public int sharkDist;
	public int courseWidth;
	private bool instantiated = false;
	// Use this for initialization
	void Start () {
		GameObject gameControllerObject = GameObject.FindWithTag ("GameController");
		if (gameControllerObject != null) {
			gameController = gameControllerObject.GetComponent<GameController>();
			gameController.sharks = new GameObject[numSingleSharks];
		} 
		if (gameController == null) {
			Debug.Log ("Cannot find 'GameController' object!!!");
		}
		SpawnSharks (true);
		instantiated = true;
	}

	void Update(){
		if(instantiated && GameObject.FindWithTag ("Player").transform.position.x > gameController.lastSharkIndex){
			Debug.Log("Spawning More");
			gameController.resetSharks();
			SpawnSharks(false);
		}
	}

	void SpawnSharks(bool first) {
		int range = courseWidth - 10;
		Vector3 spawnPosition;
		if(first){
			spawnPosition = shark.transform.position;
		} else{
			spawnPosition = shark.transform.position + new Vector3(gameController.lastSharkIndex,0f,0f);
		}
		Quaternion spawnRotation = shark.transform.rotation;
		float maxX = 0.0f;
		for (int i = 1; i <= numSingleSharks; i++) {
			float zPos = Random.Range(-range, range);
			gameController.addShark((GameObject) Instantiate (shark, spawnPosition + new Vector3(i * sharkDist, 0, zPos), spawnRotation));
			maxX = spawnPosition.x + i*sharkDist;
		}
		gameController.lastSharkIndex = maxX;
		return;
	}

}