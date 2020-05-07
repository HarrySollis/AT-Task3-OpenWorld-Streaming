using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RailMover : MonoBehaviour {

    public Rail rail;
    public Transform look_at;

    public bool smooth_move = true;
    public float move_speed = 5.0f;

    private Transform camera_transform;
    private Vector3 last_position;

    private void Start()
    {
        camera_transform = this.transform;
        last_position = camera_transform.position;
    }

    private void Update()
    {
        //If smooth moved is activated (true) then smoothly lerp the transition stage from the last position of the camera to another point on the rail.
        if (smooth_move)
        {
            last_position = Vector3.Lerp(last_position, rail.ProjectPositionOnRail(look_at.position), Time.deltaTime * move_speed);
            camera_transform.position = last_position;
        }
        
        //FOR TESTING PURPOSES!
        //camera_transform.position = rail.ProjectOnSegment(Vector3.zero, Vector3.forward * 20, look_at.position);

        camera_transform.position = rail.ProjectPositionOnRail(look_at.position);

        camera_transform.LookAt(look_at.position);
    }
}
