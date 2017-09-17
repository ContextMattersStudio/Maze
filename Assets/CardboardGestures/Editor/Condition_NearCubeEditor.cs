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

			myScript.object1 = (GameObject)EditorGUILayout.ObjectField("Object 1 (Mobile)", myScript.object1, typeof(GameObject));

			myScript.showCubicZone = EditorGUILayout.Toggle("Show cubic zone", myScript.showCubicZone);
			myScript.positionCube = EditorGUILayout.Vector3Field("Position of cubic zone", myScript.positionCube);
			myScript.cubeSide = EditorGUILayout.FloatField("Cube side longitude", myScript.cubeSide);

			if (myScript.showCubicZone)
            {
				if (myScript.cubicZone == null)
                {
					myScript.cubicZone = GameObject.CreatePrimitive(PrimitiveType.Cube);
                    //myScript.zonaCubica.GetComponent<MeshRenderer>().materials = new Material[0];
					myScript.cubicZone.name = "Range cube";
					myScript.cubicZone.transform.position = myScript.positionCube;
                    Color c = Color.yellow;
					c.a = 0.3f;
					myScript.cubicZone.GetComponent<Renderer>().material.color = c;
					myScript.cubicZone.transform.localScale = new Vector3(myScript.cubeSide, myScript.cubeSide, myScript.cubeSide);
                }

				if (myScript.cubeSide != myScript.oldCubeSideValue)
                {
					myScript.oldCubeSideValue = myScript.cubeSide;
					myScript.cubicZone.transform.position = myScript.positionCube;
					myScript.cubicZone.transform.localScale = new Vector3(myScript.cubeSide, myScript.cubeSide, myScript.cubeSide);
                }
            }
            else
            {
				GameObject.DestroyImmediate(myScript.cubicZone);
				myScript.cubicZone = null;
            }
        }
    }

}