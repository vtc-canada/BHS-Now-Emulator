using UnityEngine;
using System.Collections;

public class Bag : MonoBehaviour {
	
	private enum security_status_type { Clear, Alarmed, Error, Pending,Unscanned }
	private int security_status = (int)security_status_type.Unscanned;
	private int prime_security_status = (int)security_status_type.Unscanned;
	private bool security_status_primed = false;
	private float prime_security_status_timer = 0;
	private float falling_photoeye_delay = 0.2f;
	
	private bool one_click = false;
	private bool timer_running;
	private float timer_for_double_click;
	private float delay =0.50f;

	// Use this for initialization
	void Start () {
	
	}
	
	public void primeSecurityStatus(int set_status)
	{
		prime_security_status = set_status;
		security_status_primed = true;
		prime_security_status_timer = Time.time;
	}
	
	public void setSecurityStatus(int set_status)
	{
		security_status = set_status;
		fixSecurityColors();
	}
	
	private void fixSecurityColors(){
		if(security_status == (int)security_status_type.Clear)
		{
			gameObject.renderer.material.color = Color.green;
		}
		else if(security_status == (int)security_status_type.Alarmed)
		{
			gameObject.renderer.material.color = Color.red;
		}
		else if(security_status == (int)security_status_type.Error)
		{
			gameObject.renderer.material.color = Color.black;
		}
		else if(security_status == (int)security_status_type.Pending)
		{
			gameObject.renderer.material.color = Color.blue;
		}
		else if(security_status == (int)security_status_type.Unscanned)
		{
			gameObject.renderer.material.color = Color.white;
		}
		
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
		
		if(security_status_primed&&(Time.time -prime_security_status_timer )> falling_photoeye_delay)
		{
			security_status_primed = false;
			setSecurityStatus(prime_security_status);
		}
		

	}
}
