using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.SceneManagement;

public class FarmerControl : MonoBehaviour
{
    public AudioClip ANGERsfx;
    public AudioClip movingsfx;

    public float speed;
    public Transform player;
    Transform target;
    public Transform[] patrolPoints;
    public int sightRange;
    public int fov;
    private Animator animator;
    int pointsIndex;
    NavMeshAgent agentGuard;
    private AudioSource source;

    // Start is called before the first frame update
    void Start()
    {
        source = GetComponent<AudioSource>();

        animator = GetComponent<Animator>();
        agentGuard = this.GetComponent<NavMeshAgent>();
        SpeedUp();
        SetDestination();
        animator.SetBool("isWalking", true); 
        source.PlayOneShot(movingsfx);

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
        Debug.Log("a");
        if(patrolPoints.Length == 0)
        {
            return;
        }
        agentGuard.destination = patrolPoints[pointsIndex].position;
        pointsIndex = (pointsIndex+1)%patrolPoints.Length;
        animator.SetBool("alert", false);
        animator.SetBool("isWalking", false);
        agentGuard.speed = 0;
    }

    void SetDestination()
    {
        FindPlayer();
        if (target != null)
        {
            Vector3 targetVector = target.position;
            agentGuard.SetDestination(targetVector);
            if(animator.GetBool("alert") == false)
            {
                animator.SetBool("alert", true);
                animator.SetBool("isWalking", false);
                agentGuard.speed = 0;
                source.PlayOneShot(ANGERsfx);
            }
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

    public void SpeedUp()
    {
        agentGuard.speed = speed;

    }
}
