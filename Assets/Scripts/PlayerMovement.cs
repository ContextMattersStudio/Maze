using UnityEngine;
using System.Collections;

public class PlayerMovement : MonoBehaviour {

	public float movSpeed = 2.0f;

	public GameObject camera;

	private Vector3 forward;

	private Vector3 backward;

	private bool colliding = false;

	void FixedUpdate()
	{
		//transform.Translate(Camera.main.transform.forward * thrust * Time.deltaTime );
		//transform.position += this.GetComponent<Main Camera>().transform.forward * thrust * Time.deltaTime;

		//transform.position += camera.transform.forward * movSpeed * Time.deltaTime;

		/*
		Vector3 forward = Camera.main.transform.forward;
		forward.y = 0;  // zero out y, leaving only x & z
		transform.position += forward * Time.deltaTime * moveSpeed;
		*/
		foreach (Touch touch in Input.touches)
			if (touch.phase == TouchPhase.Stationary)
				if (!colliding){
				//(Input.touchCount >= 0)
				forward = camera.transform.forward;
				forward.y = 0;
				transform.position += forward * Time.deltaTime * movSpeed;
				}

	}

	void OnCollitionEnter(Collider col){
			
		colliding = true;

	}
	/*void OnCollitionStay(Collider col){
	
	}*/

	void OnCollitionExit(Collider col){

		colliding = false;

	}


	void OnTriggerStay(Collider other){

		if (other.attachedRigidbody){
			backward = camera.transform.forward;
			backward.y = 0;
			transform.position -= backward * Time.deltaTime * movSpeed;
			//camera.GetComponent<Collider>().attachedRigidbody.AddForce (Vector3.back * 10);
		}
	}




}
