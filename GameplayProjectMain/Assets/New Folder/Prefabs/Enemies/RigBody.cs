using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RigBody : MonoBehaviour
{
    public Rigidbody rigBody;
    // Use this for initialization
    void Start ()
    {
        rigBody = GetComponent<Rigidbody>();
    }
	
	// Update is called once per frame
	void Update ()
    {
		if(Input.GetMouseButtonUp(0))
        {
            rigBody.AddForce(5, 5, 0, ForceMode.Impulse);
        }
	}
}
