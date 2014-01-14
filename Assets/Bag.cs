using UnityEngine;
using System.Collections;

public class Bag : MonoBehaviour {
	
	private enum bagtype { Clear, Alarmed, NoDecision }
	public int type = (int)bagtype.Clear;
	
	private bool one_click = false;
	private bool timer_running;
	private float timer_for_double_click;
	private float delay =0.50f;

	// Use this for initialization
	void Start () {
	
	}
	
	void OnMouseOver(){
		if(Input.GetMouseButtonDown(0))
		{
		   if(!one_click) // first click no previous clicks
		   {
		     one_click = true;
		
		     timer_for_double_click = Time.time; // save the current time
		     // do one click things;
		   } 
		   else
		   {
		     one_click = false; // found a double click, now reset
			
				//Debug.Log("doubleclick");;
				//Component.Destroy(gameObject.GetComponent<DragRigidbody>());
				Destroy(gameObject);	
		   }
		}
		
		if(one_click)
		{
		   // if the time now is delay seconds more than when the first click started. 
		   if((Time.time - timer_for_double_click) > delay)
			{
			    one_click = false;			
			}
		}
		
	}
	
	// Update is called once per frame
	void Update () {
		if(gameObject.transform.position.y<-10)
		{
			Destroy(gameObject);	
		}
		

	}
}
