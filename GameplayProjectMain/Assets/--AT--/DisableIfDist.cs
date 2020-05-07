using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DisableIfDist : MonoBehaviour
{
    private GameObject chunkActivatorObject;
    private ChunkActivator activationScript;

    // Start is called before the first frame update
    void Start()
    {
        chunkActivatorObject = GameObject.Find("ChunkActivatorObj");
        activationScript = chunkActivatorObject.GetComponent<ChunkActivator>();

        StartCoroutine("AddToList");
    }

    IEnumerator AddToList()
    {
        yield return new WaitForSeconds(0.01f);

        //activationScript.activatorChunks.Add(new ActivatorChunk { chunk = this.gameObject, chunkPos = transform.position });
    }
}
