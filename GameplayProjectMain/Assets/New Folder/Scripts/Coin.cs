using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coin : MonoBehaviour
{
    bool floating = false;
    int counter = 0;
    public int spinSpeed = 15;

    // Use this for initialization
    void Start ()
    {

	}

    void OnTriggerEnter(Collider col)
    {
        if (col.gameObject.CompareTag("Player"))
        {
            Destroy(this.gameObject);
        }
    }
    // Update is called once per frame
    void Update ()
    {
        if (floating == false)
        {
            transform.Translate(0, 0.3f * Time.deltaTime, 0);
            counter++;
            if (counter > 45)
            {
                floating = true;
                counter = 0;
            }
        }

        if (floating == true)
        {
            transform.Translate(0, -0.3f * Time.deltaTime, 0);
            counter++;
            if (counter > 45)
            {
                floating = false;
                counter = 0;
            }
        }




        gameObject.transform.Rotate(0, spinSpeed * Time.deltaTime, 0);

    }
}
