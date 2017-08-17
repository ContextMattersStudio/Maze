using UnityEngine;
using System.Collections;
using UnityEngine.Events;
using System;

namespace CardboardGestures.Gestures
{
    public class Gesture_Yes : AbstractGesture
    {
        public float tiempoMaximoParaReconocerGesto = 5.0f;
        bool arribaReconocido = false;
        bool abajoReconocido = false;
        bool reconociendo;
        public float sensibilidad; //que tan sensible es el gesto de "Yes" mientras mas grande el número menos sensible. [0 - 5 mas o menos]
        float tiempoMax = 5.0f;
        

        public override string GestureName()
        {
            return "Yes";
        }

        public override bool Analyze()
        { // tiempoMax es el tiempo máximo en el que se espera reconocer el gesto a partir del primer movimiento hacia la izquierda de la cabeza.

            if (Time.time >= tiempoMaximoParaReconocerGesto)
            {
                reconociendo = false;
                arribaReconocido = false;
                abajoReconocido = false;
            }

            if (Input.gyro.rotationRateUnbiased.x > sensibilidad && !arribaReconocido)
                reconociendo = true;

            if (!reconociendo)
                tiempoMaximoParaReconocerGesto = Time.time + tiempoMax;
            else
            {
                if (!arribaReconocido && Time.time < tiempoMaximoParaReconocerGesto)
                {
                    if (Input.gyro.rotationRateUnbiased.x > sensibilidad)// reconocí el primer movimiento de la cabeza hacia arriba dentro del tiempo permitido
                    {
                        reconociendo = true;
                        arribaReconocido = true;
                        if (debugInfo!= null) { 
                            debugInfo.GetComponent<TextMesh>().text = "arriba Reconocido";
                        }
                    }
                }
                else
                    if (Input.gyro.rotationRateUnbiased.x < -sensibilidad && !abajoReconocido && Time.time < tiempoMaximoParaReconocerGesto) // reconocí el primer movimiento de la cabeza hacia abajo dentro del tiempo permitido
                    {
                        abajoReconocido = true;
                        debugInfo.GetComponent<TextMesh>().text = "abajo Reconocido";
                    }
                    else
                        if (Input.gyro.rotationRateUnbiased.x > sensibilidad && Time.time < tiempoMaximoParaReconocerGesto && abajoReconocido)// reconocí el segundo movimiento de la cabeza hacia arriba dentro del tiempo permitido
                        {
                            arribaReconocido = false;
                            abajoReconocido = false;
                            reconociendo = false;
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
            reconociendo = true;
			sensibilidad = 5 - sensibilidad;
            arribaReconocido = false;
            abajoReconocido = false;

        }
    }
}