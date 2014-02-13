using UnityEngine;
using System.Collections;

public class Drop1bag : MonoBehaviour {
	
	private enum security_status_type { Clear, Alarmed,  Error, Pending, Unscanned }
	public float startx = -0.5f;
	public float startz = 11.5f;
	public float starty = 0.22f;
	public float bagalarmed = 10;
	public float bagpending = 5;
	public float bagclear = 80;
	public float bagerror = 5;
	
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	
	void OnMouseDown(){ 
		GameObject cube = GameObject.CreatePrimitive(PrimitiveType.Cube);
		cube.transform.position = new Vector3(startx, cube.transform.localScale.y/2 +starty, startz);
		cube.transform.localScale = new Vector3 (1.33f, 1f, 2.33f);
		cube.AddComponent<Rigidbody>();
		//if(constrainY)
		//{
		//	cube.rigidbody.constraints = RigidbodyConstraints.FreezePositionY;
		//}
		cube.rigidbody.angularDrag = Mathf.Infinity;
		cube.collider.material.dynamicFriction = 0;
		cube.collider.material.staticFriction = 0;
		
		cube.gameObject.AddComponent<Bag>();
		cube.gameObject.AddComponent<DragRigidbody>();
		
		
		/*float chosentype = Random.Range(0,bagalarmed+bagpending+bagerror+bagclear);
		if(chosentype <bagalarmed)
		{
			cube.gameObject.GetComponent<Bag>().security_status = (int)security_status_type.Alarmed;
		}
		else if(chosentype <bagalarmed+bagpending)
		{
			cube.gameObject.GetComponent<Bag>().security_status = (int)security_status_type.Pending;
		}
		else if(chosentype <bagalarmed+bagpending+bagerror)
		{
			cube.gameObject.GetComponent<Bag>().security_status = (int)security_status_type.Error;
		}
		else
		{
			cube.gameObject.GetComponent<Bag>().security_status = (int)security_status_type.Clear;
		}*/
		
	}
}
