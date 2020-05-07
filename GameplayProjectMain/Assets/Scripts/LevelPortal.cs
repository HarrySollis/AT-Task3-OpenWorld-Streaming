using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class LevelPortal : MonoBehaviour
{
    public string level;

    // Use this for initialization
    void Start()
    {

    }

    void OnTriggerEnter(Collider collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            SceneManager.LoadScene(level);
        }
    }

        // Update is called once per frame
        void Update ()
    {

    }
}
