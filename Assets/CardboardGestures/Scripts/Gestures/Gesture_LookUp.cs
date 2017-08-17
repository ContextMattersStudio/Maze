using UnityEngine;

namespace CardboardGestures.Gestures
{
    public class Gesture_LookUp : AbstractGesture
    {
		public float inclinacion = 45f;

		private float sensibilidad = 0.7f;

        public DeviceOrientation orientation;

        private Vector3 initialVector;

        public override string GestureName()
        {
            return "Look Up";
        }

        public override bool Analyze()
        {
            if ((orientation == DeviceOrientation.LandscapeLeft || orientation == DeviceOrientation.LandscapeRight)
                && initialVector.z + sensibilidad <= Input.acceleration.z)
            {
                return true;
            }
            return false;
        }

        public void Start()
        {
            initialVector = new Vector3(0.0f, -1.0f, 0.0f);
			sensibilidad = convertDegreesToSensibility (inclinacion);
        }
		private float convertDegreesToSensibility(float inclinationDegrees){
			return inclinationDegrees / 90f;
		}   
                
    }
}