using UnityEngine;
using System.Collections;

public class ObstacleCollision : MonoBehaviour {
	
	private GameController gameController;
	public GameObject playerExplosion;
	
	void Start() {
		GameObject gameControllerObject = GameObject.FindWithTag ("GameController");
		if (gameControllerObject != null) {
			gameController = gameControllerObject.GetComponent<GameController>();
		} 
		if (gameController == null) {
			Debug.Log ("Cannot find 'GameController' object!!!");
		}
	}

	void OnTriggerEnter(Collider other) {
		if (other.tag == "PlaneObstacle") {
			return;
		} 
		if (other.tag == "Player") {
			gameController.setGameOver();
			Instantiate(playerExplosion, other.transform.position + Vector3.up * -1, other.transform.rotation);
			Instantiate(playerExplosion, other.transform.position + Vector3.up * 0, other.transform.rotation);
			Instantiate(playerExplosion, other.transform.position + Vector3.up * 1, other.transform.rotation);
		}
	}
}
