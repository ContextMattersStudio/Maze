using UnityEngine;
using System.Collections;
using System;

namespace CardboardGestures.Gestures
{
    public class Gesture_OR : AbstractGesture
    {

        public AbstractGesture gesture1;
        public AbstractGesture gesture2;

        void Start()
        {

        }

        public override string GestureName()
        {
            return "OR";
        }

        public override bool Analyze()
        {
            if (debugInfo != null)
            {
                debugInfo.GetComponent<TextMesh>().text = "gesture1: " + gesture1.isDetected() + ",gesture2: " + gesture2.isDetected();
            }
			if (gesture1 == null || gesture2 == null)
			{
				return false;
			}

            if (gesture1.isDetected() || gesture2.isDetected())
            {
                return true;
            }
                
            return false;
        }
    }
}