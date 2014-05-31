﻿using UnityEngine;
using System;
using System.Collections;
using System.Runtime.InteropServices;

public class WithWiimote : MonoBehaviour {

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

	private GameController gameController;
	private bool isWiimote;
	private bool resetPosition;

	void Start ()
	{
		if (wiimote_count() > 0) {
			wiimote_start();
			isWiimote = true;
		} else {
			isWiimote = false;
		}
		resetPosition = false;
		GameObject gameControllerObject = GameObject.FindWithTag ("GameController");
		if (gameControllerObject != null) {
			gameController = gameControllerObject.GetComponent<GameController>();
		} 
		if (gameController == null) {
			Debug.Log ("Cannot find 'GameController' object!!!");
		}
	}

	// Update is called once per frame
	void FixedUpdate () {
		if (gameController.isGameOver()) {
			rigidbody.velocity = Vector3.zero;
			if (!resetPosition) {
				gameObject.transform.position = gameObject.transform.position + new Vector3(-10, 0, 0);
				resetPosition = true;
			}
		} else {
			doMotion ();
		}
	}

	void doMotion() {
		int c = wiimote_count ();
		float horizontalSpeed = 0;
		
		if (isWiimote && c > 0) {
			Debug.Log ("Wiimote Connected!!");
			
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
	}
}
