using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorSwitch : MonoBehaviour
{
    //objects
    public Rigidbody playerAvatar;
    //public AvatarController playerCharacter;
    public GameObject buttonDoor;
    public GameObject doorLeft;
    public GameObject doorRight;
    public GameObject triggerClose;
    public GameObject playerCamera;
    public GameObject sceneLocale1;
    public GameObject sceneLocale2;
    public GameObject originalCamPos;

    //bools
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

    void Start()
    {

    }

    void Update()
    {
        //GameObject g = GameObject.FindGameObjectWithTag("Player");

        //playerCharacter = playerCharacter.GetComponent<AvatarController>();
        if (playerClose && Input.GetKeyDown("e") && !buttonPressed)
        {
            openDoor = true;
        }

        if (openDoor == true && !buttonPressed)
        {
            //playerCharacter.freezePos = true;
            //playerCharacter.EnableAnimateBool("Idle");
            //playerCharacter.EnableAnimateBool("PressButton");
            playerCamera.transform.SetPositionAndRotation(sceneLocale1.transform.position, sceneLocale1.transform.rotation);
            intCounter++;
            buttonDoor.transform.Translate(0, 0, -2 * Time.deltaTime);

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
            if (intCounter == 80)
            {
                playerCamera.transform.SetPositionAndRotation(sceneLocale2.transform.position, sceneLocale2.transform.rotation);
            }

            if (intCounter > 80 && intCounter != 300)
            {
                playerCamera.transform.Translate(0, 0, -1 * Time.deltaTime);
            }
        }

        if (intCounter == 300 && doorOpened)
        {
            //playerCharacter.freezePos = false;
            playerCamera.transform.SetPositionAndRotation(originalCamPos.transform.position, originalCamPos.transform.rotation);
            doorOpened = false;
        }



    }
}
