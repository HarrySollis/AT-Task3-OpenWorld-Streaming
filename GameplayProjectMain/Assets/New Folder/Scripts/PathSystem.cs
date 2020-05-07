using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PathSystem : MonoBehaviour
{
    public Rigidbody playerRigid;
    PlayerController Player;

    public bool cameraEnabled;
    public Transform[] target;
    public float moveSpeed;

    public int currentPos;

    // Use this for initialization
    void Start()
    {
        GetComponent<Rigidbody>().transform.SetPositionAndRotation(target[0].position, target[0].rotation);
        cameraEnabled = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (cameraEnabled == true)
        {
            if (Input.GetKey("d") || Input.GetKey("w") || (Input.GetAxis("Vertical") > 0))
            {
                if (currentPos < target.Length - 1 && transform.position == target[currentPos].position)
                {
                    currentPos = currentPos + 1 % target.Length;
                }

                Vector3 pos = Vector3.MoveTowards(transform.position, target[currentPos].position, moveSpeed * Time.deltaTime);
                GetComponent<Rigidbody>().MovePosition(pos);
            }

            if (Input.GetKey("a") || Input.GetKey("s") || (Input.GetAxis("Vertical") < 0))
            {
                if (currentPos > 0 && transform.position == target[currentPos].position)
                {
                    currentPos = currentPos - 1 % target.Length;
                }

                Vector3 pos = Vector3.MoveTowards(transform.position, target[currentPos].position, moveSpeed * Time.deltaTime);
                GetComponent<Rigidbody>().MovePosition(pos);
            }
        }
    }
}
