using UnityEngine;
using System;
using System.Collections;
using System.Runtime.InteropServices;

public class PlayerController : MonoBehaviour {

	[DllImport ("UniWii")]
	private static extern void wiimote_start();
	
	[DllImport ("UniWii")]
	private static extern void wiimote_stop();
	
	[DllImport ("UniWii")]
	private static extern int wiimote_count();
	
	[DllImport ("UniWii")]
	private static extern byte wiimote_getAccX(int which);
	[DllImport ("UniWii")]
	private static extern byte wiimote_getAccY(int which);
	[DllImport ("UniWii")]
	private static extern byte wiimote_getAccZ(int which);
	
	[DllImport ("UniWii")]
	private static extern float wiimote_getIrX(int which);
	[DllImport ("UniWii")]
	private static extern float wiimote_getIrY(int which);
	[DllImport ("UniWii")]
	private static extern float wiimote_getRoll(int which);
	[DllImport ("UniWii")]
	private static extern float wiimote_getPitch(int which);
	[DllImport ("UniWii")]
	private static extern float wiimote_getYaw(int which);

	public float angleAdjust;
	public float speedFactor;
	public float turnSpeedFactor;
	public float maxSpeed;
	public float baseForce;
	public int moveBackAmount;

	public bool withWiimote;
	private GameController gameController;
	private bool resetPosition;
	private String display;
	private int messageCount;


	void Start ()
	{
		wiimote_start();

		resetPosition = false;
		GameObject gameControllerObject = GameObject.FindWithTag ("GameController");
		if (gameControllerObject != null) {
			gameController = gameControllerObject.GetComponent<GameController>();
		} 
		if (gameController == null) {
			Debug.Log ("Cannot find 'GameController' object!!!");
		}
	}

	
	void Update() {
		if (Input.GetKeyDown (KeyCode.W)) {
			for (int j = 0; j < transform.GetChildCount(); j++) {
				if (transform.GetChild(j).gameObject.tag == "Whale") {
					transform.GetChild(j).gameObject.SetActive(true);
				}
				if (transform.GetChild(j).gameObject.tag == "Jim") {
					transform.GetChild(j).gameObject.SetActive(false);
				}
			}
			gameController.turnWaterOn();
		}
		if (Input.GetKeyDown (KeyCode.J)) {
			for (int j = 0; j < transform.GetChildCount(); j++) {
				if (transform.GetChild(j).gameObject.tag == "Whale") {
					transform.GetChild(j).gameObject.SetActive(false);
				}
				if (transform.GetChild(j).gameObject.tag == "Jim") {
					transform.GetChild(j).gameObject.SetActive(true);
				}
			}
			gameController.turnWaterOff();
		}
	}

	// Update is called once per frame
	void FixedUpdate () {
		if (gameController.isGameOver()) {
			rigidbody.velocity = Vector3.zero;
			if (!resetPosition) {
				gameObject.transform.position = gameObject.transform.position + new Vector3(-moveBackAmount, 0, 0);
				resetPosition = true;
			}
		} else {
			doMotion ();
		}
	}

	void doMotion() {
		int c = wiimote_count ();
		float horizontalSpeed = 0;

		if (withWiimote) {
			float moveHorizontal = 0 - Mathf.Round(wiimote_getPitch(0));
			
			Vector3 angles = new Vector3(moveHorizontal / angleAdjust, 0, 0);
			transform.eulerAngles = angles;
			horizontalSpeed = moveHorizontal * turnSpeedFactor / 180.0f;
		} else {
			float moveHorizontal = Input.GetAxis("Horizontal");
			Vector3 angles = new Vector3(moveHorizontal * 45, 0, 0);
			transform.eulerAngles = -angles;
			horizontalSpeed = -moveHorizontal * speedFactor;
		}
		rigidbody.AddForce (baseForce, 0, 0);
		
		Vector3 movement = new Vector3(0, 0, horizontalSpeed);
		transform.Translate(movement * Time.deltaTime);
		rigidbody.velocity = Vector3.ClampMagnitude(rigidbody.velocity, maxSpeed);

		rigidbody.position = new Vector3 
			(
				rigidbody.position.x, 
				rigidbody.position.y, 
				Mathf.Clamp (rigidbody.position.z, -6.0f, 6.5f)
			);
	}
}
