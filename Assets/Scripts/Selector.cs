using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Selector : MonoBehaviour {

	void OnTriggerExit(Collider selector) {
		if (selector.gameObject.name == "Unit") {
			//considered = false;
		}
	}
	void OnTriggerEnter(Collider selector) {
		Debug.Log("!");
		if (selector.gameObject.name == "Unit") {
			//considered = true;
		}
	}
}
