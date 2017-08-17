using UnityEngine;
using System.Collections;
using UnityEngine.Events;
using System;

namespace CardboardGestures.Gestures
{
    public class Gesture_No : AbstractGesture
    {
        public float tiempoMaximoParaReconocerGesto = 5.0f;
        bool derechaReconocida;
        bool IzquierdaReconocida;
        bool reconociendo;
        public float sensibilidad; //que tan sensible es el gesto de "no" mientras mas grande el número menos sensible. [0 - 5 mas o menos]
        float tiempoMax = 5.0f;

        public override string GestureName()
        {
            return "No";
        }

        public override bool Analyze()
        { // tiempoMax es el tiempo máximo en el que se espera reconocer el gesto a partir del primer movimiento hacia la izquierda de la cabeza.

            if (Time.time >= tiempoMaximoParaReconocerGesto)
            {
                reconociendo = false;
                IzquierdaReconocida = false;
                derechaReconocida = false;
            }

            if (Input.gyro.rotationRateUnbiased.y > sensibilidad && !IzquierdaReconocida)
                reconociendo = true;

            if (!reconociendo)
                tiempoMaximoParaReconocerGesto = Time.time + tiempoMax;
            else
            {
                if (!IzquierdaReconocida && Time.time < tiempoMaximoParaReconocerGesto)
                {
                    if (Input.gyro.rotationRateUnbiased.y > sensibilidad)// reconocí el primer movimiento de la cabeza hacia la izquierda dentro del tiempo permitido
                    {
                        reconociendo = true;
                        IzquierdaReconocida = true;
                        if (debugInfo != null)
                        {
                            debugInfo.GetComponent<TextMesh>().text = "Izquierda reconocida";
                        }
                    }
                }
                else
                {
                    if (Input.gyro.rotationRateUnbiased.y < -sensibilidad && !derechaReconocida && Time.time < tiempoMaximoParaReconocerGesto) // reconocí el primer movimiento de la cabeza hacia la derecha dentro del tiempo permitido
                    {
                        derechaReconocida = true;
                        if (debugInfo != null)
                        {
                            debugInfo.GetComponent<TextMesh>().text = "Derecha reconocida";
                        }
                    }
                    else
                    {
                        if (Input.gyro.rotationRateUnbiased.y > sensibilidad && Time.time < tiempoMaximoParaReconocerGesto && derechaReconocida)// reconocí el segundo movimiento de la cabeza hacia la izquierda dentro del tiempo permitido
                        {
                            IzquierdaReconocida = false;
                            derechaReconocida = false;
                            reconociendo = false;
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
            derechaReconocida = false;
            IzquierdaReconocida = false;
            reconociendo = true;
            sensibilidad = 5 - sensibilidad;
        }
        
    }
}