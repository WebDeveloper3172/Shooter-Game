using System.Collections;
using UnityEngine;
using UnityEngine.AI;

public class AIEnemyNavigation : MonoBehaviour
{
    public NavMeshAgent agent;
    public Transform player;

    public LayerMask groundLayer, playerLayer;

    //Patroling
    public Vector3 walkPoint;
    bool walkPointSet;
    public float walkPointRange;

    //Attacking
    public float timeBetweenAttacks;
    bool alreadyAttacked;
    public GameObject projectilePrefab;

    //States
    public float sightRange, attackRange;
    public bool playerInSightRange, playerInAttackRange;

    //Shot Speed
    [SerializeField] float forwardSpeed = 25f;
    [SerializeField] float upSpeed = 5f;
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("PlayerBullet"))
        {
            Destroy(other.gameObject);
        }
    }
    private void Awake()
    {
        agent = GetComponent<NavMeshAgent>();
    }

    private void Update()
    {
        playerInSightRange = Physics.CheckSphere(transform.position, sightRange, playerLayer);
        playerInAttackRange = Physics.CheckSphere(transform.position , attackRange , playerLayer);

        if(!playerInSightRange && !playerInAttackRange) Patroling();
        if(playerInSightRange && !playerInAttackRange) ChasePlayer();
        if(playerInSightRange && playerInAttackRange) AttackPlayer();
    }

   private void Patroling()
    { 
        if(!walkPointSet) SearchWalkPoint();
        if (walkPointSet) { agent.SetDestination(walkPoint); }

        Vector3 distanceToWalkPoint = transform.position - walkPoint;

        if (distanceToWalkPoint.magnitude < 1f) walkPointSet = false;
    }

    private void SearchWalkPoint()
    {
        float randomZ = Random.Range(-walkPointRange , walkPointRange);
        float randomX = Random.Range(-walkPointRange , walkPointRange);

        walkPoint = new Vector3(transform.position.x + randomX , transform.position.z + randomZ);

        if (Physics.Raycast(walkPoint , -transform.up , 2f , groundLayer))
        {
            walkPointSet = true;
        }
    }

    private void ChasePlayer()
    {
        agent.SetDestination(player.position);
    }

    private void AttackPlayer()
    {
        agent.SetDestination(transform.position);
        transform.LookAt(player);

        if (!alreadyAttacked)
        {
            GameObject projectile = Instantiate(projectilePrefab, transform.position, Quaternion.identity);
            Rigidbody rb = projectile.GetComponent<Rigidbody>();
            rb.AddForce(transform.forward * forwardSpeed , ForceMode.Impulse);
            rb.AddForce(transform.up * upSpeed , ForceMode.Impulse);

            Destroy(projectile.gameObject , 3f);

            alreadyAttacked = true;

            Invoke(nameof(ResetAttack), timeBetweenAttacks);
        }
    }

    private void ResetAttack()
    {
        alreadyAttacked = false;
    }
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position , attackRange);

        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, sightRange);
    }
}
