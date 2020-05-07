using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class NPCNavMesh : MonoBehaviour
{
    bool isWandering, isWalking, isFleeing;

    Animator anim;

    public GameObject player;

    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
        player = GameObject.FindGameObjectWithTag("Player");
    }

    // Update is called once per frame
    void Update()
    {
        if (Vector3.Distance(transform.position, player.transform.position) <= 4 && !isWandering)
        {
            StartCoroutine(Flee());
        }
        //GetComponent<NavMeshAgent>().SetDestination(GameObject.FindGameObjectWithTag("Player").transform.position);
        if (!isWandering && !isFleeing)
        {
            StartCoroutine(Wander());
        }

       

    }

    IEnumerator Wander()
    {
        isWandering = true;

        GetComponent<NavMeshAgent>().SetDestination(new Vector3(transform.position.x + Random.Range(-10, 10), transform.position.y, transform.position.z + Random.Range(-10, 10)));

        anim.SetBool("Move Forwards", true);
        yield return new WaitUntil(delegate () { return GetComponent<NavMeshAgent>().velocity.magnitude < 1; });
        anim.SetBool("Move Forwards", false);

        isWandering = false;
    }


    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            player = other.gameObject;
            StartCoroutine(Flee());
            //transform.Rotate((transform.up * -360) * Time.deltaTime * rotSpeed);
        }
    }

    IEnumerator Flee()
    {
        isFleeing = true;

        isWalking = true;

        GetComponent<NavMeshAgent>().SetDestination((transform.position * 2) - player.transform.position);
        anim.SetBool("Move Forwards", true);
        yield return new WaitWhile(delegate () { return Vector3.Distance(player.transform.position, transform.position) < 4; });
        isWalking = false;

        anim.SetBool("Move Forwards", false);


        isFleeing = false;
        //yield return new WaitForSeconds(0.01f);
    }
}
