using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.SceneManagement;

public class FarmerControl : MonoBehaviour
{
    public float speed;
    public Transform player;
    Transform target;
    public Transform[] patrolPoints;
    public int sightRange;
    public int fov;
    int pointsIndex;
    NavMeshAgent agentGuard;

    // Start is called before the first frame update
    void Start()
    {
        agentGuard = this.GetComponent<NavMeshAgent>();
        agentGuard.speed = speed;
        SetDestination();
    }

    // Update is called once per frame
    void Update()
    {
        SetDestination();
    }

    public void SetTarget(Transform s)
    {
        target = s;
        SetDestination();
    }

    void GoToNext()
    {
        if(patrolPoints.Length == 0)
        {
            return;
        }
        agentGuard.destination = patrolPoints[pointsIndex].position;
        pointsIndex = (pointsIndex+1)%patrolPoints.Length;
    }

    void SetDestination()
    {
        FindPlayer();
        if (target != null)
        {
            Vector3 targetVector = target.position;
            agentGuard.SetDestination(targetVector);
        } else
        {
            if(!agentGuard.pathPending && agentGuard.remainingDistance < 0.5f)
            {
                GoToNext();
            }
        }
    }

    void FindPlayer()
    {
        RaycastHit hit;
        if (Physics.Raycast(transform.position, (new Vector3(player.position.x,player.position.y+.3f,player.position.z) - transform.position), out hit, sightRange))
        {
            GameObject obj = hit.transform.gameObject;
            if (obj.transform.gameObject.GetComponent(typeof(PlayerControl)) != null)
            {
                Debug.Log(Vector3.Angle(player.position-transform.position,transform.forward));
                if(Vector3.Angle(player.position-transform.position,transform.forward)<fov)
                {
                    target = player;
                    return;
                } else
                {
                    target = null;
                }
            }
        }
        target = null;
    }
}
