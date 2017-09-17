using UnityEngine;
using System.Collections;

public class AbrirPuerta1 : MonoBehaviour {

	public GameObject user;

	public float range = 2f;
	public float inclination = 45f;
	bool doorMoved = false;

	// Use this for initialization
	void Start () {
		inclination = convertDegreesToSensibility (inclination);
	}
	
	// Update is called once per frame
	void Update () {
		if (!doorMoved) {
			float distanceX = user.transform.position.x - gameObject.transform.position.x;
			float distanceY = user.transform.position.y - gameObject.transform.position.y;
			float distanceZ = user.transform.position.z - gameObject.transform.position.z;
			if (Mathf.Abs (distanceX) <= range 
				&& Mathf.Abs (distanceY) <= range 
				&& Mathf.Abs (distanceZ) <= range) {
				if (- inclination >= Input.acceleration.z) {
					gameObject.transform.position = new Vector3 (gameObject.transform.position.x,
				         	gameObject.transform.position.y - 100, gameObject.transform.position.z);
					doorMoved = true;
				}
			}
		}
	}

	private float convertDegreesToSensibility(float inclinationDegrees){
		return inclinationDegrees / 90f;
	}
}

