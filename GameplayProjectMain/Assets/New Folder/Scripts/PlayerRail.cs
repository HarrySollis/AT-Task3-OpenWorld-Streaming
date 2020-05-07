using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerRail : MonoBehaviour
{
    public bool enablePlayerRail = false;
    public Transform[] target;
    public Rigidbody playerRigid;
    public float moveSpeed;
    public float smoothSpeed;

    public int currentPos;

    // Use this for initialization
    void Start ()
    {

    }
	
	// Update is called once per frame
	void Update ()
    {
    
        if (enablePlayerRail == true)
        {
            var lookPoint = target[currentPos].position;
            lookPoint = new Vector3(lookPoint.x, transform.position.y, lookPoint.z);

            var lookDir = (lookPoint - transform.position);
            smoothSpeed = Mathf.Abs(100) * (Vector3.Distance(transform.forward, lookDir) / 5000);
            Vector3 smoothedDir = Vector3.Lerp(transform.forward, lookDir, smoothSpeed);


            var lookRotation = Vector3.Distance(Quaternion.LookRotation(transform.forward, transform.up).eulerAngles, Quaternion.LookRotation(lookDir, transform.up).eulerAngles) > 160f ? Quaternion.LookRotation(lookDir, transform.up) : Quaternion.LookRotation(smoothedDir, transform.up);

            transform.rotation = lookRotation;



/*
 

            if (Input.GetKey("d"))
            {
                if (currentPos < target.Length - 1 && transform.position == target[currentPos].position)
                {
                    currentPos = currentPos + 1 % target.Length;
                }

                Vector3 pos = Vector3.MoveTowards(transform.position, target[currentPos].position, moveSpeed * Time.deltaTime);
                playerRigid.transform.LookAt(target[currentPos]);
            }

            if (Input.GetKey("a"))
            {
                if (currentPos > 0 && transform.position == target[currentPos].position)
                {
                    currentPos = currentPos - 1 % target.Length;
                }

                Vector3 pos = Vector3.MoveTowards(transform.position, target[currentPos].position, moveSpeed * Time.deltaTime);
                playerRigid.transform.LookAt(target[currentPos].y);
            }

    */
        }
    }
}

