using UnityEngine;

namespace CardboardGestures.Gestures
{
    public class Gesture_LookUp : AbstractGesture
    {
		public float inclination = 45f;

		private float sensibility = 0.7f;

        public DeviceOrientation orientation;

        private Vector3 initialVector;

        public override string GestureName()
        {
            return "Look Up";
        }

        public override bool Analyze()
        {
            if ((orientation == DeviceOrientation.LandscapeLeft || orientation == DeviceOrientation.LandscapeRight)
                && initialVector.z + sensibility <= Input.acceleration.z)
            {
                return true;
            }
            return false;
        }

        public void Start()
        {
            initialVector = new Vector3(0.0f, -1.0f, 0.0f);
			sensibility = convertDegreesToSensibility (inclination);
        }
		private float convertDegreesToSensibility(float inclinationDegrees){
			return inclinationDegrees / 90f;
		}   
                
    }
}