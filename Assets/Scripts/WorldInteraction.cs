using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class WorldInteraction : MonoBehaviour {

    NavMeshAgent playerAgent;

    void Start()
    {
        playerAgent = GetComponent<NavMeshAgent>();
    }

    void Update()
    {
        if (Input.GetMouseButton(0) && !UnityEngine.EventSystems.EventSystem.current.IsPointerOverGameObject())
        {
            getInteraction();
        }
    } 

    void getInteraction ()
    {
        Ray interactionRay = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit interactionInfo;
        if (Physics.Raycast(interactionRay, out interactionInfo, Mathf.Infinity))
        {
            GameObject interactedObject = interactionInfo.collider.gameObject;
            if(interactedObject.tag == "Interactable Object")
            {
                Debug.Log("Interactable interacted.");
            }
            else
            {
                //Pathfinding!!
                playerAgent.destination = interactionInfo.point;
            }
        }
    }
}
