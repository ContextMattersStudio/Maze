using UnityEngine;
using System.Collections;

public class AbrirPuerta2 : MonoBehaviour {
	public GameObject user;
	public float range = 2f;
	public float speed = 0.7f;
	bool doorMoved = false;
	bool movementUpRecognized = false;
	bool movementDownRecognized = false;
	bool recognizing = false;
	[Header("Max time to recognize a gesture")]
	public float maxTime= 5f;
	float maxTimeToRecognizeGesture=5f;
	bool insideZone = false;

	void Start () {
		Input.gyro.enabled = true;
		recognizing = true;
		movementUpRecognized = false;
		movementDownRecognized = false;
	}

	void Update(){
		if (!doorMoved) {
			float distanceX = user.transform.position.x - gameObject.transform.position.x;
			float distanceY = user.transform.position.y - gameObject.transform.position.y;
			float distanceZ = user.transform.position.z - gameObject.transform.position.z;
			if (Mathf.Abs (distanceX) <= range && Mathf.Abs (distanceY) <= range &&
				Mathf.Abs (distanceZ) <= range) {
				if (!insideZone) 
				{
					user.GetComponent<TextMesh> ().text = "INSIDE ZONE";
					insideZone = true;
				}
				if (Time.time >= maxTimeToRecognizeGesture) {
					recognizing = false;
					movementUpRecognized = false;
					movementDownRecognized = false;
				}

				if (Input.gyro.rotationRateUnbiased.x > speed && !movementUpRecognized)
					recognizing = true;

				if (!recognizing)
					maxTimeToRecognizeGesture = Time.time + maxTime;
				else {
					if (!movementUpRecognized && Time.time < maxTimeToRecognizeGesture) {
						if (Input.gyro.rotationRateUnbiased.x > speed) {
	           // reconocí el primer movimiento de la cabeza hacia arriba dentro del tiempo permitido
							recognizing = true;
							movementUpRecognized = true;
							if (user.GetComponent<TextMesh> ().text != null) { 
								user.GetComponent<TextMesh> ().text = "arriba Reconocido";
							}
						}
					} else if (Input.gyro.rotationRateUnbiased.x < -speed &&
						!movementDownRecognized && Time.time < maxTimeToRecognizeGesture) { 
		      // reconocí el primer movimiento de la cabeza hacia abajo dentro del tiempo permitido
						movementDownRecognized = true;
						user.GetComponent<TextMesh> ().text = "abajo Reconocido";
					} else if (Input.gyro.rotationRateUnbiased.x > speed &&
						Time.time < maxTimeToRecognizeGesture && movementDownRecognized) {
			  // reconocí el segundo movimiento de la cabeza hacia arriba dentro del tiempo permitido
						movementUpRecognized = false;
						movementDownRecognized = false;
						recognizing = false;
						if (user.GetComponent<TextMesh> ().text != null) { 
							user.GetComponent<TextMesh> ().text = "Yes Reconocido";
						}
						gameObject.transform.position = new Vector3 (gameObject.transform.position.x,
							gameObject.transform.position.y - 100, gameObject.transform.position.z);
						doorMoved = true;
					}

				}
			}
		}
	}
}
