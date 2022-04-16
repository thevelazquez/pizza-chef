using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollisionDetection : MonoBehaviour
{
    public RaffaController rfc;
 public GameObject HitParticle;

    

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Enemy") && rfc.isAttacking)
        {
            Debug.Log(other.name);
            other.GetComponent<Animator>().SetTrigger("hit");
            StartCoroutine(EnemyFade());
          Instantiate(HitParticle, new Vector3(other.transform.position.x,transform.position.y,other.transform.position.z), other.transform.rotation);
        }


        IEnumerator EnemyFade()
        {
            //Print the time of when the function is first called.
            Debug.Log("Started Coroutine at timestamp : " + Time.time);

            //yield on a new YieldInstruction that waits for 5 seconds.
            yield return new WaitForSeconds(3);

            //After we have waited 5 seconds print the time again.
            Debug.Log("Finished Coroutine at timestamp : " + Time.time);
            other.gameObject.SetActive(false);

        }
    }
}