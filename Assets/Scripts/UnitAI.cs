using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class UnitAI : MonoBehaviour {
	public GameObject selRing;
	public Material consideredShader;
	public Material selectedShader;
	
	public Material unitShader;
	public Material unitHoldingShader;
	public float yOffset = .5f;
	
	public bool selected = false;
	private bool considered = false; //Is this unit being considered for selection?
	private GameObject myRing;
	
	private GameObject targetObject;
	private bool isGettingObject = false;
	
	public bool pathToFruit = false;
	public bool holdingObject = false;
	
	private int carryingNum;

	public int angularSpd = 120;
	
	public GameObject myDest;
	
	void Start() {
		myRing = Instantiate(selRing);
		myRing.transform.localScale = new Vector3(.75f, 1f, .75f);
		myRing.GetComponent<Renderer>().material = consideredShader;
		myRing.SetActive(false);
		gameObject.name = "obj_unit";
	}
	void OnCollisionEnter(Collider other) {
		Physics.IgnoreCollision(GetComponent<Collider>(), other, true);
	}
	
	
	void Update() {
		myRing.transform.position = new Vector3(transform.position.x, transform.position.y+yOffset, transform.position.z);
		if (GetComponent<InputManager>().getSelectUp()) {
			checkSelected(false);
		}
		if (GetComponent<InputManager>().getSelect() == false) {
			if (considered) {
				considered = false;
				if (selected == false) {
					myRing.SetActive(false);
				}
			}
			
		}
		if (holdingObject) {
			GetComponent<Renderer>().material = unitHoldingShader;
			float xOffset = targetObject.GetComponent<AppleScript>().xOffset[carryingNum];
			float zOffset = targetObject.GetComponent<AppleScript>().zOffset[carryingNum];
			float tarX = targetObject.transform.position.x;
			float tarY = targetObject.transform.position.y;
			float tarZ = targetObject.transform.position.z;
			Vector3 dest = new Vector3(tarX+xOffset, tarY, tarZ+zOffset);
			GetComponent<NavMeshAgent>().SetDestination(dest);
			GetComponent<NavMeshAgent>().angularSpeed = 0;
		}
		else {
			GetComponent<Renderer>().material = unitShader;
			GetComponent<NavMeshAgent>().angularSpeed = angularSpd;
		}
	}
	
	void OnTriggerExit(Collider other) {
		if (other.gameObject.name == "obj_selector") {
			considered = false;
			myRing.SetActive(false);
		}
		if (isGettingObject) {
			if (other.gameObject == targetObject) {
				isGettingObject = false;
			}
		}
		
	}
    
	void OnTriggerEnter(Collider other) {
		if (other.gameObject.name == "obj_selector") {
			considered = true;
			myRing.SetActive(true);
			if (!selected) {
				setShader(0);
			}
		}
		if (isGettingObject && !holdingObject) {
			if (other.gameObject == targetObject) {
				holdingObject = true;
			}
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
		if (considered && !selected) {
			selected = true;
			myRing.SetActive(true);
			setShader(1);
			GameObject.Find("ReVIVE Scoreboard").GetComponent<GameHandler>().unitFound();
		}
		else if (absCheck) {
			//selected = false;
			//myRing.SetActive(false);
		}
	}
	
	public void setGettingObject(bool val) {
		isGettingObject = val;
	}
	public void setTargetObject(GameObject obj) {
		targetObject = obj;
	}
	
	public void pathToPoint(Vector3 point) {
		NavMeshAgent myNav = GetComponent<NavMeshAgent>();
		myNav.SetDestination(point);
	}
	
	public GameObject getTargetObject() {
		return targetObject;
	}
	
	public bool getGettingObject() {
		return isGettingObject;
	}
	
	public int getCarryingNum() {
		return carryingNum;
	}
	public void setCarryingNum(int num) {
		carryingNum = num;
	}
	public void setDest(Vector3 destinationPoint) {
		myDest.transform.position = destinationPoint;
		GetComponent<NavMeshAgent>().SetDestination(myDest.transform.position);
	}
}
