using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEngine.UI;
using UnityEngine;

public class Door : MonoBehaviour
{
    public bool playerClose = false;
    private bool DoorOpening = false;

    public PlayerController Player;

    public GameObject Gate;
    public Text eToUse;


    public int DoorSpeed = 0;
    public int Duration = 0;
    public int CameraSpeed;

    int counter = 0;

    public Camera OriginalCamera;
    public Camera CutsceneCam1;
    public Camera CutsceneCam2;

    void OnTriggerEnter(Collider trigger)
    {
        if (trigger.gameObject.CompareTag("Player"))
        {
            eToUse.enabled = true;
            playerClose = true;
        }
    }

    void OnTriggerExit(Collider trigger)
    {
        if (trigger.gameObject.CompareTag("Player"))
        {
            eToUse.enabled = false;
            playerClose = false;
        }
    }

    // Use this for initialization
    void Start ()
    {
        CutsceneCam1.enabled = false;
        CutsceneCam2.enabled = false;
        OriginalCamera.enabled = true;
        eToUse.enabled = false;
    }
	
	// Update is called once per frame
	void Update ()
    {
        if (playerClose && Input.GetKeyDown("e") || Input.GetKeyDown("joystick button 2"))
        {
            Player.gameSettings.cinematic = true;
            Player.gameSettings.inGame = false;
            OriginalCamera.enabled = false;
            DoorOpening = true;
        }

        if (DoorOpening == true)
        {
            counter++;
            eToUse.enabled = false;
            CutsceneCam1.enabled = true;
            if (counter < Duration)
            {
                Gate.transform.Rotate(0, 0, DoorSpeed * Time.deltaTime);
                CutsceneCam1.transform.Translate(0, 0, -CameraSpeed * Time.deltaTime);

                if (counter > Duration / 2)
                {
                    CutsceneCam1.enabled = false;
                    CutsceneCam2.enabled = true;
                    CutsceneCam2.transform.Translate(0, 0, -CameraSpeed * Time.deltaTime);
                }
            }

              if (counter > Duration)
            {
                Player.gameSettings.cinematic = false;
                Player.gameSettings.inGame = true;
                CutsceneCam1.enabled = false;
                CutsceneCam2.enabled = false;
                OriginalCamera.enabled = true;
            }
        }
		
	}
}
