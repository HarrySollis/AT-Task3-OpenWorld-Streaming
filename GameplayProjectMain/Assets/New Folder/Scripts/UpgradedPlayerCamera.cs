//using System.Collections;
//using System.Collections.Generic;
//using UnityEngine;

//public class UpgradedPlayerCamera : MonoBehaviour {

//    public Transform player_target;

//    //public BezierSpline spline;

//    public float progress, duration;

//    float heading = 0f;

//    [System.Serializable]
//    public class PositionSettings
//    {
//        public Vector3 player_position_offset;

//        public float camera_look_smoothing = 100f;

//        public float zoom_smoothing = 10f;

//        public float distance_from_player = -8f;

//        public float minimum_zoom;
//        public float maximum_zoom;

//        public bool smooth_follow = true;
//        public float smoothing = 0.05f;

//        [HideInInspector]
//        public float new_distance = -8;
//        [HideInInspector]
//        public float adjustment_distance = -8;
//    }

//    [System.Serializable]
//    public class DebugSettings
//    {
//        public bool draw_desired_collision_lines = true;
//        public bool draw_adjusted_collision_lines = true;
//    }

//    [System.Serializable]
//    public class OrbitSettings
//    {
//        public float x_rotation = -20f;
//        public float y_rotation = -180f;

//        public float minimum_x_rotation = -85f;
//        public float maximum_x_rotation = 25f;

//        public float x_vertical_rotation_smoothing = 150f;
//        public float y_horizontal_rotation_smoothing = 150f;
//    }

//    [System.Serializable]
//    public class InputSettings
//    {
//        public string y_horizontal_snap = "Orbit Y Horizontal Snap";
//        public string y_horizontal = "Orbit Y Horizontal";
//        public string x_vertical = "Orbit X Vertical";
//        public string zooming_input = "Mouse ScrollWheel";

//        //CREATE INPUTS FOR THESE IN THE PROJECT SETTINGS -> INPUT SECTION (Mouse ScrollWheel is already a default input in the settings).

//        //SET EACH ONE TO HAVE -> 1000 GRAVITY, 0.001 DEAD AND 1000 SENSITIVITY IN THEIR SETTINGS.

//    }

//    public PositionSettings position = new PositionSettings();
//    public OrbitSettings orbit = new OrbitSettings();
//    public InputSettings input = new InputSettings();
//    public CollisionHandler collision = new CollisionHandler();
//    public DebugSettings debug = new DebugSettings();


//    Vector3 player_position = Vector3.zero;
//    Vector3 destination = Vector3.zero; //if not colliding

//    Vector3 adjusted_destination = Vector3.zero; // if colliding
//    Vector3 camera_velocity = Vector3.zero;

//    //CharacterControllerScript player_controller_script; //UNCOMMENT THIS LATER WHEN THE SCRIPT IS USED IN THE ACTUAL PROJECT

//    float x_vertical_input, y_horizontal_input, zoom_input, y_horizontal_snap_input, mouse_input, vertical_mouse_input;

//    Vector3 previous_mouse_position = Vector3.zero;
//    Vector3 current_mouse_position = Vector3.zero;

//    void MoveToTarget()
//    {
//        player_position = player_target.position + Vector3.up * position.player_position_offset.y + Vector3.forward * position.player_position_offset.z + transform.TransformDirection(Vector3.right * position.player_position_offset.x);
//        destination = Quaternion.Euler(orbit.x_rotation, orbit.y_rotation + player_target.eulerAngles.y, 0) * -Vector3.forward * position.distance_from_player;
//        destination += player_position;
//        transform.position = destination;

//        if (collision.colliding)
//        {
//            adjusted_destination = Quaternion.Euler(orbit.x_rotation, orbit.y_rotation + player_target.eulerAngles.y, 0) * Vector3.forward * position.adjustment_distance;
//            adjusted_destination += player_position;

//            if (position.smooth_follow)
//            {
//                transform.position = Vector3.SmoothDamp(transform.position, adjusted_destination, ref camera_velocity, position.smoothing);
//            }
//            else
//            {
//                transform.position = adjusted_destination;
//            }
//        }
//        else
//        {
//            if (position.smooth_follow)
//            {
//                transform.position = Vector3.SmoothDamp(transform.position, destination, ref camera_velocity, position.smoothing);
//            }
//            else
//            {
//                transform.position = destination;
//            }
//        }
//    }

//    void LookAtTarget()
//    {
//        Quaternion player_rotation = Quaternion.LookRotation(player_position - transform.position);
//        transform.rotation = Quaternion.Lerp(transform.rotation, player_rotation, position.camera_look_smoothing * Time.deltaTime);
//    }

//    void GetInput()
//    {
//        x_vertical_input = Input.GetAxisRaw("Orbit X Vertical");
//        y_horizontal_input = Input.GetAxisRaw("Orbit Y Horizontal");
//        zoom_input = Input.GetAxisRaw("Mouse ScrollWheel");
//        y_horizontal_snap_input = Input.GetAxisRaw("Orbit Y Horizontal Snap");
//    }

//    void OrbitTarget()
//    {
//        if (y_horizontal_snap_input > 0)
//        {
//            orbit.y_rotation = -180;
//        }

//        orbit.x_rotation += x_vertical_input * orbit.x_vertical_rotation_smoothing * Time.deltaTime;
//        orbit.y_rotation += y_horizontal_input * orbit.y_horizontal_rotation_smoothing * Time.deltaTime;

//        if (orbit.x_rotation > orbit.maximum_x_rotation)
//        {
//            orbit.x_rotation = orbit.maximum_x_rotation;
//        }

//        if (orbit.x_rotation < orbit.minimum_x_rotation)
//        {
//            orbit.x_rotation = orbit.minimum_x_rotation;
//        }
//    }

//    void ZoomIntoTarget()
//    {
//        position.distance_from_player += zoom_input * position.zoom_smoothing * Time.deltaTime;

//        if (position.distance_from_player > position.maximum_zoom)
//        {
//            position.distance_from_player = position.maximum_zoom;
//        }

//        if (position.distance_from_player < position.minimum_zoom)
//        {
//            position.distance_from_player = position.minimum_zoom;
//        }
//    }

//    [System.Serializable]
//    public class CollisionHandler
//    {
//        public LayerMask collision_layer;

//        [HideInInspector]
//        public bool colliding = false;
//        [HideInInspector]
//        public Vector3[] adjusted_camera_clip_points;
//        [HideInInspector]
//        public Vector3[] desired_camera_clip_points;

//        Camera camera;

//        public void Initialise(Camera cam)
//        {
//            camera = cam;

//            adjusted_camera_clip_points = new Vector3[5];
//            desired_camera_clip_points = new Vector3[5];

//        }


//        public void UpdateCameraClipPoints(Vector3 camera_position, Quaternion at_rotation, ref Vector3[] into_array)
//        {
//            if (!camera)
//            {
//                return;
//            }

//            //clear contents of intoArray

//            into_array = new Vector3[5];

//            float z = camera.nearClipPlane; //distance from camera position to near clip plane
//            float x = Mathf.Tan(camera.fieldOfView / 3.41f) * z; // how big or small cushion between camera and collision is. (size of collision space)
//            float y = x / camera.aspect;

//            //top left
//            into_array[0] = (at_rotation * new Vector3(-x, y, z)) + camera_position; //added/rotated point relative to camera

//            //top right
//            into_array[1] = (at_rotation * new Vector3(x, y, z)) + camera_position;

//            //bottom left
//            into_array[2] = (at_rotation * new Vector3(-x, -y, -z)) + camera_position;

//            //bottom right
//            into_array[3] = (at_rotation * new Vector3(x, -y, z)) + camera_position;

//            //camera's position
//            into_array[4] = camera_position - camera.transform.forward; // little more room behind camera to collide with

//        }
//        bool CollisionDetectedAtClipPoints(Vector3[] clip_points, Vector3 from_position)
//        {
//            for (int i = 0; i < clip_points.Length; i++)
//            {
//                Ray ray = new Ray(from_position, clip_points[i] - from_position);
//                float distance = Vector3.Distance(clip_points[i], from_position);
//                if (Physics.Raycast(ray, distance, collision_layer))
//                {
//                    return true;
//                }
//            }

//            return false;
//        }

//        public float GetAdjustedDistanceWithRayFrom(Vector3 from) //Returns the distance camera needs to be from player (useful only with collision)
//        {
//            float distance = -1;

//            for (int i = 0; i < desired_camera_clip_points.Length; i++)
//            {
//                //finding shortest distance between any clip point colliding and return it

//                Ray ray = new Ray(from, desired_camera_clip_points[i] - from);
//                RaycastHit hit;

//                if (Physics.Raycast(ray, out hit))
//                {
//                    if (distance == -1)
//                    {
//                        distance = hit.distance;
//                    }
//                    else
//                    {
//                        if (hit.distance < distance)
//                        {
//                            distance = hit.distance;
//                        }
//                    }
//                }
//            }

//            if (distance == -1)
//            {
//                return 0;
//            }
//            else
//            {
//                return distance;
//            }
//        }

//        public void CheckColliding(Vector3 player_position) //checks if colliding is true or false at one moment
//        {
//            if (CollisionDetectedAtClipPoints(desired_camera_clip_points, player_position))
//            {
//                colliding = true;
//            }
//            else
//            {
//                colliding = false;
//            }
//        }

//    }

//    void SetCameraTarget(Transform main_target)
//    {
//        player_target = main_target;

//        // Sanity Checks that there is actually a player target for the camera functionality to focus on
//        if (player_target != null)
//        {
//            if (player_target.GetComponent<CharacterControllerScript>())
//            {
//                player_controller_script = player_target.GetComponent<CharacterControllerScript>();
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

//    // Use this for initialization
//    void Start()
//    {
//        SetCameraTarget(player_target);

//        x_vertical_input = y_horizontal_input = zoom_input = y_horizontal_snap_input = mouse_input = vertical_mouse_input = 0;

//        MoveToTarget();

//        collision.Initialise(Camera.main);
//        collision.UpdateCameraClipPoints(transform.position, transform.rotation, ref collision.adjusted_camera_clip_points);
//        collision.UpdateCameraClipPoints(destination, transform.rotation, ref collision.desired_camera_clip_points);

//        previous_mouse_position = current_mouse_position = Input.mousePosition;

//    }
	
//	// Update is called once per frame
//	void Update ()
//    {
//        GetInput();
//        ZoomIntoTarget();
//    }

//    void FixedUpdate()
//    {
//        MoveToTarget();
//        LookAtTarget();
//        OrbitTarget();
//        //MouseOrbitTarget();

//        collision.UpdateCameraClipPoints(transform.position, transform.rotation, ref collision.adjusted_camera_clip_points);
//        collision.UpdateCameraClipPoints(destination, transform.rotation, ref collision.desired_camera_clip_points);

//        //draw debug lines
//        for (int i = 0; i < 5; i++)
//        {
//            if (debug.draw_desired_collision_lines)
//            {
//                Debug.DrawLine(player_position, collision.desired_camera_clip_points[i], Color.white);
//            }

//            if (debug.draw_adjusted_collision_lines)
//            {
//                Debug.DrawLine(player_position, collision.adjusted_camera_clip_points[i], Color.green);
//            }
//        }

//        collision.CheckColliding(player_position); // using raycasts here
//        position.adjustment_distance = collision.GetAdjustedDistanceWithRayFrom(player_position);
//    }
//}
