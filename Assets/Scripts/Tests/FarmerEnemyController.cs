using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.SceneManagement;

public class FarmerEnemyController : MonoBehaviour
{
    private NavMeshAgent enemy = null;

    [SerializeField]
    private Transform target;

    private Animator animator;
    public GameObject player;
    public Transform[] patrolPoints;
    public float speed;
    public int sightRange;
    public int initfov;
    public int fov;
    public int buffFov = 180;
    int pointsIndex;

    // Start is called before the first frame update
    void Start()
    {
        enemy = GetComponent<NavMeshAgent>();
        animator = GetComponent<Animator>();
        SpeedUp();
        fov = initfov;
    }

    // Update is called once per frame
    void Update()
    {
        SetDestination();
    }

    void SetDestination()
    {
        FindPlayer();
        animator.SetFloat("Speed", 1f, 0.3f, Time.deltaTime);
        if (target != null)
        {
            Vector3 targetVector = target.position;
            enemy.SetDestination(targetVector);
            if(animator.GetBool("alert") == false)
            {
                animator.SetBool("alert", true);
                animator.SetBool("isWalking", false);
                //enemy.speed = 0;
                //animator.SetFloat("Speed", 0f, 0.1f, Time.deltaTime);
                //source.PlayOneShot(ANGERsfx);
            }
            float distanceToTarget = Vector3.Distance(target.position, transform.position);
            if (animator.GetCurrentAnimatorStateInfo(0).IsName("Run") && distanceToTarget <= enemy.stoppingDistance)
            {
                //animator.SetFloat("Speed", 0f, 0.1f, Time.deltaTime);
                animator.SetTrigger("Attack");

              

                //enemy.speed = 0;
            }
        } else
        {
            if(!enemy.pathPending && enemy.remainingDistance < 2.5f)
            {
                GoToNext();
            }
        }
    }

    void FindPlayer()
    {
        RaycastHit hit;
        if (Physics.Raycast(transform.position, new Vector3(player.transform.position.x,player.transform.position.y+.6f,player.transform.position.z) - new Vector3(transform.position.x,transform.position.y+.3f,transform.position.z), out hit, sightRange))
        {
            Debug.DrawRay(transform.position,new Vector3(player.transform.position.x,player.transform.position.y+.6f,player.transform.position.z) - new Vector3(transform.position.x,transform.position.y+.3f,transform.position.z),Color.white,.5f);
            //Debug.DrawRay(transform.position,new Vector3(player.transform.position.x,player.transform.position.y+.3f,player.transform.position.z) - transform.position,Color.white,.5f);
            GameObject obj = hit.transform.gameObject;
            if (obj.transform.gameObject.GetComponent(typeof(RaffaController)) != null)
            {
                if(player.GetComponent<RaffaController>().IsSneaking() == false && Vector3.Angle(player.transform.position-transform.position,transform.forward)<fov)
                {
                    target = player.transform;
                    return;
                } else
                {
                    target = null;
                }
            }
        }
        //target = null;
    }

    void GoToNext()
    {
        if(patrolPoints.Length == 0)
        {
            return;
        }
        enemy.destination = patrolPoints[pointsIndex].position;
        pointsIndex = (pointsIndex+1)%patrolPoints.Length;
        animator.SetBool("alert", false);
        animator.SetBool("isWalking", false);
        //enemy.speed = 0;
        //animator.SetFloat("Speed", 0f, 0.1f, Time.deltaTime);
    }

    public void SpeedUp()
    {
        enemy.speed = speed;
    }

    public void SpeedZero()
    {
        enemy.speed = 0;
    }

    public void PlayerFade()
    {
        StartCoroutine(player.GetComponent<HPscript>().Lose());
    }
}
