using UnityEngine;
using System.Collections;
using UnityEditor;

namespace CardboardGestures.Conditions
{
    [CustomEditor(typeof(Condition_NearCube))]
    public class Condition_NearCubeEditor : Editor
    {
        public override void OnInspectorGUI()
        {
            Condition_NearCube myScript = (Condition_NearCube)target;

            myScript.objeto1 = (GameObject)EditorGUILayout.ObjectField("Objecto 1 (Movil)", myScript.objeto1, typeof(GameObject));

            myScript.showZonaCubica = EditorGUILayout.Toggle("Mostrar zonaCubica", myScript.showZonaCubica);
            myScript.posicion = EditorGUILayout.Vector3Field("Posición del zonaCubica", myScript.posicion);
            myScript.lado = EditorGUILayout.FloatField("Longitud de cada cara", myScript.lado);

            if (myScript.showZonaCubica)
            {
                if (myScript.zonaCubica == null)
                {
                    myScript.zonaCubica = GameObject.CreatePrimitive(PrimitiveType.Cube);
                    //myScript.zonaCubica.GetComponent<MeshRenderer>().materials = new Material[0];
                    myScript.zonaCubica.name = "Range cube";
                    myScript.zonaCubica.transform.position = myScript.posicion;
                    Color c = Color.yellow;
					c.a = 0.3f;
                    myScript.zonaCubica.GetComponent<Renderer>().material.color = c;
                    myScript.zonaCubica.transform.localScale = new Vector3(myScript.lado, myScript.lado, myScript.lado);
                }

                if (myScript.lado != myScript.oldLado)
                {
                    myScript.oldLado = myScript.lado;
                    myScript.zonaCubica.transform.position = myScript.posicion;
                    myScript.zonaCubica.transform.localScale = new Vector3(myScript.lado, myScript.lado, myScript.lado);
                }
            }
            else
            {
                GameObject.DestroyImmediate(myScript.zonaCubica);
                myScript.zonaCubica = null;
            }
        }
    }

}