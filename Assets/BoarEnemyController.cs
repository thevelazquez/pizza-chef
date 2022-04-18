using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.SceneManagement;

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
    Vector3 testPoint = Vector3.zero;
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
        if (Vector3.Distance(player.transform.position,transform.position)<=3f)
        {
            StartCoroutine(PlayerFade());

        }
        if(enemy.remainingDistance == 0)
        {
            animator.SetBool("isWalking",false);
        } else
        {
            animator.SetBool("isWalking",true);
        }
        RaycastHit hit;
        if(Physics.Raycast(transform.position, player.transform.position - transform.position, out hit, aggroRange,1))
        {
            testPoint = hit.transform.position;
            if(hit.transform.gameObject.GetComponent(typeof(RaffaController)) != null)
            {
                Debug.Log("a");
                if(enemy.remainingDistance <= .5f)
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
        }
        timer += Time.deltaTime;
        if(timer>=walkTime)
        {
            enemy.speed = walkSpeed;
            Vector3 randomPoint = Random.insideUnitSphere * randomDistance;
            randomPoint += origin;
            NavMeshHit realPoint;
            NavMesh.SamplePosition(randomPoint,out realPoint,randomDistance,-1);
            enemy.SetDestination(realPoint.position);
            //testPoint = realPoint.position;
            timer = 0;
        }
    }

    void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawSphere(testPoint, 1);
    }

    IEnumerator PlayerFade()
    {
        //Print the time of when the function is first called.
        Debug.Log("Started Coroutine at timestamp : " + Time.time);

        //yield on a new YieldInstruction that waits for 5 seconds.
        yield return new WaitForSeconds(2);

        //After we have waited 5 seconds print the time again.
        Debug.Log("Finished Coroutine at timestamp : " + Time.time);
        SceneManager.LoadScene("Level3");


    }
}
