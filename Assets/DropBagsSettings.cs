using UnityEngine;
using System.Collections;

public class DropBagsSettings : MonoBehaviour {
	
	private enum bagtype { Clear, Alarmed, NoDecision }
	private int GUIopen = 0;
	private Rect windowRect;
	public string devicename = "1";
	private int windowID=1;
	public float minlengthx=12f;
	public float maxlengthx=24f;
	public float minlengthy=6f;
	public float maxlengthy=12f;
	public float minlengthz=24f;
	public float maxlengthz=36f;
	public bool random=true;
	public float dropinterval = 0f;
	public bool normaldistribution = true;
	private float nextinterval = 0f;
	public float lastupdate=0f;
	public float startx = -0.5f;
	public float startz = 11.5f;
	public float starty = 0.22f;
	public float bagalarmed = 15;
	public float bagnodecision = 5;
	public float bagclear = 80;
	public bool constrainY = true;
	public float startAngle = 0;


	void Start () {
		windowID = Random.Range(0,100000);
		windowRect= new Rect (50, 50, 400, 400);
		nextinterval = 	dropinterval;//(float)SimpleRNG.SimpleRNG.GetNormal((double)dropinterval,(double)(dropinterval/4));
	}


	void OnGUI(){
		if(GUIopen==1)
		{
			windowRect = GUI.Window (windowID, windowRect, WindowFunction, "Drop Box Properties: " + devicename);
		}
	}

	void WindowFunction (int windowID) {
	    if (GUI.Button (new Rect (270, 0,30, 30), "X")) {
	    	GUIopen = 0;
	  	}
	 
	 	random = GUI.Toggle(new Rect(5, 35, 150, 30), random, "Random Sizes");
	 
	 
	 	GUI.Label(new Rect (5, 65, 45, 20),"min-x:");
	  	minlengthx = GUI.HorizontalSlider (new Rect (50, 70, 115, 30), minlengthx, 1f, 36f);
	  	GUI.Label(new Rect (170, 65, 50, 20),(minlengthx/1f).ToString());
	  	 
	 	GUI.Label(new Rect (205, 65, 45, 20),"max-x:");	 	
	  	maxlengthx = GUI.HorizontalSlider (new Rect (250, 70, 115, 30), maxlengthx, 1f, 36f);
	  	GUI.Label(new Rect (370, 65, 50, 20),(maxlengthx/1f).ToString());
	  	
	 	GUI.Label(new Rect (5, 95, 45, 20),"min-y:");
	  	minlengthy = GUI.HorizontalSlider (new Rect (50, 100, 115, 30), minlengthy, 1f, 36f);
	  	GUI.Label(new Rect (170, 95, 50, 20),(minlengthy/1f).ToString());
	  	 
	 	GUI.Label(new Rect (205, 95, 45, 20),"max-y:");	 	
	  	maxlengthy = GUI.HorizontalSlider (new Rect (250, 100, 115, 30), maxlengthy, 1f, 36f);
	  	GUI.Label(new Rect (370, 95, 50, 20),(maxlengthy/1f).ToString());
	  	
	 	GUI.Label(new Rect (5, 125, 45, 20),"min-z:");
	  	minlengthz = GUI.HorizontalSlider (new Rect (50, 130, 115, 30), minlengthz, 1f, 36f);
	  	GUI.Label(new Rect (170, 125, 50, 20),(minlengthz/1f).ToString());
	  	 
	 	GUI.Label(new Rect (205, 125, 45, 20),"max-z:");	 	
	  	maxlengthz = GUI.HorizontalSlider (new Rect (250, 130, 115, 30), maxlengthz, 1f, 36f);
	  	GUI.Label(new Rect (370, 125, 50, 20),(maxlengthz/1f).ToString());
	  	
	  	
	  	
	  	GUI.Label(new Rect (5, 175, 75, 20),"Drop Interval");	 	
	  	dropinterval = GUI.HorizontalSlider (new Rect (100, 180, 115, 30), dropinterval, 0f, 10f);
	  	GUI.Label(new Rect (220, 175, 50, 20),(dropinterval).ToString());
	  	
		normaldistribution = GUI.Toggle(new Rect(5, 200, 150, 30), normaldistribution, "Normal Distribution");
	  	
	  	GUI.Label(new Rect (5, 225, 75, 20),"Bag Alarmed");	 	
	  	bagalarmed = GUI.HorizontalSlider (new Rect (100, 230, 115, 30), bagalarmed, 0f, 100f);
	  	GUI.Label(new Rect (220, 225, 50, 20),(bagalarmed).ToString());
	  	
	  	GUI.Label(new Rect (5, 255, 75, 20),"Bag No Decision");	 	
	  	bagnodecision = GUI.HorizontalSlider (new Rect (100, 260, 115, 30), bagnodecision, 0f, 100f);
	  	GUI.Label(new Rect (220, 255, 50, 20),(bagnodecision).ToString());
	  	
	  	GUI.Label(new Rect (5, 285, 75, 20),"Bag Clear");	 	
	  	bagclear = GUI.HorizontalSlider (new Rect (100, 290, 115, 30), bagclear, 0f, 100f);
	  	GUI.Label(new Rect (220, 285, 50, 20),(bagclear).ToString());
	  	
	  	//overridespeed = GUI.Toggle(new Rect(5, 35, 150, 30), overridespeed, "Override Speed");
	//  	if(overridespeed)
	//  	{
	  		//speed = GUI.HorizontalSlider (new Rect (55, 70, 115, 30), speed, 0.0, 20.0);
		  	//GUI.Label(new Rect (5, 65, 50, 20),"Speed");
		  	//GUI.Label(new Rect (175, 65, 25, 30),speed.ToString());
	//	}
	
		
	
	
		GUI.DragWindow (new Rect (0,0, 10000, 10000));
	}

	void FixedUpdate(){
		if(dropinterval==0)
		{
			return;
		}
		if((!normaldistribution&&(Time.time - dropinterval > lastupdate))||(normaldistribution&&(Time.time - nextinterval>lastupdate)))
		{
		
			
			/// Check if the belt is stopped, if so, break out and dont drop the bag!!
			Ray ray = new Ray (new Vector3(startx, starty+0.1f, startz), new Vector3(0,-1,0));
			RaycastHit hit;
			if (Physics.Raycast (ray, out hit, 1)) {
				if(hit.collider.gameObject.GetComponent<conveyerForward>()!=null){
					if(!hit.collider.gameObject.GetComponent<conveyerForward>().override_speed&&!hit.collider.gameObject.GetComponent<conveyerForward>().running)
					{
						lastupdate = Time.time;
						return;
					}
				}
			}
			
			
			// check if there is a bag that is close, (within 6 inches) - then dont drop it. TODO!
			/*
			ray = new Ray (new Vector3(startx, starty+0.1f, startz), new Vector3(0,-1,0));
			RaycastHit hit;
			if (Physics.Raycast (ray, out hit, 1)) {
				if(hit.collider.gameObject.GetComponent<conveyerForward>()!=null){
					if(!hit.collider.gameObject.GetComponent<conveyerForward>().override_speed&&!hit.collider.gameObject.GetComponent<conveyerForward>().running)
					{
						lastupdate = Time.time;
						return;
					}
				}
			}
			*/
			
			GameObject cube = GameObject.CreatePrimitive(PrimitiveType.Cube);
		
			if(random)
			{
				cube.transform.localScale = new Vector3 (Random.Range(minlengthx,maxlengthx)/12f, Random.Range(minlengthy,maxlengthy)/12f, Random.Range(minlengthz,maxlengthz)/12f);
			}
			else{
				cube.transform.localScale = new Vector3 (maxlengthx/12f, maxlengthy/12f, maxlengthz/12f);
			}
			
			//print(maxlengthx/10f);
			cube.transform.position = new Vector3(startx, cube.transform.localScale.y/2 +starty, startz);
			cube.transform.rotation = new Quaternion (0,startAngle,0,1);
			cube.AddComponent<Rigidbody>();
			
			
			
			
			
			
			if(constrainY)
			{
				cube.rigidbody.constraints = RigidbodyConstraints.FreezePositionY;
			}
			cube.rigidbody.angularDrag = Mathf.Infinity;
			cube.collider.material.dynamicFriction = 0;
			cube.collider.material.staticFriction = 0;
			
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
			
			//Debug.Log("droppin");
			lastupdate = Time.time;
			
			if(normaldistribution)
			{
				if(dropinterval!=0.0f)
				{
					nextinterval = 	(float)SimpleRNG.SimpleRNG.GetNormal((double)dropinterval,(double)(dropinterval/4));
					if(nextinterval<dropinterval*3f/4f)
					{
						nextinterval = dropinterval*3f/4f;
					}
				}
			}
			
			
			
			
		}
	
	}
	
	void OnMouseDown(){ 
		if(GUIopen==1)
		{
			GUIopen=0;
		}
		else{
			GUIopen=1;
		}
	}
	

}
