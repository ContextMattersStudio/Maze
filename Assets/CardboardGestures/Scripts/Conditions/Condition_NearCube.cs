using UnityEngine;
using System.Collections;
namespace CardboardGestures.Conditions
{
    public class Condition_NearCube : AbstractCondition
    {
        public GameObject objeto1;
        public GameObject zonaCubica;
        public bool showZonaCubica;
        public float lado;
        public float oldLado;
        public Vector3 posicion;

        // el range forma una esfera alrededor del centro del objeto que de ser traspasada hacia adentro 
		// hace que la función satisfied se evalue en true, de lo contrario en false

        void Start()
        {
			if (zonaCubica == null) 
			{
				zonaCubica = GameObject.CreatePrimitive (PrimitiveType.Cube);
				zonaCubica.transform.position = posicion;
				zonaCubica.transform.localScale = new Vector3(lado, lado, lado);
			}
            zonaCubica.GetComponent<MeshRenderer>().materials = new Material[0];
        }

        public override bool satisfied()
        {
            if (objeto1 != null)
			{	
				if (objeto1.GetComponent<BoxCollider>() == null) 
				{
					BoxCollider c = objeto1.AddComponent<BoxCollider>();
					c.size = new Vector3 (10, 10, 10);
				}
				if (zonaCubica.GetComponent<BoxCollider>().bounds.Intersects(objeto1.GetComponent<BoxCollider>().bounds))
                {
                    return true;
                }
            }
            return false;
        }
    }
    
}
