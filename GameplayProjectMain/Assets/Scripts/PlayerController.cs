using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class MovementSettings
{
    public float forwardVel = 4;
    public float strafeVel = 4;
    public float jumpVel = 11;
    public float distToGrounded = 0.05f;
    public int jumpCount;
    public bool jumping;
    public Vector3 velocity = Vector3.zero;
    public float sprintVel = 0f;
    public LayerMask ground;
}

[System.Serializable]
public class PhysicSettings
{
    public float downAccel = 0.4f;
}

[System.Serializable]
public class InputSettings
{
    public float inputDelay = 0.1f;
    public string FORWARD_AXIS = "Vertical";
    public string STRAFE_AXIS = "Horizontal";
    public string JUMP_AXIS = "Jump";
    public float TRIGGER_AXIS;
}

[System.Serializable]
public class CameraSettings
{
    public Camera mainCam;
    public Transform mainCamTransform;
}

[System.Serializable]
public class CharacterSettings
{
    public int PlayerScore = 0;
    public Animator anim;
    public Rigidbody rigBody;
}

[System.Serializable]
public class GameSettings
{
    public bool inGame;
    public bool cinematic;
}

[System.Serializable]
public class PowerupSettings
{
    public string abilityType;

    public float abilityTimer = 0f;
}
public class PlayerController : MonoBehaviour
{
    public bool enableSideMove = false;
    public bool disableAxis = false;
    public MovementSettings moveSettings = new MovementSettings();
    public PhysicSettings physSettings = new PhysicSettings();
    public InputSettings inputSettings = new InputSettings();
    public CameraSettings camSettings = new CameraSettings();
    public CharacterSettings charSettings = new CharacterSettings();
    public GameSettings gameSettings = new GameSettings();
    public PowerupSettings powerupSettings = new PowerupSettings();

    float forwardInput, strafeInput, jumpInput;

    Vector2 input;

    Quaternion targetRotation;
    Transform target;

    public Quaternion TargetRotation
    {
        get { return targetRotation; }
    }
    public bool x;
    public bool Grounded()
    { 
        if (Physics.Raycast(transform.position, Vector3.down, moveSettings.distToGrounded, moveSettings.ground))
        {
            if (powerupSettings.abilityType != "DoubleJump")
            {
                moveSettings.jumpCount = 1;
            }
            else
            {
                moveSettings.jumpCount = 2;
            }
            charSettings.anim.SetBool("Landing", false);
            charSettings.anim.SetBool("Landed", true);
            //Debug.Log("Grounded");
            x = true;
            return true;
        }
        else
        {
            x = false;
            return false;
        }
    }

    void CharacterAnimationController()
    {
        charSettings.anim.SetBool("Move Forwards", false);
        charSettings.anim.SetBool("Move Backwards", false);
        charSettings.anim.SetBool("Move Left", false);
        charSettings.anim.SetBool("Move Right", false);

        

        //Make the player move forward and backwards.
        if (Input.GetAxis("Vertical") > 0)
        {
            if (moveSettings.jumping == false)
            {
                
                charSettings.anim.SetBool("Move Backwards", false);
                charSettings.anim.SetBool("Move Left", false);
                charSettings.anim.SetBool("Move Right", false);
                charSettings.anim.SetBool("Move Forwards", true);
            }
        }

        if (Input.GetAxis("Vertical") < 0)
        {
            if (moveSettings.jumping == false)
            {
                charSettings.anim.SetBool("Move Forwards", false);
                charSettings.anim.SetBool("Move Left", false);
                charSettings.anim.SetBool("Move Right", false);
                charSettings.anim.SetBool("Move Backwards", true);
                
            }
        }

        if (enableSideMove == true)
        {
            if (Input.GetAxis("Horizontal") < 0)
            {
                if (moveSettings.jumping == false)
                {
                    charSettings.anim.SetBool("Move Forwards", false);
                    charSettings.anim.SetBool("Move Backwards", false);
                    charSettings.anim.SetBool("Move Right", false);
                    charSettings.anim.SetBool("Move Left", true);
                }
            }

            if (Input.GetAxis("Horizontal") > 0)
            {
                if (moveSettings.jumping == false)
                {
                    charSettings.anim.SetBool("Move Forwards", false);
                    charSettings.anim.SetBool("Move Backwards", false);
                    charSettings.anim.SetBool("Move Left", false);
                    charSettings.anim.SetBool("Move Right", true);
                }
            }
        }

        if (Input.GetButtonDown("Right Punch"))
        {
            //Debug.Log("<b> MOUSE BUTTON CLICKED </b> ;)");
            charSettings.anim.SetTrigger("Right Punch");
        }

        if (Input.GetButtonDown("Left Punch"))
        {
            charSettings.anim.SetTrigger("Left Punch");
        }

        if(Input.GetButtonDown("Jump"))
        {

        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Vector3 dir = transform.TransformDirection(Vector3.down) *5;
        Gizmos.DrawRay(transform.position, dir);
    }

    // Use this for initialization
    void Start ()
    {
        charSettings.anim = GetComponent<Animator>();
        targetRotation = transform.rotation;
        charSettings.rigBody = GetComponent<Rigidbody>();
        forwardInput = strafeInput = 0;
        gameSettings.inGame = true;
        moveSettings.jumpCount = 1;
        enableSideMove = true;
        //camSettings.mainCam = Camera.;
        //inputSettings.TRIGGER_AXIS = Input.GetAxis("XboxTriggers");

    }
	
	// Update is called once per frame
	void Update ()
    {
        if (gameSettings.cinematic)
        {
            charSettings.anim.SetBool("Move Forwards", false);
            charSettings.anim.SetBool("Move Backwards", false);
            charSettings.anim.SetBool("Move Left", false);
            charSettings.anim.SetBool("Move Right", false);
            moveSettings.velocity = Vector3.zero;
        }



        if (!gameSettings.cinematic)
        {

            input = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));
            input = Vector2.ClampMagnitude(input, 1);

            if (!x && charSettings.anim.GetBool("Move Forwards") == true)
            {
                charSettings.anim.SetBool("Move Forwards", false);
                charSettings.anim.SetBool("Falling", true);
            }

            moveSettings.velocity = transform.InverseTransformDirection(charSettings.rigBody.velocity);
            GetInput();
        }
       
	}

    void GetInput()
    {
        forwardInput = Input.GetAxis(inputSettings.FORWARD_AXIS);

        if(!disableAxis)
        {
            strafeInput = Input.GetAxis(inputSettings.STRAFE_AXIS);
        }

        if(Input.GetKeyDown(KeyCode.Space) || Input.GetKeyDown("joystick button 0"))
        {
            jumpInput++;
            Debug.Log("JumpCount = " + moveSettings.jumpCount);
            Debug.Log("Jump Input = " + jumpInput);
        }
    }

    private void FixedUpdate()
    {
        if(gameSettings.inGame)
        {
            CharacterAnimationController();
            Running();
            Strafing();
            Jumping();
            Attacking();
            PowerUpTimer();
        }
        charSettings.rigBody.velocity = transform.TransformDirection(moveSettings.velocity);
    }

    public void PowerUpTimer()
    {
        if (powerupSettings.abilityTimer > 0)
        {
            powerupSettings.abilityTimer--;
        }
        if (powerupSettings.abilityTimer == 0 && powerupSettings.abilityType == "DoubleJump")
        {
            powerupSettings.abilityType = null;
            moveSettings.jumpCount = 1;
        }
    }

    void Running()
    {
        //if(Mathf.Abs(forwardInput) > inputSettings.inputDelay)
        //{
        //    moveSettings.velocity.z = moveSettings.forwardVel * forwardInput * 1f;
        //}
        //else
        //{

        //}

        Vector3 camera_forward = camSettings.mainCamTransform.forward;
        Vector3 camera_right = camSettings.mainCamTransform.right;

        camera_forward.y = 0;
        camera_right.y = 0;

        camera_forward = camera_forward.normalized;
        if(enableSideMove)
        {
            camera_right = camera_right.normalized;

            if (Mathf.Abs(forwardInput) > inputSettings.inputDelay)
            {
                //charSettings.anim.SetBool("Move Forwards", true);
                transform.position += ((camera_forward * input.y) + (camera_right * input.x)) * Time.deltaTime * moveSettings.forwardVel;
                //charSettings.anim.SetBool("Move Forwards", true);
            }
        }

        else

        {
            camera_right = camera_right.normalized;
            //charSettings.anim.SetBool("Move Forwards", true);
            if (Mathf.Abs(forwardInput) > inputSettings.inputDelay)
            {
                //charSettings.anim.SetBool("Move Forwards", true);
                transform.position += ((camera_forward * input.y)) * Time.deltaTime * moveSettings.forwardVel;
                //charSettings.anim.SetBool("Move Backwards", true);
            }
        }
        //If pressing the vertical movement (forwards and backwards input) - then make the player face the camera's current forwards direction.
        if (Mathf.Abs(forwardInput) > inputSettings.inputDelay)
        {
            transform.forward = camera_forward;
        }


    }
    
    void Strafing()
    {
        if (enableSideMove == true)
        {
            if (Mathf.Abs(strafeInput) > inputSettings.inputDelay)
            {
                moveSettings.velocity.x = moveSettings.strafeVel * strafeInput * 1f;
            }
        }

        else
        {
            moveSettings.velocity.x = 0;
        }
    }

    void Jumping()
    {
        //if(moveSettings.jumpCount < 2 && jumpInput > 0)
        //{
        //    moveSettings.jumpCount = moveSettings.jumpCount + 1;
        //    //Debug.Log(moveSettings.jumpCount);
        //    moveSettings.jumping = true;
        //}
        //if(jumpInput > 0 && (Grounded()))
        //{
        //    //charSettings.rigBody.AddForce(0, 1f, 0, ForceMode.Impulse);
        //    moveSettings.velocity.y = moveSettings.jumpVel;
        //    charSettings.anim.SetBool("Jumping", true);
        //    jumpInput = 0;
        //}
        //else if(Grounded())
        //{
        //    moveSettings.velocity.y = -1f;
        //    moveSettings.jumping = false;
        //    charSettings.anim.SetBool("Falling", false);
        //    charSettings.anim.SetBool("Landing", true);
        //}
        //else
        //{
        //    moveSettings.velocity.y -= physSettings.downAccel;
        //    charSettings.anim.SetBool("Jumping", false);
        //    charSettings.anim.SetBool("Falling", true);
        //}
        if (jumpInput > 0 && !Grounded())
        {
            jumpInput = 0;
        }
        if (moveSettings.jumpCount != 0 && jumpInput > 0 && Grounded())
        {
            moveSettings.jumpCount = moveSettings.jumpCount - 1;

            moveSettings.velocity.y = moveSettings.jumpVel;
            charSettings.anim.SetBool("Jumping", true);
        }
        else if (Grounded())
        {
            moveSettings.velocity.y = -1f;
            charSettings.anim.SetBool("Falling", false);
            charSettings.anim.SetBool("Landing", true);
            //moveSettings.jumpCount = 1;
            //jumpInput = 0;
        }
        else
        {
            moveSettings.velocity.y -= physSettings.downAccel;
            charSettings.anim.SetBool("Jumping", false);
            charSettings.anim.SetBool("Falling", true);
        }

        if (powerupSettings.abilityType == "DoubleJump" && moveSettings.jumpCount != 0)
        {
            if (Input.GetButtonDown("Jump") || Input.GetKeyDown("joystick button 0"))
            {
                moveSettings.jumpCount = moveSettings.jumpCount - 1;

                moveSettings.velocity.y = moveSettings.jumpVel;
                charSettings.anim.SetBool("Jumping", true);
            }
        }
    }

    void Attacking()
    {
        if(Input.GetMouseButtonDown(0) || Input.GetKeyDown("joystick button 5"))
        {
            Debug.Log("Light Attack");
        }
        //if(Input.GetAxis("XboxTriggers") != 0)
        //{
        //    Debug.Log("Trigger Value: " + inputSettings.TRIGGER_AXIS);
        //}
    }

    void Equipping()
    {
        if(Input.GetKeyDown(KeyCode.R) || Input.GetKeyDown("joystick button 3"))
        {

        }
    }
}
