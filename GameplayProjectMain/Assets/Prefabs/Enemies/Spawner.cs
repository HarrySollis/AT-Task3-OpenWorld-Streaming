using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    public Transform spawnPos;
    public GameObject spawnee;
    public GameObject enemy;
    public Vector3 currentLoc = Vector3.zero;
    public Quaternion currentRot;

    public bool firstWave;
    public bool secondWave;
    public bool thirdWave;
    public bool fourthWave;

    public bool isSpawned;
    public bool spawn;

    private void Start()
    {
        firstWave = true;
    }
    // Update is called once per frame
    void Update()
    {
        Spawn();
    }

    public List<GameObject> enemies = new List<GameObject>();

    void Spawn()
    { 
        if (firstWave == true)
        {
            GameObject go1 = (GameObject)Instantiate(spawnee, spawnPos.position, spawnPos.rotation);
            go1.gameObject.tag = "Wave1";
            firstWave = false;
        }
        if(secondWave == true && GetComponent<EnemyStats>().Lives == 3)
        {
            GameObject go1 = (GameObject)Instantiate(spawnee, currentLoc + (new Vector3(1, 0, 0)), currentRot);
            go1.gameObject.tag = "Wave2";
            go1.GetComponent<EnemyController>().maxHealth = 2;
            go1.transform.localScale = go1.transform.localScale * 0.85f;
            GameObject go2 = (GameObject)Instantiate(spawnee, currentLoc + (new Vector3(-1, 0, 0)), currentRot);
            go2.gameObject.tag = "Wave2";
            go2.GetComponent<EnemyController>().maxHealth = 2;
            go2.transform.localScale = go2.transform.localScale * 0.85f;
            secondWave = false;
        }
        //if(thirdWave == true && GetComponent<EnemyStats>().Lives == 2)
        //{
        //    GameObject go1 = (GameObject)Instantiate(spawnee, currentLoc + (new Vector3(1, 0, 0)), currentRot);
        //    go1.gameObject.tag = "Wave3";
        //    go1.GetComponent<EnemyController>().maxHealth = 2;
        //    go1.transform.localScale = go1.transform.localScale * 0.85f;
        //    GameObject go2 = (GameObject)Instantiate(spawnee, currentLoc + (new Vector3(-1, 0, 0)), currentRot);
        //    go2.gameObject.tag = "Wave3";
        //    go2.GetComponent<EnemyController>().maxHealth = 2;
        //    go2.transform.localScale = go2.transform.localScale * 0.85f;
        //    thirdWave = false;
        //}
        //if(fourthWave == true)
        //{
        //    GameObject go1 = (GameObject)Instantiate(spawnee, currentLoc + (new Vector3(1, 0, 0)), currentRot);
        //    go1.gameObject.tag = "Wave4";
        //    go1.GetComponent<EnemyController>().maxHealth = 2;
        //    go1.transform.localScale = go1.transform.localScale * 0.85f;
        //    GameObject go2 = (GameObject)Instantiate(spawnee, currentLoc + (new Vector3(-1, 0, 0)), currentRot);
        //    go2.gameObject.tag = "Wave4";
        //    go2.GetComponent<EnemyController>().maxHealth = 2;
        //    go2.transform.localScale = go2.transform.localScale * 0.85f;
        //    fourthWave = false;
        //}








       if (thirdWave == true)
       {
           GameObject go1 = (GameObject)Instantiate(spawnee, currentLoc + (new Vector3(2, 0, 0)), currentRot);
           go1.gameObject.tag = "Wave3";
           go1.GetComponent<EnemyController>().maxHealth = 1;
           go1.transform.localScale = go1.transform.localScale * 0.65f;
           GameObject go2 = (GameObject)Instantiate(spawnee, currentLoc + (new Vector3(1, 0, 0)), currentRot);
           go2.gameObject.tag = "Wave3";
           go2.GetComponent<EnemyController>().maxHealth = 1;
           go2.transform.localScale = go2.transform.localScale * 0.65f;
           GameObject go3 = (GameObject)Instantiate(spawnee, currentLoc + (new Vector3(-1, 0, 0)), currentRot);
           go3.gameObject.tag = "Wave3";
           go3.GetComponent<EnemyController>().maxHealth = 1;
           go3.transform.localScale = go3.transform.localScale * 0.65f;
           GameObject go4 = (GameObject)Instantiate(spawnee, currentLoc + (new Vector3(-2, 0, 0)), currentRot);
           go4.gameObject.tag = "Wave3";
           go4.GetComponent<EnemyController>().maxHealth = 1;
           go4.transform.localScale = go4.transform.localScale * 0.65f;
           thirdWave = false;
       }









        //if (GetComponent<EnemyStats>().Lives == 3 && isSpawned)
        //{
        //    GetComponent<EnemyStats>().Lives = 3;
        //    GameObject go1 = (GameObject)Instantiate(spawnee, currentLoc + (new Vector3(1, 0, 0)), currentRot);
        //    go1.GetComponent<EnemyController>().maxHealth = 2;
        //    go1.transform.localScale = go1.transform.localScale * 0.85f;
        //    GameObject go2 = (GameObject)Instantiate(spawnee, currentLoc + (new Vector3(-1, 0, 0)), currentRot);
        //    go2.GetComponent<EnemyController>().maxHealth = 2;
        //    go2.transform.localScale = go2.transform.localScale * 0.85f;
        //    isSpawned = false;
        //    spawn = false;
        //}
        //if (GetComponent<EnemyStats>().Lives == 2 && isSpawned)
        //{
        //    GetComponent<EnemyStats>().Lives = 2;
        //    GameObject go1 = (GameObject)Instantiate(spawnee, currentLoc + (new Vector3(1, 0, 0)), currentRot);
        //    go1.GetComponent<EnemyController>().maxHealth = 1;
        //    go1.transform.localScale = go1.transform.localScale * 0.65f;
        //    go1.gameObject.tag = "FinalWave";
        //    GameObject go2 = (GameObject)Instantiate(spawnee, currentLoc + (new Vector3(-1, 0, 0)), currentRot);
        //    go2.GetComponent<EnemyController>().maxHealth = 1;
        //    go2.gameObject.tag = "FinalWave";
        //    go2.transform.localScale = go2.transform.localScale * 0.65f;
        //    isSpawned = false;
        //    spawn = false;
        //}

            //if (GetComponent<EnemyStats>().Lives == 1 && !spawn)
            //{
            //    GetComponent<EnemyStats>().Lives = 1;
            //    GameObject go1 = (GameObject)Instantiate(spawnee, currentLoc + (new Vector3(1, 0, 0)), currentRot);
            //    go1.GetComponent<EnemyController>().maxHealth = 1;
            //    go1.transform.localScale = go1.transform.localScale * 0.65f;
            //    GameObject go2 = (GameObject)Instantiate(spawnee, currentLoc + (new Vector3(-1, 0, 0)), currentRot);
            //    go2.GetComponent<EnemyController>().maxHealth = 1;
            //    go2.gameObject.tag = "FinalWave";
            //    go2.transform.localScale = go2.transform.localScale * 0.65f;
            //    spawn = true;
            //}
    }
}
