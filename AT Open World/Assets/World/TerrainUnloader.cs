using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TerrainUnloader : MonoBehaviour
{
    public GameObject terrain;
    private string tag;
    MeshRenderer render;


    // Start is called before the first frame update
    void Start()
    {
        tag = terrain.tag; 
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Player")
        {
            terrain.SetActive(false);
        }
    }
}
