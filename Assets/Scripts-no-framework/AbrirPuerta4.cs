using UnityEngine;
using System.Collections;

public class AbrirPuerta4 : MonoBehaviour
{
	public GameObject user;
	public float range = 2f;
	public float TurnVelocity = 2.0f;
	bool doorMoved = false;

	void Start ()
	{
		Input.gyro.enabled = true;
	}

	void Update ()
	{
		if (!doorMoved) {
			float distanceX = user.transform.position.x - gameObject.transform.position.x;
			float distanceY = user.transform.position.y - gameObject.transform.position.y;
			float distanceZ = user.transform.position.z - gameObject.transform.position.z;
			if (Mathf.Abs (distanceX) <= range 
				&& Mathf.Abs (distanceY) <= range && Mathf.Abs (distanceZ) <= range) {
				if (Input.gyro.rotationRateUnbiased.y < -TurnVelocity)
					gameObject.transform.position = new Vector3 (gameObject.transform.position.x,
						gameObject.transform.position.y - 100, gameObject.transform.position.z);
				doorMoved = true;
			}
		}
	}
}
