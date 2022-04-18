using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class BoarEnemyController : MonoBehaviour
{
    NavMeshAgent enemy;
    Vector3 origin;
    float walkTime = 2f;
    float timer;
    public float chargeSpeed = 7f;
    public float walkSpeed = 3.5f;
    public float randomDistance; // distance the boar chooses to wander towards
    public float aggroRange;
    public GameObject player;
    Animator animator;
    // Start is called before the first frame update
    void Start()
    {
        enemy = GetComponent<NavMeshAgent>();
        animator = GetComponent<Animator>();
        enemy.speed = walkSpeed;
        origin = transform.position;
        timer = walkTime;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if(enemy.remainingDistance == 0)
        {
            animator.SetBool("isWalking",false);
        } else
        {
            animator.SetBool("isWalking",true);
        }
        RaycastHit hit;
        if(Physics.Raycast(transform.position, player.transform.position - transform.position, out hit, aggroRange))
        {
            if(enemy.remainingDistance == 0)
            {
                enemy.speed = walkSpeed;
            }
            if(enemy.speed != chargeSpeed)
            {
                enemy.SetDestination(player.transform.position);
                enemy.speed = chargeSpeed;
                return;
            }
        }
        timer += Time.deltaTime;
        if(timer>=walkTime)
        {
            Debug.Log("1");
            enemy.speed = walkSpeed;
            Vector3 randomPoint = Random.insideUnitSphere * randomDistance;
            randomPoint += origin;
            NavMeshHit realPoint;
            NavMesh.SamplePosition(randomPoint,out realPoint,randomDistance,1);
            enemy.SetDestination(realPoint.position);
            timer = 0;
        }
    }
}
