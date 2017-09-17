using UnityEngine;
using System.Collections;
using UnityEngine.Events;
using System;

namespace CardboardGestures.Gestures
{
    public class Gesture_No : AbstractGesture
    {
        public float maxTimeToRecognizeGesture = 5.0f;
		bool rightMoveRecognized;
		bool leftMoveRecognized;
		bool recognizing;
        public float gestureMovementSpeed; //que tan sensible es el gesto de "no" mientras mas grande el número menos sensible. [0 - 5 mas o menos]
		float maxTime = 5.0f;

        public override string GestureName()
        {
            return "No";
        }

        public override bool Analyze()
        { // tiempoMax es el tiempo máximo en el que se espera reconocer el gesto a partir del primer movimiento hacia la izquierda de la cabeza.

            if (Time.time >= maxTimeToRecognizeGesture)
            {
                recognizing = false;
                leftMoveRecognized = false;
                rightMoveRecognized = false;
            }

            if (Input.gyro.rotationRateUnbiased.y > gestureMovementSpeed && !leftMoveRecognized)
                recognizing = true;

            if (!recognizing)
                maxTimeToRecognizeGesture = Time.time + maxTime;
            else
            {
                if (!leftMoveRecognized && Time.time < maxTimeToRecognizeGesture)
                {
                    if (Input.gyro.rotationRateUnbiased.y > gestureMovementSpeed)// reconocí el primer movimiento de la cabeza hacia la izquierda dentro del tiempo permitido
                    {
                        recognizing = true;
                        leftMoveRecognized = true;
                        if (debugInfo != null)
                        {
                            debugInfo.GetComponent<TextMesh>().text = "Izquierda reconocida";
                        }
                    }
                }
                else
                {
                    if (Input.gyro.rotationRateUnbiased.y < -gestureMovementSpeed && !rightMoveRecognized && Time.time < maxTimeToRecognizeGesture) // reconocí el primer movimiento de la cabeza hacia la derecha dentro del tiempo permitido
                    {
                        rightMoveRecognized = true;
                        if (debugInfo != null)
                        {
                            debugInfo.GetComponent<TextMesh>().text = "Derecha reconocida";
                        }
                    }
                    else
                    {
                        if (Input.gyro.rotationRateUnbiased.y > gestureMovementSpeed && Time.time < maxTimeToRecognizeGesture && rightMoveRecognized)// reconocí el segundo movimiento de la cabeza hacia la izquierda dentro del tiempo permitido
                        {
                            leftMoveRecognized = false;
                            rightMoveRecognized = false;
                            recognizing = false;
                            if (debugInfo != null)
                            {
                                debugInfo.GetComponent<TextMesh>().text = "No Reconocido";
                            }
                            return true;
                        }
                    }
                }

            }
            return false;
        }

        public void Start()
        {
            Input.gyro.enabled = true;
            rightMoveRecognized = false;
            leftMoveRecognized = false;
            recognizing = true;
            gestureMovementSpeed = 5 - gestureMovementSpeed;
        }
        
    }
}