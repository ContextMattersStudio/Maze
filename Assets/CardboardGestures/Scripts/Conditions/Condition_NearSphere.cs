using UnityEngine;
using System.Collections;
namespace CardboardGestures.Conditions
{
    public class Condition_NearSphere : AbstractCondition
    {
        public GameObject object1;
        public GameObject object2;
        public float range;
        public float oldCubeSide;
        public GameObject SphericZone;
        public bool showSphericZone;

        // el range forma una zonaEsferica alrededor del centro del objeto que de ser traspasada hacia adentro hace que la función satisfied se evalue en true, de lo contrario en false

        void Start()
        {
			if (SphericZone == null) 
			{
				SphericZone = GameObject.CreatePrimitive (PrimitiveType.Sphere);
			}
            SphericZone.SetActive(false);
        }

        public override bool satisfied()
        {
            if (object1 != null && object2 != null)
            {
                Vector3 v = object1.transform.position - object2.transform.position;
                if (v.magnitude < range)
                {
                    return true;
                }
            }
            return false;
        }
    }
}
