using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyController : MonoBehaviour
{
    public float lookRadius = 10f;
    public float atkRadius = 2f;

    public float force = 1f;
    public Rigidbody rB;

    public int Health = 0, maxHealth = 3;
    public int lives;
    public bool allKilled;
    

    public Transform target;

    public Vector3 wanderTarget = Vector3.zero;

    //public Transform[] patrolPoints;

    public GameObject spawner;

    public NavMeshAgent agent;

	// Use this for initialization
	void Start ()
    {
        spawner = GameObject.Find("Spawner");
        rB = GetComponent<Rigidbody>();
        target = PlayerManager.instance.player.transform;
        agent = GetComponent<NavMeshAgent>();
        //wanderTarget = Random.insideUnitSphere * 5;
        InvokeRepeating("Wander", 1, 4);
        Health = maxHealth;
	}

    public Vector3 knockback = Vector3.zero;

    // Update is called once per frame
    void Update ()
    {
        float distance = Vector3.Distance(target.position, transform.position);
        lives = spawner.GetComponent<EnemyStats>().Lives;

        if (GetComponent<NavMeshAgent>().enabled == false)
        {
            Invoke("Resume", 2f);
        }

        if (distance <= lookRadius)
        {
            agent.SetDestination(target.position);

            if (distance <= agent.stoppingDistance)
            {

            }
        }
        else
        {
            if (transform.position != wanderTarget)
            {
                agent.SetDestination(wanderTarget);
            }
        }

        if (distance <= atkRadius && timer <= 0)
        {
            timer = 3 / Time.fixedDeltaTime;
            knockback = transform.position - target.transform.position;
            knockback = knockback.normalized;

            //knockback *= 35f;

            //knockback.y = 300f;
            //GetComponent<NavMeshAgent>().updatePosition = false;
            GetComponent<NavMeshAgent>().enabled = false;
            rB.AddForce(0, 2, -4, ForceMode.Impulse);
            //target.GetComponent<PlayerController>().npcSettings.rigBody.AddForce(0, 9, -7, ForceMode.Impulse);
            Invoke("Resume", 0.5f);

        }   
    }

    void Resume()
    {
        GetComponent<NavMeshAgent>().enabled = true;
        //GetComponent<NavMeshAgent>().updatePosition = true;
    }

    void Wander()
    {
        wanderTarget = Random.insideUnitSphere * 10;
    }

    private void FixedUpdate()
    {
        AttackTimer();
    }

    public float timer = 0;

    public void AttackTimer()
    {
        if (timer > 0)
        {
            timer--;
        }
    }

    public void Damage(int Amount)
    {
        Health -= Amount;
        if(Health <= 0)
        {
            onDeath();
            Health = maxHealth;
        }
    }

    private void onDeath()
    {       
        if(!spawner.GetComponent<Spawner>().firstWave && spawner.GetComponent<EnemyStats>().Lives == 4)
        {
            //firstWave = false;
            spawner.GetComponent<EnemyStats>().Lives -= 1;
            spawner.GetComponent<Spawner>().secondWave = true;
        }
        if (!spawner.GetComponent<Spawner>().secondWave)
        //{
        //    spawner.GetComponent<EnemyStats>().Lives -= 1;
        //    spawner.GetComponent<Spawner>().thirdWave = true;
        //}
        //if(!spawner.GetComponent<Spawner>().thirdWave)
        //{

        //}

        if (!spawner.GetComponent<Spawner>().secondWave && (spawner.GetComponent<EnemyStats>().Lives == 3 || spawner.GetComponent<EnemyStats>().Lives == 2))
        {
            spawner.GetComponent<EnemyStats>().Lives -= 1;   
        }

        if (spawner.GetComponent<EnemyStats>().Lives == 1)
        {
            spawner.GetComponent<Spawner>().thirdWave = true;
            spawner.GetComponent<EnemyStats>().Lives = 0;
        }


        //if (!spawner.GetComponent<Spawner>().thirdWave)
        //{

        //}

        //spawner.GetComponent<Spawner>().isSpawned = true;

        // spawner.GetComponent<Spawner>().enemies.RemoveAt(spawner.GetComponent<Spawner>().enemies.IndexOf(gameObject));
        spawner.GetComponent<Spawner>().currentLoc = transform.position;
        spawner.GetComponent<Spawner>().currentRot = transform.rotation;
        Destroy(this.gameObject);  
    }

    void FaceTarget()
    {
        Vector3 direction = (target.position - transform.position).normalized;
        Quaternion lookRotation = Quaternion.LookRotation(new Vector3(direction.x, 0, direction.z));
        transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * 5f);
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, lookRadius);

        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, atkRadius);
    }
}
