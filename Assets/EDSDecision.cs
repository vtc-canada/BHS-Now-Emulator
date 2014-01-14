using UnityEngine;
using System.Collections;

public class EDSDecision : MonoBehaviour {
	private enum bagtype { Clear, Alarmed, NoDecision }
	
	void OnTriggerEnter (Collider collision) {
		if(collision.gameObject.GetComponent<Bag>().type == (int)bagtype.Clear)
		{
			collision.gameObject.renderer.material.color = Color.green;
		}
		else if(collision.gameObject.GetComponent<Bag>().type == (int)bagtype.Alarmed)
		{
			collision.gameObject.renderer.material.color = Color.red;
		}
		else if(collision.gameObject.GetComponent<Bag>().type == (int)bagtype.NoDecision)
		{
			collision.gameObject.renderer.material.color = Color.yellow;
		}
	
	
	}
}
