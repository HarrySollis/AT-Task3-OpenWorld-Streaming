using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyWander : MonoBehaviour
{
    public NavMeshAgent agent;
    public GameObject player;

    public enum State
    {
        PATROL,
        CHASE
    }

    public State state;
    private bool alive;

    public GameObject[] waypoints;
    private int waypointInd;
    public float patrolSpeed = 0.5f;

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();

        agent.updatePosition = true;
        agent.updatePosition = false;

        waypoints = GameObject.FindGameObjectsWithTag("Waypoint");
        waypointInd = Random.Range(0, waypoints.Length);

        state = State.PATROL;

        alive = true;

        StartCoroutine("FSM");
    }

    IEnumerator FSM()
    {
        while(alive)
        {
            switch(state)
            {
                case State.PATROL:
                    Patrol();
                    break;
                case State.CHASE:
                    //Chase();
                    break;
            }
            yield return null;
        }
    }

    void Patrol()
    {
        if(Vector3.Distance(transform.position, waypoints[waypointInd].transform.position) >= 2)
        {
            agent.SetDestination(waypoints[waypointInd].transform.position);
        }
        else if(Vector3.Distance(transform.position, waypoints[waypointInd].transform.position) <=2)
        {
            waypointInd = Random.Range(0, waypoints.Length); 
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
