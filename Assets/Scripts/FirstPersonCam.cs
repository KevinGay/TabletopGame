using UnityEngine;
using System.Collections;

public class FirstPersonCam : MonoBehaviour
{

    public float speedH = 2.0f;
    public float speedV = 2.0f;

    private float yaw = 0.0f;
    private float pitch = 0.0f;

    Rect crosshairRect;
    Texture crosshairTexture;

    private void Start()
    {
        float crosshairSize = Screen.width * 0.05f;
        crosshairTexture = Resources.Load("Textures/crosshair") as Texture;
        crosshairRect = new Rect(Screen.width / 2 - crosshairSize / 2, 
            Screen.height / 2 - crosshairSize / 1.5f,
            crosshairSize, crosshairSize);
    }

    void Update()
    {
        //Change this code to make compatible with VR headset
        yaw += speedH * Input.GetAxis("Mouse X");
        pitch -= speedV * Input.GetAxis("Mouse Y");

        transform.eulerAngles = new Vector3(pitch, yaw, 0.0f);

        if (Input.GetKey(KeyCode.Escape))
        {
            Cursor.lockState = CursorLockMode.None;
        }
        else
        {
            Cursor.lockState = CursorLockMode.Locked;
        }
    }

    void OnGUI()
    {
        GUI.DrawTexture(crosshairRect, crosshairTexture);
    }
}
