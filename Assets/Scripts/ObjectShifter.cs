using UnityEngine;
using System.Collections;

public class ObjectShifter : MonoBehaviour {

    public void rotar() {

    }

    public void openDoor() {
        transform.position = new Vector3(0, -50, 0);
    }

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
        //transform.Rotate(90, 0, 0);
    }
}
