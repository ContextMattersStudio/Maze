using UnityEngine;
using System.Collections;

public class AbrirPuerta3 : MonoBehaviour {
	public GameObject usuario;
	public float rango = 2f;
	bool seMovio = false;
	bool reconociendo = false;
	public DeviceOrientation orientation;
	[Header("Tiempo máximo para reconocer el gesto")]
	public float tiempoMax= 5f;
	float tiempoMaximoParaReconocerGesto=5f;
	bool insideZone;

	private Vector3 initialVector;
	bool reconociTiltLeft = false;
	[Header("Configuración sensibilidades")]
	public float inclinacionTiltLeft;
	public float sensibilidadLookDown;


	// Use this for initialization
	void Start () {
		Input.gyro.enabled = true;
		reconociendo = true;
		initialVector = new Vector3(0.0f, -1.0f, 0.0f);
		insideZone = false;
	}

	void Update(){
		if (!seMovio) {
			float distanceX = usuario.transform.position.x - gameObject.transform.position.x;
			float distanceY = usuario.transform.position.y - gameObject.transform.position.y;
			float distanceZ = usuario.transform.position.z - gameObject.transform.position.z;
			if (Mathf.Abs (distanceX) <= rango &&
				Mathf.Abs (distanceY) <= rango && Mathf.Abs (distanceZ) <= rango) {
				if (!insideZone) {
					usuario.GetComponent<TextMesh> ().text = "INSIDE ZONE2";
					insideZone = true;
				}
				if (Time.time >= tiempoMaximoParaReconocerGesto) {
					reconociendo = false;
					reconociTiltLeft = false;
				}

				if ((orientation == DeviceOrientation.LandscapeLeft
					&& initialVector.x - inclinacionTiltLeft > Input.acceleration.x)
					||
					(orientation == DeviceOrientation.LandscapeRight
						&& initialVector.x + inclinacionTiltLeft > Input.acceleration.x)) {
					reconociendo = true;
					reconociTiltLeft = true;
				}

				if (!reconociendo)
					tiempoMaximoParaReconocerGesto = Time.time + tiempoMax;
				else {
					if (reconociTiltLeft) {
						if ((orientation == DeviceOrientation.LandscapeLeft ||
							orientation == DeviceOrientation.LandscapeRight)
						   && initialVector.z - sensibilidadLookDown >= Input.acceleration.z) {
							seMovio = true;
							gameObject.transform.position = new Vector3 (gameObject.transform.position.x,
								gameObject.transform.position.y - 100, gameObject.transform.position.z);
						}
					}
				}
			}
		}
	}
}
