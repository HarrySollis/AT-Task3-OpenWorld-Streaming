using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerEnable : MonoBehaviour
{
    public GameObject blockToSpawn;

    void OnTriggerEnter(Collider trigger)
    {
        if (trigger.gameObject.CompareTag("Player"))
        {
            blockToSpawn.SetActive(true);
        }
    }
        // Use this for initialization
        void Start ()
    {
        blockToSpawn.SetActive(false);
    }
	
	// Update is called once per frame
	void Update () {
		
	}
}
