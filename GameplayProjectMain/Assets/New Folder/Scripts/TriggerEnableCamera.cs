using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerEnableCamera : MonoBehaviour
{
    public Camera SplineCamera;
    public Camera MainCamera;
    public FinalPlayerCamera PlayerCameraScript;
    public PlayerController Player;
    public PlayerRail PlayerRailScript;
    public float RotationValue = 0;

    void OnTriggerEnter(Collider hit)
    {
        if (hit.gameObject.CompareTag("Player"))
        {
            PlayerCameraScript.enableCam = false;
            Player.disableAxis = true;
            Player.enableSideMove = false;
            SplineCamera.enabled = true;
            MainCamera.enabled = false;
            PlayerRailScript.enablePlayerRail = true;
            //PathSystemScript.cameraEnabled = true;
        }
    }

    void OnTriggerExit(Collider hit)
    {
        if (hit.gameObject.CompareTag("Player"))
        {
            PlayerCameraScript.enableCam = true;
            Player.enableSideMove = true;
            SplineCamera.enabled = false;
            MainCamera.enabled = true;
            PlayerRailScript.enablePlayerRail = false;
            //PathSystemScript.cameraEnabled = false;
        }
    }

    // Use this for initialization
    void Start ()
    {
        SplineCamera.enabled = false;
        MainCamera.enabled = true;
    }
	
	// Update is called once per frame
	void Update ()
    {
		
	}
}
