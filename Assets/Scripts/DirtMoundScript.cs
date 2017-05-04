using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DirtMoundScript : MonoBehaviour {
	private GameObject myPlant;
	public GameObject myController;
	private bool planted;
	
	// Use this for initialization
	void Start () {
		planted = false;
	}
	
	// Update is called once per frame
	void Update () {
		
	}
	
	void OnTriggerEnter(Collider other) {
		if (other.gameObject.name == "obj_fruitDest") {
			
		}
	}
}
