using UnityEngine;
using System.Collections;

namespace CardboardGestures.Gestures
{
    public class Gesture_LookRight : AbstractGesture
    {

        public float turnSpeed = 2.0f; //que tan sensible es el gesto de "Mirar Derecha" mientras mas grande el número menos sensible. [0 - 5 mas o menos]


        public void Start()
        {
            Input.gyro.enabled = true;
        }

        public override string GestureName()
        {
            return "LookRight";
        }

        public override bool Analyze()
        {
			if (Input.gyro.rotationRateUnbiased.y < -turnSpeed) // reconocí el movimiento de la cabeza hacia la derecha

                return true;
            return false;
        }
        
    }
}