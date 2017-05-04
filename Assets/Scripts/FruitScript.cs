using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FruitScript : MonoBehaviour {

	public int minUnits = 1; //The minimum required units for this object to be moved;
	public int maxUnits = 2; //The maximum allowed units to move this object at one time;
	
	public float [] xOffset = new float[2];
	public float [] yOffset = new float[2];
	
	public float getXOffset(int unitNumber) {
		return xOffset[unitNumber];
	}
	public float getYOffset(int unitNumber) {
		return xOffset[unitNumber];
	}
	
}
