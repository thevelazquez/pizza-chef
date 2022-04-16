using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class CollectibleDep : MonoBehaviour
{
    public AudioClip pickup;
    public GameObject HitParticle;


    private AudioSource source;
    // Start is called before the first frame update
    void Start()
    {
     

        source = GetComponent<AudioSource>();

    }

 

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Pick Up"))
        {
            other.gameObject.SetActive(false);
        

            source.PlayOneShot(pickup);
            Instantiate(HitParticle, new Vector3(other.transform.position.x, transform.position.y, other.transform.position.z), other.transform.rotation);


        }
    }
   
}