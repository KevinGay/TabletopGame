using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class AcceptInput : MonoBehaviour {
	public float initialX;
	public float initialZ;
	public Material selectShader;
	public GameObject cursorRing;
	public Material cursorShader;
	
	public GameObject selectionRange;
	private bool selecting = false;
	private float xScale = 0.0f;
	private float zScale = 0.0f;
	private float xScaleMax = 10.0f;
	private float zScaleMax = 10.0f;
	private float xScaleDelta = .5f;
	private float zScaleDelta = .5f;
	private GameObject myCursor;

    private float navSpread = 1.5f;
	
	private Camera myCam;

    public GameObject Target;
    public static GameObject[] allUnits;
    public static List<GameObject> CurrentlySelectedUnits = new List<GameObject>();
    RaycastHit hit;
    Vector3 mouseDownPoint = new Vector3(0.0f, 0.0f, 0.0f);

    void Awake()
    {
        mouseDownPoint = Vector3.zero;

        //Keep all units in an array for future reference
        allUnits = GameObject.FindGameObjectsWithTag("Player");
    }

    // Use this for initialization
    void Start () {
		selectionRange = Instantiate<GameObject>(selectionRange);
		selectionRange.name = "obj_selector";
		selectionRange.transform.position = new Vector3(0.0f, 0.0f, 0.0f);
		selectionRange.GetComponent<Renderer>().material = selectShader;
		selectionRange.transform.localScale = new Vector3(initialX, .001f, initialZ);
		selectionRange.SetActive(false);
		myCam = GetComponentInParent<Camera>();
		cursorRing.GetComponent<Renderer>().material = cursorShader;
		
		myCursor = Instantiate(cursorRing);
	}
	
	// Update is called once per frame
	void Update () {
        //Remove any objects from currentlySelectedUnits that aren't selected

        //Cannot change list while looping through with 'foreach', so create a separate remove list
        //then remove the units in the remove list after the loop completes
        List<GameObject> UnitsToRemove = new List<GameObject>();
        foreach (GameObject unit in CurrentlySelectedUnits)
        {
            if (!unit.GetComponent<UnitAI>().selected)
            {
                UnitsToRemove.Add(unit);
            }
        }
        foreach (GameObject unit in UnitsToRemove)
        {
            CurrentlySelectedUnits.Remove(unit);
        }

        /* Selection radius expansion */
        if (selecting) {
			myCursor.SetActive(false);
			if (selectionRange.activeSelf == false) {
				selectionRange.SetActive(true);
			}
			selectionRange.transform.localScale = new Vector3(xScale, .001f, zScale);
			if (xScale < xScaleMax) {
				xScale+=xScaleDelta;
			}
			else {
				xScale = xScaleMax;
			}
			if (zScale < zScaleMax) {
				zScale+=zScaleDelta;
			}
			else {
				zScale = zScaleMax;
			}

            //store all currently selected units in an array list
            foreach (GameObject unit in allUnits)
            {
                if (unit.GetComponent<UnitAI>().selected && !CurrentlySelectedUnits.Contains(unit)) 
                {
                    CurrentlySelectedUnits.Add(unit);
                }
            }
            
		}
		else {
			myCursor.SetActive(true);
			if (selectionRange.activeSelf) {
				xScale = 0.0f;
				zScale = 0.0f;
				selectionRange.transform.localScale = new Vector3(xScale, .001f, zScale);
				selectionRange.SetActive(false);
			}
		}


        //Need to raycast from mouse to a point on the map to determine where
        //the selection circle appears
        Ray pointerStart = Camera.main.ScreenPointToRay(Input.mousePosition);
		int layerMask = 1 << 8;
		if (Physics.Raycast(pointerStart, out hit, Mathf.Infinity, layerMask)) {
			mouseDownPoint = hit.point;
			myCursor.transform.position = mouseDownPoint;
			//myCursor.transform.localRotation = Quaternion.identity;
			/* Accept User Input */
			if (Input.GetMouseButton(0)) {
				selecting = true;
				selectionRange.transform.position = mouseDownPoint;
                
            }
            else if (Input.GetMouseButton(1))
            {

                float theta = Mathf.PI * 2 / CurrentlySelectedUnits.Count;

                //Move each unit so that they are offset in a circle around the center point,
                //Unless only one unit is selected. In that case just move it to the point
                if (CurrentlySelectedUnits.Count == 1)
                {
                    GameObject ArrayListUnit = CurrentlySelectedUnits[0] as GameObject;
                    NavMeshAgent nav = ArrayListUnit.GetComponent<NavMeshAgent>();
                    nav.SetDestination(mouseDownPoint);
                }
                else
                {
                    for (int i = 0; i < CurrentlySelectedUnits.Count; i++)
                    {
                        GameObject ArrayListUnit = CurrentlySelectedUnits[i] as GameObject;
                        NavMeshAgent nav = ArrayListUnit.GetComponent<NavMeshAgent>();
                        float angle = theta * i;
                        Vector3 offset = new Vector3(navSpread * Mathf.Cos(angle), 0.0f, navSpread * Mathf.Sin(angle));

                        nav.SetDestination(mouseDownPoint + offset);
                    }
                }
            }
            else {
				selecting = false;
			}

            if (hit.collider.name == "Floor")
            {

                // When we click the right mouse button, instantiate target if there are units selected
                if (Input.GetMouseButtonDown(1) && CurrentlySelectedUnits.Count > 0)
                {
                    GameObject TargetObj = Instantiate(Target, hit.point, Quaternion.identity);
                    TargetObj.name = "Target Instantiated";

                }
            }
        }
	}
	
}
