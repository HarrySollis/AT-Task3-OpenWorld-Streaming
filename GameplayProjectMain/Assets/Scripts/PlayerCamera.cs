//using System.Collections;
//using System.Collections.Generic;
//using UnityEngine;

//public class PlayerCamera : MonoBehaviour {

//    public Transform player_target;

//    public Transform camera_rotate_target;

//    public float look_smoothing = 0.09f;

//    public Vector3 offset_from_player = new Vector3(0, 6, -8);

//    //Enables looking down on the target.
//    public float x_axis_tilt = 10;

//    Vector3 camera_destination = Vector3.zero;

//    //CharacterControllerScript player_controller;

//    float rotation_velocity = 10;

//    void Start()
//    {
//        float mouse_x = Input.GetAxis("Mouse X");

//        SetCameraTarget(player_target);
//    }

//    void Update()
//    {
        
//    }

//    void SetCameraTarget(Transform main_target)
//    {
//        player_target = main_target;

//        // Sanity Checks that there is actually a player target for the camera functionality to focus on
//        if (player_target != null)
//        {
//            if (player_target.GetComponent<CharacterControllerScript>())
//            {
//                player_controller = player_target.GetComponent<CharacterControllerScript>();
//            }
//            else
//            {
//                Debug.LogError("Camera's player target needs a character controller!");
//            }
//        }
//        else
//        {
//            //If not, release a error log statement to the debug console.
//            Debug.LogError("Camera needs a target to focus on!");
//        }
//    }

//    //ensures movement occurs after the most previous movements of the player.
//    void LateUpdate()
//    {

//        MoveToTarget();
//        LookingAtTarget();

//        //CameraRotateLogic();

//    }

//    void MoveToTarget()
//    {
//        camera_destination = player_controller.PlayerRotation * offset_from_player;
//        camera_destination += player_target.position;
//        transform.position = camera_destination;
//    }

//    void LookingAtTarget()
//    {
//        // Like Lerp but smoother.
//        float euler_y_angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, player_target.eulerAngles.y, ref rotation_velocity, look_smoothing);
//        transform.rotation = Quaternion.Euler(transform.eulerAngles.x, euler_y_angle, 0);
//    }

//    void CameraRotateLogic()
//    {
//        Quaternion camera_horizontal_rotation = Quaternion.AngleAxis(Input.GetAxis("Mouse X") * rotation_velocity, Vector3.up);

//        offset_from_player = camera_horizontal_rotation * offset_from_player;

//        Vector3 new_position = camera_rotate_target.position + offset_from_player;

//        transform.position = Vector3.Slerp(transform.position, new_position, look_smoothing);

//        transform.LookAt(player_target);
//    }
//}
