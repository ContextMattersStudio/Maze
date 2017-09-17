using UnityEngine;
using System.Collections;
namespace CardboardGestures.Conditions
{
    public class Condition_NearCube : AbstractCondition
    {
        public GameObject object1;
        public GameObject cubicZone;
        public bool showCubicZone;
        public float cubeSide = 2.0f;
		public float oldCubeSideValue;
        public Vector3 positionCube;

        // el range forma un cubo alrededor del centro del objeto que de ser traspasada hacia adentro 
		// hace que la función satisfied se evalue en true, de lo contrario false

        void Start()
        {
			if (cubicZone == null) 
			{
				cubicZone = GameObject.CreatePrimitive (PrimitiveType.Cube);
				cubicZone.transform.position = positionCube;
				cubicZone.transform.localScale = new Vector3(cubeSide, cubeSide, cubeSide);
			}
            cubicZone.GetComponent<MeshRenderer>().materials = new Material[0];
        }

        public override bool satisfied()
        {
            if (object1 != null)
			{	
				if (object1.GetComponent<BoxCollider>() == null) 
				{
					BoxCollider c = object1.AddComponent<BoxCollider>();
					c.size = new Vector3 (10, 10, 10);
				}
				if (cubicZone.GetComponent<BoxCollider>().bounds.Intersects(object1.GetComponent<BoxCollider>().bounds))
                {
                    return true;
                }
            }
            return false;
        }
    }
    
}
