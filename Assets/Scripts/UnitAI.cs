using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitAI : MonoBehaviour {
	public GameObject selRing;
	public Material consideredShader;
	public Material selectedShader;
	public float yOffset = .5f;
	
	public bool selected = false;
	private bool considered = false; //Is this unit being considered for selection?
	private GameObject myRing;
	
	void Start() {
		myRing = Instantiate(selRing);
		myRing.transform.localScale = new Vector3(.75f, 1f, .75f);
		myRing.GetComponent<Renderer>().material = consideredShader;
		myRing.SetActive(false);
	}
	
	void Update() {
		myRing.transform.position = new Vector3(transform.position.x, transform.position.y+yOffset, transform.position.z);
		if (Input.GetMouseButtonUp(0)) {
			checkSelected(true);
		}
		if (Input.GetMouseButton(0) == false) {
			if (considered) {
				considered = false;
				if (selected == false) {
					myRing.SetActive(false);
				}
			}
			
		}
	}
	
	void OnTriggerExit(Collider selector) {
		if (selector.gameObject.name == "obj_selector") {
			considered = false;
			myRing.SetActive(false);
		}
	}
    
	void OnTriggerEnter(Collider other) {
		if (other.gameObject.name == "obj_selector") {
			considered = true;
			myRing.SetActive(true);
			setShader(0);
		}
	}
	void setShader(int shaderType) {
		if (shaderType == 0) {
			myRing.GetComponent<Renderer>().material = consideredShader;
		}
		if (shaderType == 1) {
			myRing.GetComponent<Renderer>().material = selectedShader;
		}
	}
	
	void checkSelected(bool absCheck) {
		//absCheck: true is absolute check (e.g. deselect if not selected)
		//false is relative check (e.g. just don't select if not selected)
		if (considered) {
			selected = true;
			myRing.SetActive(true);
			setShader(1);
		}
		else if (absCheck) {
			selected = false;
			myRing.SetActive(false);
		}
	}
}
