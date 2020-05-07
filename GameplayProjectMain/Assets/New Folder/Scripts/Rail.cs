using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rail : MonoBehaviour {

    [SerializeField]
    private Vector3[] nodes;

    private int node_count;

    public Camera PlayerOrigCam;
    public Camera RailCam;

    private void Start()
    {
        node_count = transform.childCount;
        nodes = new Vector3[node_count];
        //RailCam.enabled = false;
    
        for (int i = 0; i < node_count; i++)
        {
            nodes[i] = transform.GetChild(i).position;
        }
    }

    private void Update()
    {
        PlayerOrigCam.enabled = true;
        RailCam.enabled = false;

        if (node_count > 1)
        {
            for (int i = 0; i < node_count - 1; i++)
            {
                Debug.DrawLine(nodes[i], nodes[i + 1], Color.red);
            }
        }

    }

    public Vector3 ProjectPositionOnRail(Vector3 position)
    {
            int closest_node_index = GetClosestNode(position);

            if (closest_node_index == 0) //If on first node
            {
                return ProjectOnSegment(nodes[0], nodes[1], position);
            }
            else if (closest_node_index == node_count - 1)
            {
                return ProjectOnSegment(nodes[node_count - 1], nodes[node_count - 2], position);
            }
            else
            {
                Vector3 left_segment = ProjectOnSegment(nodes[closest_node_index - 1], nodes[closest_node_index], position);
                Vector3 right_segment = ProjectOnSegment(nodes[closest_node_index + 1], nodes[closest_node_index], position);

                Debug.DrawLine(position, left_segment, Color.blue);
                Debug.DrawLine(position, right_segment, Color.yellow);

                if ((position - left_segment).sqrMagnitude <= (position - right_segment).sqrMagnitude)
                {
                    return left_segment;
                }
                else
                {
                    return right_segment;
                }
            }
    }

    private int GetClosestNode(Vector3 position)
    {
        int closest_node_index = -1;

        float shortest_distance = 0.0f;

        for (int i = 0; i < node_count; i++)
        {
            float square_distance = (nodes[i] - position).sqrMagnitude;

            if (shortest_distance == 0.0f || square_distance < shortest_distance)
            {
                shortest_distance = square_distance;
                closest_node_index = i;
            }
        }

            return closest_node_index;
    }

    private Vector3 ProjectOnSegment(Vector3 v1, Vector3 v2, Vector3 position)
    {
        //Helps to apply the dot product in order to get the intersection point.
        Vector3 v1_to_pos = position - v1;
        Vector3 seg_direction = (v2 - v1).normalized;

        float distance_from_v1 = Vector3.Dot(seg_direction, v1_to_pos);

        if (distance_from_v1 < 0.0f)
        {
            return v1;
        }
        else if (distance_from_v1 * distance_from_v1 > (v2 - v1).sqrMagnitude)
        {
            return v2;
        }
        else
        {
            //Gives exact distance from vector 1 to where we want to be.
            Vector3 from_v1 = seg_direction * distance_from_v1;

            return v1 + from_v1;
        }
    }
}
