using UnityEngine;
using System.Collections;
using System;

namespace CardboardGestures.Gestures
{
    public class Gesture_AND : AbstractGesture
    {

        public AbstractGesture gesture1;
        public AbstractGesture gesture2;
        bool gesture1Detected;
        bool gesture2Detected;
        float timeGesture1;
        float timeGesture;
        public float timeBetweenGestures;
        [Header("Activate if you want in order execution")]
        public bool inOrder;

        void Start()
        {

        }

        public override string GestureName()
        {
            return "AND";
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
            if (inOrder)
            {
                if (gesture1.isDetected())
                {
                    gesture1Detected = true;
                    timeGesture1 = Time.time;
                    
                }
                if (gesture1Detected)
                {
                    if (Time.time - timeGesture1 >= timeBetweenGestures)
                    {
                        gesture1Detected = false;
                    }
                    else
                    {
                        if (gesture2.isDetected())
                        {
                            return true;
                        }
                    }
                }
            }
            else
            {
                if (gesture1.isDetected())
                {
                    gesture1Detected = true;
                    if (!gesture2Detected)
                    {
                        timeGesture = Time.time;
                    }
                    else
                    {
                        return true;
                    }

                }
                if(gesture2.isDetected())
                {
                    gesture2Detected = true;
                    if (!gesture1Detected)
                    {
                        timeGesture = Time.time;
                    }
                    else
                    {
                        return true;
                    }
                }
                if (gesture1Detected)
                {
                    if (Time.time - timeGesture >= timeBetweenGestures)
                    {
                        gesture1Detected = false;
                    }
                }
                if (gesture2Detected)
                {
                    if (Time.time - timeGesture >= timeBetweenGestures)
                    {
                        gesture2Detected = false;
                    }
                }
            }
            return false;
        }
    }
}