using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Respawn : MonoBehaviour
{
    public Transform User;
    public Transform TeleLocation;
    public GameObject ObjectToTeleport;

    // Use this for initialization
    void Start ()
    {
		
	}
	
	// Update is called once per frame
	void Update ()
    {
		
	}

    void OnTriggerEnter(Collider hit)
    {
        if (hit.gameObject.CompareTag("Player"))
        {
            hit.GetComponent<PlayerController>().camSettings.mainCam.transform.parent = null;

            StartCoroutine(Respawner());
        }
    }


    IEnumerator Respawner()
    {
        ObjectToTeleport.GetComponent<PlayerController>().camSettings.mainCam.transform.LookAt(User);
        yield return new WaitForSeconds(2);

        ObjectToTeleport.GetComponent<PlayerController>().camSettings.mainCam.transform.parent = GameObject.Find("Camera Rotator Base").transform;
        ObjectToTeleport.transform.SetPositionAndRotation(TeleLocation.transform.position, TeleLocation.transform.rotation);
    }
}
