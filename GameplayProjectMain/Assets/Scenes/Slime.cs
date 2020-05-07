using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Slime : MonoBehaviour
{
    public GameObject stage1;
    public GameObject stage2;
    public GameObject coinDrop;

    bool shrink = false;

    int counter = 0;

    public GameObject Object;
    public int currentStage = 0;
    public bool dead = false;
    GameObject Clone;

    public int moveSpeed;
    public int rotationSpeed;
    public int health = 6;
    public Transform target;
    public Rigidbody targetRigid;
    private Transform myTransform;

    bool isGrounded = false;
    bool enableJump = false;

    void OnCollisionEnter(Collision hit)
    {
        if (hit.gameObject.CompareTag("Ground"))
        {
            isGrounded = true;
            enableJump = true;
        }

        if (hit.gameObject.CompareTag("Player"))
        {
            targetRigid.velocity -= transform.forward * 400 * Time.deltaTime;
            //targetRigid.AddRelativeForce(targetRigid.transform.InverseTransformDirection(-this.transform.position) * -10);
            //targetRigid.velocity.z += Vector3.forward * 6;
            //targetRigid.velocity = new Vector3(0, 0, 6);
        }
    }

    void Awake()
    {
        myTransform = transform;
    }

    // Use this for initialization
    void Start()
    {
        GameObject go = GameObject.FindGameObjectWithTag("Player");


        target = go.transform;


    }

    // Update is called once per frame
    void Update()
    {
        if (counter != 45)
        {
            counter++;
            if (!shrink)
            {
                transform.localScale += new Vector3(0.3F, 0.3F, 0.3F);
            }

            else
            {
                transform.localScale -= new Vector3(0.3F, 0.3F, 0.3F);
            }
        }

        else if (counter == 45)
        {
            counter = 0;

            if (!shrink)
            {
                shrink = true;
            }

            else
            {
                shrink = false;
            }
        }

        if (dead == true)
        {
            if (currentStage == 1)
            {
                dead = false;
                Clone = (GameObject)Instantiate(Object, myTransform.position, myTransform.rotation);
                Slime CloneScript = Clone.GetComponent<Slime>();
                CloneScript.health = 2;
                Clone.transform.localScale = new Vector3(40f, 40f, 40f);

                Clone = (GameObject)Instantiate(Object, myTransform.position, myTransform.rotation);
                CloneScript = Clone.GetComponent<Slime>();
                CloneScript.health = 2;
                Clone.transform.localScale = new Vector3(40f, 40f, 40f);
                gameObject.SetActive(false);
            }

            if (currentStage == 2)
            {
                dead = false;
                Clone = (GameObject)Instantiate(Object, myTransform.position, myTransform.rotation);
                Slime CloneScript = Clone.GetComponent<Slime>();
                CloneScript.health = 1;
                Clone.transform.localScale = new Vector3(20f, 20f, 20f);

                Clone = (GameObject)Instantiate(Object, myTransform.position, myTransform.rotation);
                CloneScript = Clone.GetComponent<Slime>();
                CloneScript.health = 1;
                Clone.transform.localScale = new Vector3(20f, 20f, 20f);
                gameObject.SetActive(false);
            }
        }

        float distance = Vector3.Distance(target.transform.position, transform.position);

        Debug.Log(distance);

        if (isGrounded == true)
        {
            targetRigid.velocity = new Vector3(0, 3, 0);
            isGrounded = false;
        }

        if (distance < 10)
        {

            if (isGrounded == false)
            {
                Debug.DrawLine(target.position, myTransform.position, Color.red);

                //look at target/rotate
                myTransform.rotation = Quaternion.Slerp(myTransform.rotation, Quaternion.LookRotation(target.position - myTransform.position), rotationSpeed * Time.deltaTime);

                //move towards target
                myTransform.position += myTransform.forward * moveSpeed * Time.deltaTime;
            }
        }

        else
        {

            Vector2 Temp = Random.insideUnitCircle;
            float TempDistance = Random.Range(0, 10);

            Temp = Temp * TempDistance;
            Vector3 FinalTemp = new Vector3(Temp.x, 0, Temp.y);

            Vector3 MovePos = myTransform.transform.position + FinalTemp;
        }

        if (health == 0 || health < 0)
        {
            Clone = (GameObject)Instantiate(coinDrop, myTransform.position, myTransform.rotation);
            gameObject.SetActive(false);
        }

        }
    }


