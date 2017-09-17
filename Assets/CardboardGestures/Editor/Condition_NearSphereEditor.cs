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
            myScript.object1 = (GameObject)EditorGUILayout.ObjectField("Object 1 (Mobile)", myScript.object1, typeof(GameObject));

            myScript.object2 = (GameObject)EditorGUILayout.ObjectField("Object 2 (Static)", myScript.object2, typeof(GameObject));

            myScript.range = EditorGUILayout.FloatField("Range", myScript.range);

			myScript.showSphericZone = EditorGUILayout.Toggle("Show spheric zone (Range)", myScript.showSphericZone);

            if (myScript.showSphericZone)
            {
                if (myScript.object2 != null && myScript.SphericZone == null)
                {
                    myScript.SphericZone = GameObject.CreatePrimitive(PrimitiveType.Sphere);
                    myScript.SphericZone.name = "Range sphere";
                    myScript.SphericZone.transform.position = myScript.object2.transform.position;
                    
                    myScript.SphericZone.transform.localScale = new Vector3(myScript.range * 2, myScript.range * 2, myScript.range * 2);
                    //myScript.zonaEsferica.GetComponent<Renderer>().material.shader = Shader.Find("Unlit/Transparent");
                    Color c = Color.yellow;
                    c.a = 0.3f;
                    myScript.SphericZone.GetComponent<Renderer>().material.color = c;
                    myScript.SphericZone.GetComponent<Renderer>().shadowCastingMode = UnityEngine.Rendering.ShadowCastingMode.Off;
                }
                if (myScript.range != myScript.oldCubeSide)
                {
                    myScript.oldCubeSide = myScript.range;
                    myScript.SphericZone.transform.position = myScript.object2.transform.position;
                    myScript.SphericZone.transform.localScale = new Vector3(myScript.range * 2, myScript.range * 2, myScript.range * 2);
                }
            }
            else
            {
                GameObject.DestroyImmediate(myScript.SphericZone);
                myScript.SphericZone = null;
            }
        }
    }
}