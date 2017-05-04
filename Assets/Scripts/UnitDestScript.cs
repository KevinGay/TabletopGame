using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitDestScript : MonoBehaviour {
	private UnitAI myUnit;
	
	void setMyUnit(GameObject other) {
		myUnit = other.GetComponent<UnitAI>();
	}
	
	void OnTriggerEnter(Collider other) {
		if (other.gameObject.name == "obj_fruit") {
			myUnit.pathToFruit = true;
		}
	}
}
