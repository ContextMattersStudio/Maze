using UnityEngine;
using System.Collections;
using UnityEngine.Events;
using System;

namespace CardboardGestures.Gestures
{
    public class Gesture_Yes : AbstractGesture
    {
        public float maxTimeToRecognizeGesture = 5.0f;
		bool upMovementRecognized = false;
		bool downMovementRecognized = false;
		bool recognizing;
        public float gestureMovementSpeed;
		float maxTime = 5.0f;
        

        public override string GestureName()
        {
            return "Yes";
        }

        public override bool Analyze()
        { // tiempoMax es el tiempo máximo en el que se espera reconocer el gesto a partir del primer movimiento hacia la izquierda de la cabeza.

            if (Time.time >= maxTimeToRecognizeGesture)
            {
                recognizing = false;
                upMovementRecognized = false;
                downMovementRecognized = false;
            }

            if (Input.gyro.rotationRateUnbiased.x > gestureMovementSpeed && !upMovementRecognized)
                recognizing = true;

            if (!recognizing)
                maxTimeToRecognizeGesture = Time.time + maxTime;
            else
            {
                if (!upMovementRecognized && Time.time < maxTimeToRecognizeGesture)
                {
                    if (Input.gyro.rotationRateUnbiased.x > gestureMovementSpeed)// reconocí el primer movimiento de la cabeza hacia arriba dentro del tiempo permitido
                    {
                        recognizing = true;
                        upMovementRecognized = true;
                        if (debugInfo!= null) { 
                            debugInfo.GetComponent<TextMesh>().text = "arriba Reconocido";
                        }
                    }
                }
                else
                    if (Input.gyro.rotationRateUnbiased.x < -gestureMovementSpeed && !downMovementRecognized && Time.time < maxTimeToRecognizeGesture) // reconocí el primer movimiento de la cabeza hacia abajo dentro del tiempo permitido
                    {
                        downMovementRecognized = true;
                        debugInfo.GetComponent<TextMesh>().text = "abajo Reconocido";
                    }
                    else
                        if (Input.gyro.rotationRateUnbiased.x > gestureMovementSpeed && Time.time < maxTimeToRecognizeGesture && downMovementRecognized)// reconocí el segundo movimiento de la cabeza hacia arriba dentro del tiempo permitido
                        {
                            upMovementRecognized = false;
                            downMovementRecognized = false;
                            recognizing = false;
                            if (debugInfo != null) { 
                                debugInfo.GetComponent<TextMesh>().text = "Yes Reconocido";
                            }
                //StartCoroutine(Esperar(5.0f));
                return true;
                        }

            }
            return false;
        }

        void Start()
        {

            Input.gyro.enabled = true;
            recognizing = true;
            upMovementRecognized = false;
            downMovementRecognized = false;

        }
    }
}