using UnityEngine;
using System.Collections;

public class MouseBagDropper : MonoBehaviour {
	
	private enum bagtype { Clear, Alarmed, NoDecision }
	
	private float lastbagdrop = 0;	
	private float delay =0.25f;
	
	public float baglength = 28f;
	public float bagwidth = 16f;
	public float bagheight = 12f;
	
	public float bagalarmed = 15;
	public float bagnodecision = 5;
	public float bagclear = 80;
	
	
	private float bag_static_friction = 0f;//0.1f;
	private float bag_dynamic_friction = 0f;//0.1f;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		
		if (Input.GetKey (KeyCode.B)){
			Ray ray = Camera.main.ScreenPointToRay (Input.mousePosition);
			
			RaycastHit hit;
			
			if (Physics.Raycast (ray, out hit, 1000)) {
					
			    //Debug.DrawLine (ray.origin, hit.point);
				
				if((Time.time - lastbagdrop) > delay)
				{
					lastbagdrop = Time.time;
					if(hit.collider.gameObject.name == "Conveyor Belt"||hit.collider.gameObject.name == "Conveyer Belt")
					{
						GameObject cube = GameObject.CreatePrimitive(PrimitiveType.Cube);
						cube.transform.position = new Vector3(hit.point.x, hit.point.y + cube.transform.localScale.y/2 -0.001f, hit.point.z);
						cube.transform.localScale = new Vector3 (bagwidth/12f, bagheight/12f, baglength/12f);
						cube.transform.rotation = hit.collider.gameObject.transform.rotation;
						
						cube.transform.Rotate(new Vector3(0,1,0),90f);
						cube.transform.Rotate(new Vector3(0,0,1),90f);
						
						
						
						//cube.transform.rotation = new Quaternion(1,1,1,1);
//cube.transform.localRotation = new Quaternion(-hit.collider.gameObject.transform.rotation.y,hit.collider.gameObject.transform.rotation.z,-hit.collider.gameObject.transform.rotation.x,1);

						
						//cube.transform.rotation =  hit.collider.gameObject.transform.rotation;
						//cube.transform.rota
						//cube.transform.rotation.SetFromToRotation(new Vector3(1,1,1),new Vector3(1,1,2));
						cube.AddComponent<Rigidbody>();
						
						cube.rigidbody.constraints = RigidbodyConstraints.FreezePositionY;
						//if(constrainY)
						//{
						//	cube.rigidbody.constraints = RigidbodyConstraints.FreezePositionY;
						//}
						cube.rigidbody.angularDrag = Mathf.Infinity;
						
						//cube.rigidbody.mass = 10;
						
						
						cube.collider.material.dynamicFriction = bag_dynamic_friction;
						cube.collider.material.staticFriction = bag_static_friction;
						cube.gameObject.AddComponent<Bag>();
						cube.gameObject.AddComponent<DragRigidbody>();
						
						float chosentype = Random.Range(0,bagalarmed+bagnodecision+bagclear);
						if(chosentype <bagalarmed)
						{
							cube.gameObject.GetComponent<Bag>().type = (int)bagtype.Alarmed;
						}
						else if(chosentype <bagalarmed+bagnodecision)
						{
							cube.gameObject.GetComponent<Bag>().type = (int)bagtype.NoDecision;
						}
						else
						{
							cube.gameObject.GetComponent<Bag>().type = (int)bagtype.Clear;
						}
				    	Debug.Log ("Dropped bag : " + hit.collider.gameObject.name);
					}
				}
			}	
	    }		
	}
}
