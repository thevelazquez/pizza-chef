using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GuardianController : MonoBehaviour
{
    [SerializeField]
    bool isAwakened = false;

    public void Awaken () {
        isAwakened = true;
        Debug.Log("Awakened");
        //start animation or whatever other visual queue
    }
}
