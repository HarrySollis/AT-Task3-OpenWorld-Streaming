using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CamLookAt : MonoBehaviour
{
    public bool LookAtPlayer = false;
    public Transform PlayerTransform;
    public Camera CamObject;
    // Use this for initialization
    void Start()
    {
        CamObject.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (LookAtPlayer)
        {
            transform.LookAt(PlayerTransform);
        }
    }
}
