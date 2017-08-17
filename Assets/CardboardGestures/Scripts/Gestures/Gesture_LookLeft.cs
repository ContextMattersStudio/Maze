using UnityEngine;
using System.Collections;

namespace CardboardGestures.Gestures
{
    public class Gesture_LookLeft : AbstractGesture
    {
        
		public float velocidadDeGiro = 2.0f; //que tan sensible es el gesto de "mirar izquierda" mientras mas grande el número menos sensible. [0 - 5 mas o menos]


        public void Start()
        {
            Input.gyro.enabled = true;
        }

        public override string GestureName()
        {
            return "LookLeft";
        }

        public override bool Analyze()
        { 
			if (Input.gyro.rotationRateUnbiased.y > velocidadDeGiro)// reconocí el movimiento de la cabeza hacia la izquierda
            {
                return true;
            }
            return false;
        }
                
    }
}