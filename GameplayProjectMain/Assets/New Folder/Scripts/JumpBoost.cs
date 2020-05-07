using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JumpBoost : PowerUp
{
    //public Camera PlayerCamera;
    public PlayerController player;
    public TrailRenderer PlayerTrails1;
    public TrailRenderer PlayerTrails2;

    public ParticleSystem particlePickup;
    private ParticleSystem.EmissionModule particlePlay;
    //public int BoostSpeed = 15;
    //public int Duration = 100;
    public bool DoubleJump;
    //private int counter = 0;


    public override void Pickup(Collider player)
    {
        particlePickup.Play();
        var temp = Instantiate(pickupEffect, transform.position, transform.rotation);
        ActivateJump(duration, player);
        PlayerTrails1.enabled = false;
        PlayerTrails2.enabled = false;
        Destroy(temp, 1);
        gameObject.SetActive(false);
    }

    // Update is called once per frame
    void ActivateJump(float dur, Collider player)
    {
        //if (PickedUp)
        //{
        PlayerTrails1.enabled = true;
        PlayerTrails2.enabled = true;
        Debug.Log("DJ");
        PlayerController JumpAbil = player.GetComponent<PlayerController>();
        JumpAbil.powerupSettings.abilityType = "DoubleJump";
        DoubleJump = true;
        //PlayerCamera.fieldOfView = 90;
        //counter++;            

        JumpAbil.moveSettings.jumpCount = 2;
        JumpAbil.powerupSettings.abilityTimer = dur / Time.deltaTime;
        if(JumpAbil.powerupSettings.abilityTimer == 0)
        {
            PlayerTrails1.enabled = true;
            PlayerTrails2.enabled = true;
        }
        //if (counter > Duration)
        //{
        //    counter = 0;
        //    PlayerCamera.fieldOfView = 75;
        //    PickedUp = false;
        //}
        //}
    }
}
