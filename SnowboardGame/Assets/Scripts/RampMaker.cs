using UnityEngine;
using System.Collections;

public class RampMaker : MonoBehaviour {
	
	private GameController gameController;
	public GameObject ramp;
	public int numSingleRamps;
	public GameObject cube;
	public int rampDist;
	public int courseWidth;
	private bool instantiated = false;
	// Use this for initialization
	void Start () {
		GameObject gameControllerObject = GameObject.FindWithTag ("GameController");
		if (gameControllerObject != null) {
			gameController = gameControllerObject.GetComponent<GameController>();
			gameController.ramps = new GameObject[numSingleRamps];
			gameController.cubes = new GameObject[numSingleRamps];
			
		} 
		if (gameController == null) {
			Debug.Log ("Cannot find 'GameController' object!!!");
		}
		SpawnRamps (true);
		instantiated = true;
	}
	
	void Update(){
		//if(instantiated && GameObject.FindWithTag ("Player").transform.position.x > gameController.lastRampIndex){
		//	Debug.Log("Spawning More");
		//gameController.resetRamps();
		//SpawnRamps(false);
		//}
		
		if(instantiated && GameObject.FindWithTag ("Player").transform.position.x > gameController.lastCubeIndex){
			gameController.resetCubes();
			gameController.resetRamps();
			SpawnRamps(false);
		}
		
	}
	
	void SpawnRamps(bool first) {
		int range = courseWidth - 10;
		Vector3 spawnPosition;
		if(first){
			spawnPosition = ramp.transform.position;
		} else{
			spawnPosition = ramp.transform.position + new Vector3(gameController.lastRampIndex,0f,0f);
		}
		Quaternion spawnRotation = ramp.transform.rotation;
		float maxX = 0.0f;
		float maxCubeX = 0.0f;
		for (int i = 1; i <= numSingleRamps; i++) {
			int zPos = Random.Range(-range, range);
			gameController.addRamp((GameObject) Instantiate (ramp, spawnPosition + new Vector3(i * rampDist, 0, zPos), spawnRotation));
			gameController.addCube((GameObject) Instantiate (cube, spawnPosition + new Vector3((i * rampDist) + 6, 3, zPos), spawnRotation));
			maxX = spawnPosition.x + i*rampDist;
			maxCubeX = spawnPosition.x + (i * rampDist) + 6;
		}
		gameController.lastRampIndex = maxX;
		gameController.lastCubeIndex = maxCubeX;
		return;
	}
	
}
