using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class HitDetect : MonoBehaviour
{
    public bool Hit;

    public bool Attacking;

    public float timer = 0f;

    void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<EnemyController>() != null && Attacking && !Hit)
        {

            other.GetComponent<EnemyController>().Damage(1);
            other.GetComponent<NavMeshAgent>().enabled = false;
            other.GetComponent<EnemyController>().rB.AddForce(0, 7, 4f, ForceMode.Impulse);
            Hit = true;

            //timer = 1f / Time.fixedDeltaTime;
        }

    }

    void FixedUpdate()
    {
        //if(Hit)
        //{
           HitTimer();
        //}
    }

    public void HitTimer()
    {
        if (timer > 0)
        {
            timer--;
        }
        if(timer <= 0)
        {
            Attacking = false;
        }
    }
}
