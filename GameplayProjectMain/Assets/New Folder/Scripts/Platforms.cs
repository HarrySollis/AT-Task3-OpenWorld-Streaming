using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Platforms : MonoBehaviour
{
    public bool MoveForward = false;
    public bool MoveBackwards = false;
    public bool MoveLeft = false;
    public bool MoveRight = false;
    public int duration = 0;
    private int current_stage = 0;

    public bool Complex = false;

    public bool floating = false;
    int counter = 0;
    public float speed = 5;

    void OnCollisionEnter(Collision hit)
    {
        if (hit.gameObject.CompareTag("Player"))
        {
            hit.transform.parent = this.gameObject.transform;
        }
    }

    void OnCollisionExit(Collision hit)
    {
        if (hit.gameObject.CompareTag("Player"))
        {
            gameObject.transform.DetachChildren();
        }
    }

    // Use this for initialization
    void Start ()
    {
		
	}
	
	// Update is called once per frame
	void Update ()
    {
        if (Complex == false)
        {

            if (floating == false)
            {
                this.gameObject.transform.Translate(speed * Time.deltaTime, 0, 0);
                counter++;
                if (counter > duration)
                {
                    floating = true;
                    counter = 0;
                }
            }

            if (floating == true)
            {
                this.gameObject.transform.Translate(-speed * Time.deltaTime, 0, 0);
                counter++;
                if (counter > duration)
                {
                    floating = false;
                    counter = 0;
                }
            }
        }

        else
        {
            if (current_stage == 0)
            {
                this.gameObject.transform.Translate(speed * Time.deltaTime, 0, 0);
                counter++;

                if (counter > duration)
                {
                    current_stage++;
                    counter = 0;
                }
            }

            if (current_stage == 1)
            {
                this.gameObject.transform.Translate(0, speed * Time.deltaTime, 0);
                counter++;

                if (counter > duration)
                {
                    current_stage++;
                    counter = 0;
                }
            }

            if (current_stage == 2)
            {
                this.gameObject.transform.Translate(-speed * Time.deltaTime,0, 0);
                counter++;

                if (counter > duration)
                {
                    current_stage++;
                    counter = 0;
                }
            }

            if (current_stage == 3)
            {
                this.gameObject.transform.Translate(0, -speed * Time.deltaTime, 0);
                counter++;

                if (counter > duration)
                {
                    current_stage=0;
                    counter = 0;
                }
            }



        }

        }
}
