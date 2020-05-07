using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUp : MonoBehaviour
{
   // public ParticleSystem runTrail;
    public GameObject pickupEffect;
    public float multiplier = 2.5f;
    public float duration = 4f;
    public bool PickedUp;

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            other.GetComponent<PlayerController>();
            PickedUp = true;
            Pickup(other);
        }
    }

    public virtual void Pickup(Collider player)
    {
        
    }
}
