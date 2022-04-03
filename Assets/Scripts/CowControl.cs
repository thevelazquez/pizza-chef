using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.SceneManagement;

public class CowControl : MonoBehaviour
{
    public float speed;
    private Animator animator;
    NavMeshAgent agentGuard;
    public Vector2[] area = {Vector2.zero,Vector2.zero};

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        agentGuard = this.GetComponent<NavMeshAgent>();
        SpeedUp();
        SetDestination();
    }

    // Update is called once per frame
    void Update()
    {
        SetDestination();
    }

    void SetDestination()
    {
        Vector3 targetVector = new Vector3(Random.Range(area[0].x,area[1].x),0,Random.Range(area[0].y,area[1].y));
        if(!agentGuard.pathPending && agentGuard.remainingDistance < 0.5f)
        {
            agentGuard.SetDestination(targetVector);
        }
    }

    public void SpeedUp()
    {
        agentGuard.speed = speed;
    }
}
