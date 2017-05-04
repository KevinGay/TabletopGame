using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.VR;

public class VRToggle : MonoBehaviour
{

    public bool VR;
    public GameObject NonVRCamera;
    public GameObject VRCamera;
    public GameObject controller;

    void Awake()
    {
        if (VR == true)
        {
            VRCamera.SetActive(true);
            VRCamera.tag = "MainCamera";
        }
        else
        {
            NonVRCamera.SetActive(true);
            NonVRCamera.tag = "MainCamera";
            VRSettings.enabled = false;
        }
        controller.GetComponent<AcceptInput>().enabled = true;
    }
}
