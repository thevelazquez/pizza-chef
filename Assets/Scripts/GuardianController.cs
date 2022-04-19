using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GuardianController : MonoBehaviour
{
    public bool isAwakened = false;

    private Animator animator;

    void Start()
    {
        animator = GetComponent<Animator>();
    }

    public void Awaken() {
        isAwakened = true;
        Debug.Log("Awakened");
        //start animation or whatever other visual queue
        animator.SetBool("Awake", true);
        FindObjectOfType<Level2Manager>().areAllGuardiansAwakened();
    }
}
