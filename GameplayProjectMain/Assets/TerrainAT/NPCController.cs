using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCController : MonoBehaviour
{
    public float moveSpeed = 3f;
    public float rotSpeed = 100f;

    public Transform destination;

    public GameObject player;

    public bool lookAt = false;

    private bool isWandering = false;
    private bool isRotatingLeft= false;
    private bool isRotatingRight = false;
    private bool isWalking = false;
    private bool isFleeing = false;

    public Animator anim;

    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        // Wandering
        if (isWandering == false)
        {        
            StartCoroutine(Wander());
        }
        if (isRotatingRight == true)
        {
            transform.Rotate(transform.up * Time.deltaTime * rotSpeed);
        }
        if (isRotatingLeft == true)
        {
            transform.Rotate(transform.up * Time.deltaTime * -rotSpeed);
        }

        // Fleeing
        if (isFleeing == true/* && isRotatingRight == true*/)
        {
            transform.rotation = Quaternion.LookRotation( - new Vector3(player.transform.position.x, transform.position.y, player.transform.position.z) + transform.position);
        }
       

        // Moving Forward
        if (isWalking == true)
        {
            
            //transform.position += transform.forward * moveSpeed * Time.deltaTime;
        }
    }

    IEnumerator Wander()
    {
        float rotTime = Random.Range(0, 3);
        float rotateWait = Random.Range(3, 4);
        int rotateLorR = Random.Range(1, 2);
        float walkWait = Random.Range(1, 4);
        float walkTime = Random.Range(1, 3);

        isWandering = true;

        yield return new WaitForSeconds(walkWait);
        isWalking = true;
        anim.SetBool("Move Forwards", true);
        yield return new WaitForSeconds(walkTime);
        isWalking = false;
        anim.SetBool("Move Forwards", false);
        yield return new WaitForSeconds(rotateWait);
        if(rotateLorR == 1)
        {
            isRotatingRight = true;
            yield return new WaitForSeconds(rotTime);
            isRotatingRight = false;

        }
        if (rotateLorR == 2)
        {
            isRotatingLeft = true;
            yield return new WaitForSeconds(rotTime);
            isRotatingLeft = false;

        }
        isWandering = false;
    }

    
    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player")
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
        

        anim.SetBool("Move Forwards", true);
        yield return new WaitWhile(delegate () { return Vector3.Distance(player.transform.position, transform.position) < 4; });
        isWalking = false;

        anim.SetBool("Move Forwards", false);
  
        
        isFleeing = false;
        yield return new WaitForSeconds(0.01f);
    }

}
