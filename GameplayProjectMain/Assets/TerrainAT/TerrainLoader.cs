using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TerrainLoader : MonoBehaviour
{
    public GameObject terrain;
    private string tag;
    MeshRenderer rend;

    // Start is called before the first frame update
    void Start()
    {
        tag = terrain.tag;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            terrain.SetActive(true);
        }
    }
}
