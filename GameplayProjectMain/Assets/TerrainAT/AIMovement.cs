using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIMovement : MonoBehaviour
{
    // Start is called before the first frame update

    private List<GameObject> waypoints;
    
    private GameObject NPC;
    [SerializeField]
    //private LevelGenerator level;

    public float Speed = 1f;
    private float distance;
    private float startTime;
    bool running = false;
    //void Start()
    //{
    //    waypoints = new List<GameObject>();
    //    startTime = Time.time;
    //    float movement = (Speed * Time.deltaTime) / 1000;
    //    //level = GameObject.Find("Level").GetComponent<LevelGenerator>();
    //    for (int i = 0; i < level.pointsList.Count; i++)
    //    {
    //
    //        waypoints.Add(level.pointsList[i]);
    //       
    //      
    //    }
    //
    //}

    // Update is called once per frame
    void Update()
    {
     
        GameObject nextTarget;
        bool moving = false;
        nextTarget = (waypoints[SelectPoint(0)]);
        Vector3 compareme = new Vector3 (nextTarget.transform.position.x + 10, nextTarget.transform.position.y + 10, nextTarget.transform.position.z + 10);


        // if (moving)
            distance = Vector3.Distance(this.transform.position, nextTarget.transform.position);
            float distCovered = ((Time.time - startTime) * Speed);
        float fractOfDistance = distCovered / distance;
        this.transform.position = Vector3.Lerp(nextTarget.transform.position, nextTarget.transform.position, fractOfDistance);

      

        if (this.transform.position == compareme)
        {
            nextTarget = waypoints[SelectPoint(waypoints.Count)].GetComponent<GameObject>();
        }


    }



   private int SelectPoint(int pastPoint)
    {

       
        int nextPoint = Random.Range(0, waypoints.Count);


        if (nextPoint != pastPoint)
        {
            
            return nextPoint;
        }

        else
        {
            return nextPoint + 5;
        }
    }
}
