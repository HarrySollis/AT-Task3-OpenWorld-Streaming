using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Switch : MonoBehaviour
{
    //objects
    public Rigidbody playerAvatar;
    public GameObject buttonDoor;
    public GameObject doorLeft;
    public GameObject doorRight;
    public GameObject triggerClose;

    public int cameraSpeed = 0;


    public Camera OriginalCamera;
    public Camera CutsceneCam1;
    public Camera CutsceneCam2;


    public PlayerController Player;

    //bools
    public bool playerLooking = false;
    public bool playerClose;
    public bool buttonPressed;
    public bool doorOpened;
    bool openDoor;

    //ints
    public int intCounter = 0;



    void OnTriggerEnter(Collider trigger)
    {
        if (trigger.gameObject.CompareTag("Player"))
        {
            playerClose = true;
        }
    }

    void OnTriggerExit(Collider trigger)
    {
        if (trigger.gameObject.CompareTag("Player"))
        {
            playerClose = false;
        }
    }

    // Use this for initialization
    void Start ()
    {
        CutsceneCam2.enabled = false;
        CutsceneCam1.enabled = false;
        OriginalCamera.enabled = true;
    }
	
	// Update is called once per frame
	void Update ()
    {
        if (playerClose && (Input.GetKeyDown("e") && !buttonPressed || Input.GetKeyDown("joystick button 2") && !buttonPressed))
        {
            if (playerLooking)
            {
                openDoor = true;
                //Player.npcSettings.anim.SetTrigger("Button");
            }
        }

        if (openDoor == true && !buttonPressed)
        {
            intCounter++;
            buttonDoor.transform.Translate(0, 0, -2 * Time.deltaTime);
            Player.gameSettings.cinematic = true;
            Player.gameSettings.inGame = false;

            if (intCounter > 25)
            {
                intCounter = 0;
                buttonPressed = true;
            }
        }

        if (buttonPressed && intCounter < 300)

        {
            intCounter++;
            doorLeft.transform.Translate(0, -0.5f * Time.deltaTime, 0);
            doorRight.transform.Translate(0, -0.5f * Time.deltaTime, 0);
            doorOpened = true;
            CutsceneCam2.enabled = false;
            CutsceneCam1.enabled = true;
            OriginalCamera.enabled = false;
            CutsceneCam1.transform.Translate(0, 0, -cameraSpeed * Time.deltaTime);
            if (intCounter == 80)
            {
            }

            if (intCounter > 80 && intCounter != 300)
            {
                CutsceneCam2.enabled = true;
                CutsceneCam1.enabled = false;
                OriginalCamera.enabled = false;
                CutsceneCam2.transform.Translate(0, 0, -cameraSpeed * Time.deltaTime);
            }
        }

        if (intCounter == 300 && doorOpened)
        {
            Player.gameSettings.cinematic = false;
            Player.gameSettings.inGame = true;
            CutsceneCam2.enabled = false;
            CutsceneCam1.enabled = false;
            OriginalCamera.enabled = true;
            doorOpened = false;
        }
    }
}
