using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerProximity : MonoBehaviour
{
    public int flashTime = 0;
    public int flashLength = 0;
    public bool enable_flash;
    Color color;
    Color origColor;
    GameObject playerTarget;
    Slime slimeScript;
    Rigidbody rig;
    public int counter = 0;

    void Flasher()
    {

    }

    void OnTriggerStay(Collider Other)
    {
        if (Other.gameObject.tag == "Enemy" && (Input.GetMouseButtonDown(0) || Input.GetKeyDown("joystick button 4") || Input.GetKeyDown("joystick button 5")))
        {
            color = Other.GetComponent<MeshRenderer>().material.color;
            if ((Other.gameObject.GetComponent("Slime") as Slime) != null)
            {
                playerTarget = Other.gameObject;
                //Invoke("Flasher", flashTime);
                enable_flash = true;

                rig = Other.gameObject.GetComponent<Rigidbody>();
                rig.velocity -= transform.forward * -400 * Time.deltaTime;


                slimeScript = Other.GetComponent<Slime>();
                slimeScript.health--;

                if (slimeScript.health == 0)
                {
                    slimeScript.currentStage++;
                    slimeScript.dead = true;
                }


            }
        }
    }

    // Use this for initialization
    void Start ()
    {
		
	}
	
	// Update is called once per frame
	void Update ()
    {
		if (enable_flash)
        {
            if(flashTime == flashLength)
            {
                playerTarget.GetComponent<MeshRenderer>().material.color = color;
                flashTime = 0;
                enable_flash = false;
                
            }

            else
            {
                flashTime++;
                playerTarget.GetComponent<MeshRenderer>().material.color = Color.red;
            }

        }
	}
}
