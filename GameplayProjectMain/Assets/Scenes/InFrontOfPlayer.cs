using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InFrontOfPlayer : MonoBehaviour
{
    public Switch SwitchToUseCheck;
    void OnTriggerEnter(Collider trigger)
    {
        if (trigger.gameObject.CompareTag("Switch"))
        {
            SwitchToUseCheck.playerLooking = true;
        }

        else
        {
            SwitchToUseCheck.playerLooking = false;
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
