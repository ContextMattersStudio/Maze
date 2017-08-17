using UnityEngine;
using System.Collections;
using UnityEditor;

namespace CardboardGestures.Conditions
{
    [CustomEditor(typeof(Condition_NearSphere))]
    public class Condition_NearSphereEditor : Editor
    {
        public override void OnInspectorGUI()
        {
            Condition_NearSphere myScript = (Condition_NearSphere)target;
            myScript.objeto1 = (GameObject)EditorGUILayout.ObjectField("Objecto 1 (Movil)", myScript.objeto1, typeof(GameObject));

            myScript.objeto2 = (GameObject)EditorGUILayout.ObjectField("Objecto 2 (Inmovil)", myScript.objeto2, typeof(GameObject));

            myScript.range = EditorGUILayout.FloatField("Rango", myScript.range);

			myScript.showZonaEsferica = EditorGUILayout.Toggle("Mostrar zona esferica (Rango)", myScript.showZonaEsferica);

            if (myScript.showZonaEsferica)
            {
                if (myScript.objeto2 != null && myScript.zonaEsferica == null)
                {
                    myScript.zonaEsferica = GameObject.CreatePrimitive(PrimitiveType.Sphere);
                    myScript.zonaEsferica.name = "Range sphere";
                    myScript.zonaEsferica.transform.position = myScript.objeto2.transform.position;
                    
                    myScript.zonaEsferica.transform.localScale = new Vector3(myScript.range * 2, myScript.range * 2, myScript.range * 2);
                    //myScript.zonaEsferica.GetComponent<Renderer>().material.shader = Shader.Find("Unlit/Transparent");
                    Color c = Color.yellow;
                    c.a = 0.3f;
                    myScript.zonaEsferica.GetComponent<Renderer>().material.color = c;
                    myScript.zonaEsferica.GetComponent<Renderer>().shadowCastingMode = UnityEngine.Rendering.ShadowCastingMode.Off;
                }
                if (myScript.range != myScript.oldRange)
                {
                    myScript.oldRange = myScript.range;
                    myScript.zonaEsferica.transform.position = myScript.objeto2.transform.position;
                    myScript.zonaEsferica.transform.localScale = new Vector3(myScript.range * 2, myScript.range * 2, myScript.range * 2);
                }
            }
            else
            {
                GameObject.DestroyImmediate(myScript.zonaEsferica);
                myScript.zonaEsferica = null;
            }
        }
    }
}