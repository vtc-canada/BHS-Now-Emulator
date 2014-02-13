using UnityEngine;
using System.Collections;
using System;
using System.Data;
using System.Data.Odbc;

public class PhotoEye : MonoBehaviour {

	// Use this for initialization
	private enum bagtype { Clear, Alarmed, NoDecision }
	private bool enabled = true;
	//private int statechanged = false;
	private bool laststate = false;	
	private Boolean hitstate = false;	
	public string IO_name_photoeye="";
	
	private bool excel_override = false;
	private bool excel_val = false;
	
	private bool risingedge = false;
	private bool risinglatch = false;
	
	public bool checkTrigger(){
		if(risingedge)
		{
			risingedge = false;
			return true;
		}
		return false;
	}
	
	void Start () {
		//splitcount = divertdenominator;
		/*System.Data.DataTable dTable = GetModuleData();
		
		if(dTable.Rows.Count > 0)		
		{				
		  for (int i = 0; i < dTable.Rows.Count; i++)	
		  {	
			if(dTable.Rows[i]["DeviceName"].ToString() == devicename)			
			{
				enabled = Convert.ToBoolean(dTable.Rows[i]["Enabled"]);
			}
		  }		
		}
		*/
		if(!enabled)
		{
			gameObject.renderer.material.color = Color.black;	
		}
	}
	
	public void setExcelOverride(bool excel_override,bool excel_val)
	{
		this.excel_override = excel_override;
		this.excel_val = excel_val;
	}
	
	public bool getState(){
		if(excel_override)
		{
			return excel_val;	
		}
		return !this.laststate;	
	}
	
	void OnMouseDown()
	{
		if(enabled)
		{
			gameObject.renderer.material.color = Color.black;	
			enabled = false;
		}else{
			gameObject.renderer.material.color = Color.gray;	
			enabled = true;			
		}
		
		
		
	}
	
	// Update is called once per frame
	void Update () {
		
		if(!enabled)
		{
			return;
		}
		
		RaycastHit hit;
       
		Ray photoray = new Ray(transform.position-transform.forward*0.15f,-transform.forward);
		//Debug.DrawRay(transform.position, -transform.forward);//new Vector3(0,0,-1));
        if ((Physics.Raycast(photoray, out hit, 2.8f)&&!excel_override)||(excel_override&&!excel_val))
		{
			if(risinglatch==false)
			{
				risinglatch = true;
				risingedge =true;
			}
			
			if(excel_override)
			{
				gameObject.renderer.material.color = Color.magenta;
			}else{
				gameObject.renderer.material.color = Color.red;				
			}
			foreach(Transform child in gameObject.transform)
			{
				if(child.name=="ray")
				{
					child.transform.localPosition = new Vector3(0f,0f,-2.85f);
					child.transform.localScale =new Vector3(0.1f,2.55f,0.1f);
				}	
				if(child.name=="reflector")
				{
					if(excel_override)
			        {
				        child.renderer.material.color = Color.magenta;
			        }else{
				        child.renderer.material.color = Color.red;				
			        }
				}
			        //D// your stuff
			}

			/*
			if(togglevertical!="")
			{
				GameObject verticaldiverter = GameObject.Find(togglevertical);
				if(hit.collider.gameObject.GetComponent<Bag>().type == (int)bagtype.Alarmed)
				{
					verticaldiverter.GetComponent<VerticalDiverter>().diverteron = 0; 
				}
				else if(hit.collider.gameObject.GetComponent<Bag>().type == (int)bagtype.NoDecision)
				{
					verticaldiverter.GetComponent<VerticalDiverter>().diverteron = 0; 
				}
				else
				{
					verticaldiverter.GetComponent<VerticalDiverter>().diverteron = 1; 
				}
				
			}
			if(laststate == false && toggleleft!="")
			{
				GameObject leftdiverter = GameObject.Find(toggleleft);
				splitcount --;
				if(splitcount ==0)
				{
					
					leftdiverter.GetComponent<DiverterLeftC>().diverteron = 1; 
					splitcount = divertdenominator;
				}
				else{					
					leftdiverter.GetComponent<DiverterLeftC>().diverteron = 0; 					
				}				
			}*/
			laststate = true;
		}
		else
		{
			risinglatch = false;
			if(excel_override)
			{
				gameObject.renderer.material.color = Color.black;
			}else{
				gameObject.renderer.material.color = Color.white;
			}
			foreach(Transform child in gameObject.transform)
			{
				if(child.name=="ray")
				{
					child.transform.localPosition = new Vector3(0f,0f,-5.7f);
					child.transform.localScale = new Vector3(0.1f,5.1f,0.1f);
				}
				if(child.name=="reflector")
				{
					if(excel_override)
			        {
				        child.renderer.material.color = Color.black;
			        }else{
				        child.renderer.material.color = Color.white;				
			        }
				}
			        //Do your stuff
			}
			laststate = false;
		}
	}
	
	
	private System.Data.DataTable GetModuleData()
    {
        var connString =  "Driver={Microsoft Excel Driver (*.xls)};DriverId=790;Dbq=C:\\GDrive\\Google Drive\\Development\\Unity\\simulationconfig.xls;";   //string.Format(@"Provider=Microsoft.ACE.OLEDB.12.0;Data Source=C:\\GDrive\\Google Drive\\Development\\Unity\\simulationconfig.xlsm;Extended Properties=""Excel 12.0 Macro;HDR=YES"";");
        string SelectSQL = "select * from [PhotoEyes$]";// OFFSET "+top;//WHERE [rack]=1 AND [slot]=1";  //WHERE rack=1 AND WHERE slot=1
        OdbcCommand dbCommand = null;
        //OleDbDataAdapter dataAdapter = null;
        System.Data.DataTable dTable = new DataTable("YourData");;
        OdbcConnection  conn = null;

        using(conn = new OdbcConnection (connString)){
	        conn.Open();
	        using(dbCommand = new OdbcCommand(SelectSQL.ToString(), conn))
			{
				OdbcDataReader rData = dbCommand.ExecuteReader();
				dTable.Load(rData);
			}
		}
        return dTable;
    }
}
