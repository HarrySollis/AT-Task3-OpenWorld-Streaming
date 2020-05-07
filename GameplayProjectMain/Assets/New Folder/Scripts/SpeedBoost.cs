using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpeedBoost : MonoBehaviour
{
    public Camera PlayerCamera;
    public TrailRenderer PlayerTrails1;
    public TrailRenderer PlayerTrails2;
    public PlayerController player;
    public int BoostSpeed = 15;
    public int Duration = 100;
    public bool BoostPickedUp = false;
    private int counter = 0;
    

    // Use this for initialization
    void Start ()
    {
        PlayerTrails1.enabled = false;
        PlayerTrails2.enabled = false;
    }

    // Update is called once per frame
    void Update ()
    {
        if (BoostPickedUp)
        {
            PlayerCamera.fieldOfView = 90;
            player.moveSettings.forwardVel = 8;
            PlayerTrails1.enabled = true;
            PlayerTrails2.enabled = true;
            counter++;

            if (counter > Duration)
            {
                counter = 0;
                PlayerCamera.fieldOfView = 75;
                player.moveSettings.forwardVel = 4;
                PlayerTrails1.enabled = false;
                PlayerTrails2.enabled = false;
                BoostPickedUp = false;
            }
        }
    }
}
