using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerRotate : MonoBehaviour
{
    public GameObject PlayerCam;
    //public GameObject Player;
    public FinalPlayerCamera PlayerCameraScript;
    public int RotaionValue;
    public int ReverseRotaionValue;

    void OnTriggerStay(Collider hit)
    {
        if (hit.gameObject.CompareTag("Player"))
        {
            if (Input.GetKey("w") || Input.GetAxis("Vertical") > 0)
            {
                PlayerCam.transform.rotation = Quaternion.Euler(0, RotaionValue, 0);
            }

            else if (Input.GetKey("s") || Input.GetAxis("Vertical") < 0)
            {
                PlayerCam.transform.rotation = Quaternion.Euler(0, ReverseRotaionValue, 0);
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
		
	}
}
