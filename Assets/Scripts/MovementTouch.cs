using UnityEngine;
using System.Collections;

public class MovementTouch : MonoBehaviour
{
    int i = 1;
    // Use this for initialization

    Vector3 checkPos;
    float radius = 10.0f;

    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        MoveForward();
    }

    public void MoveForward()
    {
        foreach (Touch touch in Input.touches)
            if (touch.phase == TouchPhase.Stationary)
            //(Input.touchCount >= 0)
            {

                checkPos = new Vector3(transform.position.x, transform.position.y, transform.position.z - 0.3f);
                if (Physics.CheckSphere(checkPos, radius))
                {
                    // found something
                    transform.position = checkPos;
                }

            }
    }
   
}