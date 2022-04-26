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
    bool isAngry = false;
    public GameObject player;
    public Collider head;
    Vector3 playerPos;
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
        if(enemy.remainingDistance == 0)
        {
            animator.SetBool("isWalking",false);
        } else
        {
            animator.SetBool("isWalking",true);
        }
        RaycastHit hit;
        //Debug.DrawRay(transform.position,new Vector3(player.transform.position.x,player.transform.position.y+.6f,player.transform.position.z) - new Vector3(transform.position.x,transform.position.y+.3f,transform.position.z),Color.white,.5f);
        if(Physics.Raycast(new Vector3(transform.position.x,transform.position.y+.3f,transform.position.z), new Vector3(player.transform.position.x,player.transform.position.y+.6f,player.transform.position.z) - new Vector3(transform.position.x,transform.position.y+.3f,transform.position.z), out hit, aggroRange,1))
        {
            //Debug.Log(hit.transform.gameObject.name);
            //testPoint = hit.transform.position;
            if(hit.transform.gameObject.GetComponent(typeof(RaffaController)) != null)
            {
                if(enemy.remainingDistance <= .5f)
                {
                    enemy.speed = walkSpeed;
                }
                if(enemy.speed != chargeSpeed)
                {
                    isAngry = true;
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
            isAngry = false;
            //testPoint = realPoint.position;
            timer = 0;
        }
    }

    void OnCollisionEnter(Collision collision)
    {
        Debug.Log(collision);
        if (collision.gameObject.tag == "Player" && isAngry ==true)
        {
            foreach(ContactPoint contact in collision.contacts)
            {
                if(contact.thisCollider == head)
                {
                    player.GetComponent<HPscript>().changeHP(-1);
                    isAngry = false;
                }
            }
        }
        /* else if (collision.gameObject.tag == "Player" && isAngry == false)
        {  
            StartCoroutine(BoarAttack());
        } */
    }

    /* void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawSphere(testPoint, 1);
    } */

    IEnumerator BoarAttack()
    {
        //Print the time of when the function is first called.
        Debug.Log("Started Coroutine at timestamp : " + Time.time);

        //yield on a new YieldInstruction that waits for 5 seconds.
        yield return new WaitForSeconds(1);

        //After we have waited 5 seconds print the time again.
        Debug.Log("Finished Coroutine at timestamp : " + Time.time);
           //  SceneManager.LoadScene("Level3");

                isAngry = true;   

    }
     
}
