using System.Collections;
using UnityEngine;
using UnityEngine.Events;
using CardboardGestures.Gestures;
using CardboardGestures.Conditions;

namespace CardboardGestures
{
    public class GestureListener : MonoBehaviour
    {

        public AbstractCondition condition;
        public AbstractGesture gesture;
        public GameObject debugInfo;
        public UnityEvent action;
		public float timeBetweenInvokes;
		float currentTime;

		void Start()
		{
			currentTime = Time.time;
		}


        void Update()
        {
            if (debugInfo != null)
            {
				debugInfo.GetComponent<TextMesh>().text = "Condition:" + condition.satisfied() + ", Gesture: " + gesture.isDetected();
            }
            if (condition.satisfied())
            {
				if (gesture.isDetected() && Time.time - currentTime > timeBetweenInvokes)
                {
                    action.Invoke();
					currentTime = Time.time;
				}
            }
        }

    }
}