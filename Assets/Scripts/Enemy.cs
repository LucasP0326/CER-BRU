using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;

public class Enemy : MonoBehaviour
{
    NavMeshAgent agent;

    public float health;

    public bool hiveMind;
    public bool encounterEnemy;

    public static bool hiveMindActive;

    public bool encounterActive;

    //waypoints
    public Transform[] waypoints;
    int waypointIndex;
    Vector3 target;

    //fov
    public GameObject player;
    public float viewRadius;
    [Range(0, 360)]
    public float viewAngle;
    public bool seesPlayer;
    public bool seenPlayer;
    public LayerMask targetMask;
    public LayerMask obstructionMask;

    //attacking
    public float timeBetweenAttacks;
    bool alreadyAttacked;
    public int attackDamage;

    public Animator animator;

    bool invisibled;

    bool alive = true;

    bool xrayed;

    public AudioSource playerHurt;
    public AudioSource enemyHurt;
    public AudioSource playerDeath;
    public AudioSource enemyDeath;

    public GameObject crosshair;

    void Start()
    {
        hiveMindActive = false;
        agent = GetComponent<NavMeshAgent>();
        player = GameObject.FindWithTag("Player");
        animator = GetComponent<Animator>();
        if (waypoints.Length > 0) ChooseWaypoint();
    }

    void Update()
    {
        //Test if player is in fov
        if (!player.GetComponent<Player>().invisible && alive)
        {
            invisibled = false;
            FieldOfViewCheck();
        }
        else if (!invisibled && alive)
        {
            StopAllCoroutines();
            seenPlayer = false;
            seesPlayer = false;
            if (waypoints.Length > 0) ChooseWaypoint();
            else 
            {
                agent.SetDestination(transform.position);
                animator.ResetTrigger("Walk");
                animator.SetTrigger("Idle");
            }
            invisibled = true;
        }

        if (seenPlayer) //if player has been seen lately
        {
            //follow player
            float distance = Vector3.Distance (transform.position, player.transform.position);

            Vector3 dirToPlayer = transform.position - player.transform.position;
            Vector3 newPos = transform.position - dirToPlayer;

            if (distance >= 1){
                agent.SetDestination(newPos);
                animator.SetTrigger("Walk");
            }
            else{
                agent.SetDestination(transform.position);
                animator.SetTrigger("Idle");
            }

            if (distance <= 1.5) AttackPlayer();
        }
        else
        {
            //patrol points
            if (Vector3.Distance(transform.position, target) < 1)
            {
                ChooseWaypoint();
                animator.SetTrigger("Walk");
            }
        }
    }
    void ChooseWaypoint()
    {
        waypointIndex = Random.Range(0, waypoints.Length);
        target = waypoints[waypointIndex].position;
        agent.SetDestination(target);
        animator.SetTrigger("Walk");
    }

    void FieldOfViewCheck()
    {
        if (hiveMindActive || (encounterActive && encounterEnemy)){
            seesPlayer = true;
            seenPlayer = true;
            Debug.Log("sees");
            StopAllCoroutines();
        }
        //is the player next to the enemy
        Collider[] rangeChecks = Physics.OverlapSphere(transform.position, 1, targetMask);
        if (rangeChecks.Length != 0){
            seesPlayer = true;
            seenPlayer = true;
            if (hiveMind) hiveMindActive = true;
            StopAllCoroutines();
        }
        
        //Is the player within view distance, and doens't have invisibility active?
        rangeChecks = Physics.OverlapSphere(transform.position, viewRadius, targetMask);
        if (rangeChecks.Length != 0 && !hiveMind && !encounterEnemy)
        {
            Transform target = rangeChecks[0].transform;
            Vector3 directionToTarget = (target.position - transform.position).normalized;

            //Is the player within enemy field of view?
            if (Vector3.Angle(transform.forward, directionToTarget) < viewAngle / 2)
            {
                float distanceToTarget = Vector3.Distance(transform.position, target.position);

                //Is the player obstructed?
                if (!Physics.Raycast(transform.position, directionToTarget, distanceToTarget, obstructionMask))
                {
                    //Debug.Log("Sees player");
                    seesPlayer = true;
                    seenPlayer = true;
                    StopAllCoroutines();
                }
                else
                {
                    seesPlayer = false;
                    if (seenPlayer)
                    {
                        seenPlayer = false;
                        StopAllCoroutines();
                        StartCoroutine(ForgetPlayer());
                    }
                }
            }
            else
            {
                if (seesPlayer)
                {
                    seesPlayer = false;
                    StopAllCoroutines();
                    StartCoroutine(ForgetPlayer());
                }
            }
        }
        else if (seesPlayer && !hiveMind && !encounterEnemy)
        {
            seesPlayer = false;
            if (seenPlayer)
            {
                seenPlayer = false;
                StopAllCoroutines();
                StartCoroutine(ForgetPlayer());
            }
        }
    }

    private IEnumerator ForgetPlayer()
    {
        yield return new WaitForSeconds(10);
        //Debug.Log("Player Forgotten");
        seenPlayer = false;
        if (waypoints.Length > 0) ChooseWaypoint();
    }

    private void AttackPlayer()
    {

        if (!alreadyAttacked)
        {
            //Debug.Log("attack!");
            animator.SetTrigger("Attack");
            player.GetComponent<Player>().health -= attackDamage;
            playerHurt.Play();
            player.GetComponent<Player>().Injured();
            if (player.GetComponent<Player>().health == 0)
            {
                playerDeath.Play();
                player.GetComponent<Player>().Death();
            }
            alreadyAttacked = true;
            Invoke(nameof(ResetAttack), timeBetweenAttacks);
            Invoke(nameof(FinishAttackAnim), 0.5f);
        }
    }
        private void ResetAttack()
    {
        alreadyAttacked = false;
    }

    public void TakeDamage(int damage)
    {
        if (hiveMind) hiveMindActive = true;
        if (!player.GetComponent<Player>().invisible && alive){
        seesPlayer = true;
        seenPlayer = true;
        StopAllCoroutines();
        StartCoroutine(ForgetPlayer());
        float distance = Vector3.Distance (transform.position, player.transform.position);

        Vector3 dirToPlayer = transform.position - player.transform.position;
        Vector3 newPos = transform.position - dirToPlayer;
        health -= damage;
        enemyHurt.Play();
        crosshair.GetComponent<Crosshair>().HitEnemy();
        if (health <= 0)
        {
            enemyDeath.Play();
            Invoke(nameof(DestroyEnemy), 0.5f);
        }
        }
    }
    private void DestroyEnemy()
    {
        Destroy(gameObject.GetComponent<CapsuleCollider>());
        agent.SetDestination(transform.position);
        seesPlayer = false;
        seenPlayer = false;
        animator.SetTrigger("Death");
        if (alive == true)
            player.GetComponent<Player>().UpdateKillCount();
        alive = false; //For some reason this is really fucking funny
    }

    private void FinishAttackAnim()
    {
        float distance = Vector3.Distance (transform.position, player.transform.position);
        //if (distance <= 1.5) Debug.Log("HIT");
    }
}
