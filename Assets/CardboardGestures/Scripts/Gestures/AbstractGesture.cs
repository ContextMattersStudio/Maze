using UnityEngine;
using System.Collections;
using UnityEngine.Events;
namespace CardboardGestures.Gestures
{
    public abstract class AbstractGesture : MonoBehaviour
    {
        public float timeToPersistTheGesture;
        public GameObject debugInfo;
        protected bool detected = false; // este booleano significa si el gesto que quería reconocer fue detectado (true) o no (false)
        protected float lastTime;

        public abstract string GestureName();

        public abstract bool Analyze();

        public bool isDetected()
        {
            return detected;
        }

        void Update()
        {
            if (!detected)
            {
                if (Analyze())
                {
                    detected = true;
                    lastTime = Time.time;
                    if (debugInfo != null)
                    {
                        debugInfo.GetComponent<TextMesh>().text = "Gesto " + GestureName() + " detectado";
                    }
                }
            }
            else
            {
                if (Time.time - lastTime >= timeToPersistTheGesture)
                {
                    detected = false;
                    if (debugInfo != null)
                    {
                        debugInfo.GetComponent<TextMesh>().text = "";
                    }
                }
            }
        }
    }
}