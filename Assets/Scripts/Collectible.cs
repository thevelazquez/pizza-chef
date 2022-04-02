using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class Collectible : MonoBehaviour
{
    public GameObject collected;
    public AudioClip pickup;


    private AudioSource source;
    // Start is called before the first frame update
    void Start()
    {
        collected.SetActive(false);

        source = GetComponent<AudioSource>();

    }

 

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Pick Up"))
        {
            other.gameObject.SetActive(false);
            collected.SetActive(true);

            source.PlayOneShot(pickup);


        }
    }
   
}