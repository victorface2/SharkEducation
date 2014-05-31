using UnityEngine;
using System.Collections;

public class SharkMover : MonoBehaviour {

	public float minZ, maxZ;
	public float resetDistance;
	public float startVelocity;

	void Start() {
		if (Random.Range (0, 2) == 0) {
			rigidbody.velocity = new Vector3(0, 0, startVelocity);
		} else {
			rigidbody.velocity = new Vector3(0, 0, -startVelocity);
			transform.Rotate(0, 180, 0);
		}
	}

	void FixedUpdate() {
		if (transform.position.z >= maxZ) {
			transform.position = transform.position + Vector3.back * resetDistance;
			rigidbody.velocity = new Vector3(0, 0, -startVelocity);
			transform.Rotate(0, 180, 0);

		} else if (transform.position.z <= minZ) {
			transform.position = transform.position + Vector3.forward * resetDistance;
			rigidbody.velocity = new Vector3(0, 0, startVelocity);
			transform.Rotate(0, 180, 0);

		} 
	}
}
