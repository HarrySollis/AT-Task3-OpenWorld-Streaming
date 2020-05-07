//using System.Collections;
//using System.Collections.Generic;
//using UnityEngine;
//using UnityEngine.AI;

//public class EnemyBehaviour : MonoBehaviour {

//    public int starting_health = 100;
//    public int current_health;

//    public Transform enemy_to_instantiate;

//    //public AudioClip death_clip; -> use IF applying a sound clip for when the enemy dies

//    //Animator enemy_animator;

//    bool enemy_dead;

//    public float look_radius = 10f;

//    public float force_thrust = 100;

//    Transform player_target;

//    Vector3 knockback_direction;

//    Rigidbody enemy_rigidbody;

//    NavMeshAgent agent;

//    [HideInInspector]
//    public bool attacked_by_player = false;

//    //----------------------------------------------------------------------------------------------

//    public int main_damage = 25;

//    //Animator enemy_animator; ->  uncomment if there IS an enemy animator to use!!

//    GameObject player;

//    public bool player_in_range;

//    public float time_between_attacks = 100f; //-> use if having a delay between attacks!!
//    float timer;

//    //CharacterControllerScript player_script;

//    //----------------------------------------------------------------------------------------------

//    public int max_number_to_instantiate = 2;

//    //----------------------------------------------------------------------------------------------

//    [SerializeField]
//    bool patrol_waiting;

//    [SerializeField]
//    float wait_time = 3f;

//    [SerializeField]
//    float probability_of_switch = 0.2f;

//    [SerializeField]
//    //List<PatrolWayPoint> patrolling_points;

//    int current_patrol_index;
//    bool travelling;
//    bool waiting;
//    bool patrol_forward;
//    float wait_timer;


//    //----------------------------------------------------------------------------------------------

//    //Animation related

//    public float distance_to_grounded = 0.2f;
//    public LayerMask ground_layer;

//    //----------------------------------------------------------------------------------------------

//    private void OnDrawGizmos()
//    {
//        Gizmos.color = Color.red;
//        Gizmos.DrawWireSphere(transform.position, look_radius);
//    }

//    // Use this for initialization
//    void Start ()
//    {
//        player = GameObject.FindGameObjectWithTag("Player");
//        player_script = GameObject.Find("Player").GetComponent<CharacterControllerScript>();

//        player_target = player.transform;

//        enemy_rigidbody = GetComponent<Rigidbody>();

//        agent = GetComponent<NavMeshAgent>();

//        current_health = starting_health;

//        if (agent == null)
//        {
//            Debug.Log("Navmesh agent component not attached to player");
//        }
//        else
//        {
//            //if (patrolling_points != null && patrolling_points.Count >= 2)
//            //{
//            //    current_patrol_index = 0;
//            //    SetDestination();
//            //}
//            //else
//            //{
//            //    Debug.Log("Not enough patrol points for enemy slime to patrol");
//            //}
//        }
//    }

//    // Update is called once per frame
//    void Update ()
//    {
//        float distance = Vector3.Distance(player_target.position, transform.position);

//        if (distance <= look_radius)
//        {
//            agent.SetDestination(player_target.position);

//            if (distance <= agent.stoppingDistance)
//            {
//                //Face the Target
//                FaceTarget();
//            }
//        }

//        timer += Time.deltaTime;

//        //Debug.Log(timer);

//        if (timer >= time_between_attacks && player_in_range && current_health > 0)
//        {
//            Attack();

//            Debug.Log("Attacking Player!");
//        }

//        if (travelling && agent.remainingDistance <= 1.0f)
//        {
//            travelling = false;

//            if (patrol_waiting)
//            {
//                waiting = true;
//                wait_timer = 0f;
//            }
//            else
//            {
//                ChangePatrolPoint();
//                SetDestination();
//            }
//        }

//        if (waiting)
//        {
//            wait_timer += Time.deltaTime;

//            if (wait_timer >= wait_time)
//            {
//                waiting = false;

//                ChangePatrolPoint();
//                SetDestination();
//            }
//        }
//	}

//    void FaceTarget()
//    {
//        Vector3 direction = (player_target.position - transform.position).normalized;

//        Quaternion look_rotation = Quaternion.LookRotation(new Vector3(direction.x, 0, direction.z));

//        transform.rotation = Quaternion.Slerp(transform.rotation, look_rotation, Time.deltaTime * 5);
//    }

//    void Attack()
//    {
//        timer = 0f;

//        if (player_script != null)
//        {
//            //if (player_script.current_health > 0)
//            {
//                player_script.TakingDamage(main_damage);
//                Debug.Log("Player is still alive!!");
//            }
//        }
//        else
//        {
//            Debug.Log("NULL Problem :/!!");
//        }
//    }

//    void Death()
//    {
//        enemy_dead = true;

//        Destroy(this.gameObject);

//        Debug.Log("Big Enemy Died!");

//        for (int i = 0; i < max_number_to_instantiate; i++)
//        {
//            Instantiate(enemy_to_instantiate, new Vector3(transform.position.x, transform.position.y, transform.position.z), Quaternion.identity);
//        }

//        //enemy_box_collider.isTrigger = true; -> if you want to have the collider turn to a trigger during death so things go through it!!

//        //Play audio clips if you wish...
//    }

//    public void TakeDamage(int amount)
//    {
//        if (enemy_dead)
//        {
//            return;
//        }

//        current_health -= amount;

//        if (current_health <= 0 && !enemy_dead)
//        {
//            Death();
//        }

//        attacked_by_player = true;
//    }

//    private void FixedUpdate()
//    {
//        if (attacked_by_player)
//        {
//            Debug.Log("ATTACKED BY PLAYER AND BOUNCING!!!");

//            agent.velocity = knockback_direction.normalized * force_thrust;

//            StartCoroutine(KnockBackEnemyDelay());
//        }
//    }

//    IEnumerator KnockBackEnemyDelay()
//    {
//        yield return new WaitForSeconds(0.6f);

//        attacked_by_player = false;
//    }

//    private void OnTriggerEnter(Collider other)
//    {
//        if (other.gameObject == player)
//        {
//            player_in_range = true;

//            Debug.Log("Player Within Range");

//            knockback_direction = other.transform.forward;
//        }
//    }

//    private void OnTriggerExit(Collider other)
//    {
//        if (other.gameObject == player)
//        {
//            player_in_range = false;

//            Debug.Log("Player Now Not Within Range");
//        }
//    }

//    private void SetDestination()
//    {
//        //if (patrolling_points != null)
//        //{
//        //    Vector3 target_vector = patrolling_points[current_patrol_index].transform.position;
//        //    agent.SetDestination(target_vector);
//        //    travelling = true;
//        //}
//    }

//    private void ChangePatrolPoint()
//    {
//        if (UnityEngine.Random.Range(0f, 1f) <= probability_of_switch)
//        {
//            patrol_forward = !patrol_forward;
//        }

//        ////if (patrol_forward)
//        ////{
//        ////    current_patrol_index = (current_patrol_index + 1) % patrolling_points.Count;
//        ////}
//        ////else
//        //{
//        //    //if (--current_patrol_index < 0)
//        //    //{
//        //    //    current_patrol_index = patrolling_points.Count - 1;
//        //    //}
//        //}
//    }
//    bool Grounded()
//    {
//        return Physics.Raycast(transform.position, Vector3.down, distance_to_grounded, ground_layer);
//    }

//    private void Jump()
//    {
//        if (Grounded())
//        {
//            Debug.Log("On the Ground!");
//        }
//        else
//        {
//            Debug.Log("In the Air!");

//        }
//    }
//}
