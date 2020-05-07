using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlimeController : MonoBehaviour
{
    int currentStage = 0;
    public GameObject stage1Slime;

    GameObject Clone;


    Slime mainSlimeScript;
    public int currentHealth = 99;
    public GameObject NearPlayerTrigger;
    public bool NearPlayer = false;

    public GameObject Stage2_1;
    public GameObject Stage2_2;
    public GameObject Stage2_3;
    public GameObject Stage2_4;

    public Transform target;
    public GameObject targetGameObj;
    public Rigidbody targetRigid;

    // Use this for initialization
    void Start ()
    {
        Stage2_1.SetActive(false);
        Stage2_2.SetActive(false);
        Stage2_3.SetActive(false);
        Stage2_4.SetActive(false);
    }
	
	// Update is called once per frame
	void Update ()
    {
        mainSlimeScript = targetGameObj.gameObject.GetComponent<Slime>();

        if (currentStage == 1)
        {
            stage1Slime.SetActive(true);

        }

        if (Input.GetMouseButtonDown(0))
        {
            //currentHealth--;
            if (mainSlimeScript.health == 4)
            {
                Stage2_1.SetActive(true);
                Stage2_2.SetActive(true);
                Stage2_1.transform.position = targetRigid.position;
                Stage2_2.transform.position = targetRigid.position;
                targetGameObj.SetActive(false);
            }

            if (currentHealth == 2)
            {
                Stage2_3.SetActive(true);
                Stage2_4.SetActive(true);
                Stage2_3.transform.position = Stage2_1.transform.position;
                Stage2_4.transform.position = Stage2_2.transform.position;
                Stage2_1.SetActive(false);
                Stage2_2.SetActive(false);
            }

            if (currentHealth == 0)
            {
                targetGameObj.SetActive(false);
                Stage2_1.SetActive(false);
                Stage2_2.SetActive(false);
                Stage2_3.SetActive(false);
                Stage2_4.SetActive(false);
            }
        }
    }
}
