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

    Animator animator;

    bool invisibled;

    bool alive = true;

    bool xrayed;

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        player = GameObject.FindWithTag("Player");
        if (waypoints.Length > 0) ChooseWaypoint();
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.X)){
            if (!xrayed)
            {
                gameObject.layer = 6;
                var children = transform.GetComponentsInChildren<Transform>(includeInactive: true);
                foreach (var child in children)
                {
                    child.gameObject.layer = 6;
                }
                xrayed = true;
            }
            else
            {
                gameObject.layer = 0;
                var children = transform.GetComponentsInChildren<Transform>(includeInactive: true);
                foreach (var child in children)
                {
                    child.gameObject.layer = 0;
                }
                xrayed = false;
            }
        }
            //Controller
        if (Input.GetButtonDown("Use")){
            if (!xrayed)
            {
                gameObject.layer = 6;
                var children = transform.GetComponentsInChildren<Transform>(includeInactive: true);
                foreach (var child in children)
                {
                    child.gameObject.layer = 6;
                }
                xrayed = true;
            }
            else
            {
                gameObject.layer = 0;
                var children = transform.GetComponentsInChildren<Transform>(includeInactive: true);
                foreach (var child in children)
                {
                    child.gameObject.layer = 0;
                }
                xrayed = false;
            }
        }
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
            Vector3 dirToPlayer = transform.position - player.transform.position;
            Vector3 newPos = transform.position - dirToPlayer;
            agent.SetDestination(newPos);
            animator.SetTrigger("Walk");

            float distance = Vector3.Distance (transform.position, player.transform.position);
            if (distance <= 1) AttackPlayer();
        }
        else
        {
            //patrol points
            if (Vector3.Distance(transform.position, target) < 2)
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
    }

    void FieldOfViewCheck()
    {
        //Is the player within view distance, and doens't have invisibility active?
        Collider[] rangeChecks = Physics.OverlapSphere(transform.position, viewRadius, targetMask);
        if (rangeChecks.Length != 0)
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
        else if (seesPlayer)
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
            if (player.GetComponent<Player>().health == 0) player.GetComponent<Player>().Death();

            alreadyAttacked = true;
            Invoke(nameof(ResetAttack), timeBetweenAttacks);
        }
    }
        private void ResetAttack()
    {
        alreadyAttacked = false;
    }

    public void TakeDamage(int damage)
    {
        health -= damage;

        if (health <= 0) Invoke(nameof(DestroyEnemy), 0.5f);
    }
    private void DestroyEnemy()
    {
        Destroy(gameObject.GetComponent<CapsuleCollider>());
        agent.SetDestination(transform.position);
        seesPlayer = false;
        seenPlayer = false;
        animator.SetTrigger("Death");
        alive = false;
    }
}
