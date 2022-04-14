using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class FarmerEnemyController : MonoBehaviour
{
    private NavMeshAgent enemy = null;

    [SerializeField]
    private Transform target;

    private Animator animator;

    // Start is called before the first frame update
    void Start()
    {
        enemy = GetComponent<NavMeshAgent>();
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        enemy.SetDestination(target.position);
        animator.SetFloat("Speed", 1f, 0.3f, Time.deltaTime);

        transform.LookAt(target);
        Vector3 direction = target.position - transform.position;
        Quaternion rotation = Quaternion.LookRotation(direction, Vector3.up);

        float distanceToTarget = Vector3.Distance(target.position, transform.position);
        if (distanceToTarget <= enemy.stoppingDistance)
        {
            animator.SetFloat("Speed", 0f, 0.1f, Time.deltaTime);
            animator.SetTrigger("Attack");
        }
    }
}
