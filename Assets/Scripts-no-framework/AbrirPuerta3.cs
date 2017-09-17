using UnityEngine;
using System.Collections;

public class AbrirPuerta3 : MonoBehaviour {
	public GameObject user;
	public float range = 2f;
	bool doorMoved = false;
	bool recognizing = false;
	public DeviceOrientation orientation;
	[Header("Max time to recognize the gesture")]
	public float maxTime= 5f;
	float maxTimeToRecognizeGesture=5f;
	bool insideZone;

	private Vector3 initialVector;
	bool reconociTiltLeft = false;
	[Header("Gesture advanced settings")]
	public float inclinationTiltLeft;
	public float inclinationLookDown;

	void Start () {
		Input.gyro.enabled = true;
		recognizing = true;
		initialVector = new Vector3(0.0f, -1.0f, 0.0f);
		insideZone = false;
		inclinationLookDown = convertDegreesToSensibility (inclinationLookDown);
		inclinationTiltLeft = convertDegreesToSensibility (inclinationTiltLeft);
	}

	void Update(){
		if (!doorMoved) {
			float distanceX = user.transform.position.x - gameObject.transform.position.x;
			float distanceY = user.transform.position.y - gameObject.transform.position.y;
			float distanceZ = user.transform.position.z - gameObject.transform.position.z;
			if (Mathf.Abs (distanceX) <= range &&
				Mathf.Abs (distanceY) <= range && Mathf.Abs (distanceZ) <= range) {
				if (!insideZone) {
					user.GetComponent<TextMesh> ().text = "INSIDE ZONE2";
					insideZone = true;
				}
				if (Time.time >= maxTimeToRecognizeGesture) {
					recognizing = false;
					reconociTiltLeft = false;
				}

				if ((orientation == DeviceOrientation.LandscapeLeft
					&& initialVector.x - inclinationTiltLeft > Input.acceleration.x)
					|| (orientation == DeviceOrientation.LandscapeRight
						&& initialVector.x + inclinationTiltLeft > Input.acceleration.x)) {
					recognizing = true;
					reconociTiltLeft = true;
				}

				if (!recognizing)
					maxTimeToRecognizeGesture = Time.time + maxTime;
				else {
					if (reconociTiltLeft) {
						if ((orientation == DeviceOrientation.LandscapeLeft ||
							orientation == DeviceOrientation.LandscapeRight)
							&& initialVector.z - inclinationLookDown >= Input.acceleration.z) {
							doorMoved = true;
							gameObject.transform.position = new Vector3 (gameObject.transform.position.x,
								gameObject.transform.position.y - 100, gameObject.transform.position.z);
						}
					}
				}
			}
		}
	}
	private float convertDegreesToSensibility(float inclinationDegrees){
		return inclinationDegrees / 90f;
	}
}
