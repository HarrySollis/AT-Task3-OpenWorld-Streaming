using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectableController : MonoBehaviour
{
    public ParticleSystem particlePickup;
    private ParticleSystem.EmissionModule particlePlay;
    float c_rotate_speed;
    float c_float_speed;

    public bool IsSpeedBost = false;
    public bool IsDoubleJump = false;

    public SpeedBoost BoostScript;
    public JumpBoost JumpScript;

    public float frequency;
    public float amplitude;

    Vector3 position_offset = new Vector3();
    Vector3 temporary_position = new Vector3();

    void OnTriggerEnter(Collider col)
    {
        if (col.gameObject.CompareTag("Player"))
        {
            if (IsSpeedBost)
            {
                BoostScript.BoostPickedUp = true;
            }

            else if (IsDoubleJump)
            {
                //JumpScript.JumpPickedUp = true;
            }

            particlePickup.Play();
            Destroy(this.gameObject);
        }
    }

    // Use this for initialization
    void Start()
    {
        c_rotate_speed = Random.Range(5f, 40f);
        c_float_speed = Random.Range(0.4f, 1f);

        frequency = Random.Range(0.5f, 1f);
        amplitude = Random.Range(0.2f, 0.5f);

        position_offset = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        transform.Rotate(new Vector3(0f, Time.deltaTime * c_rotate_speed, 0f), Space.World);

        temporary_position = position_offset;

        temporary_position.y += Mathf.Sin(Time.fixedTime * Mathf.PI * frequency) * amplitude;

        transform.position = temporary_position;
    }
}