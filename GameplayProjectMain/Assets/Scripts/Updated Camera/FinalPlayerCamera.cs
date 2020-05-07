using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FinalPlayerCamera : MonoBehaviour {

    public float camera_move_sensitivity = 120.0f;

    public bool onRails = false;

    public bool enableCam = true;

    public GameObject camera_follow_object;

    Vector3 follow_position;

    public float clamp_angle = 80.0f;

    public float input_sensitivity = 450.0f;
    public float zoom_smoothing = 10.0f;

    public GameObject camera_object;

    public GameObject player_object;

    public float camera_distance_x_to_player;
    public float camera_distance_y_to_player;
    public float camera_distance_z_to_player;

    public float mouse_x;
    public float mouse_y;

    public float final_input_x;
    public float final_input_z;

    public float smooth_x;
    public float smooth_y;

    private float rotation_x = 0.0f;
    private float rotation_y = 0.0f;

    // Use this for initialization
    void Start ()
    {
        Vector3 rot = transform.localRotation.eulerAngles;
        rotation_y = rot.y;
        rotation_x = rot.x;

        //Locks the cursor and makes its visibility false, respectively.
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;


    }
	
	// Update is called once per frame
	void Update ()
    {
        //Setup for rotation of sticks.
        if (!onRails)
        {
            if (enableCam == true)
            {
                float input_x = Input.GetAxis("Right Stick Horizontal");
                float input_z = Input.GetAxis("Right Stick Vertical");

                float zoom_input = Input.GetAxis("Mouse ScrollWheel");

                mouse_x = Input.GetAxis("Mouse X");
                mouse_y = Input.GetAxis("Mouse Y");

                final_input_x = input_x + mouse_x;
                final_input_z = input_z + mouse_y;

                rotation_y += final_input_x * input_sensitivity * Time.deltaTime;
                rotation_x += final_input_z * input_sensitivity * Time.deltaTime;

                rotation_x = Mathf.Clamp(rotation_x, -clamp_angle, clamp_angle);

                Quaternion local_rotation = Quaternion.Euler(rotation_x, rotation_y, 0.0f);
                transform.rotation = local_rotation;
            }
        }
    }

    private void LateUpdate()
    {
        CameraUpdater();
        CameraZooming();
    }

    void CameraUpdater()
    {
        //Set target object to follow
        Transform target = camera_follow_object.transform;

        //move towards the gameobject that is the target
        float step = camera_move_sensitivity * Time.deltaTime;
        transform.position = Vector3.MoveTowards(transform.position, target.position, step);
    }

    void CameraZooming()
    {

    }
}
