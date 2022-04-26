using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GuardianController : MonoBehaviour
{
    public bool isAwakened = false;
    public GameObject Ingredient;
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
        DropIngredient();
        FindObjectOfType<Level2Manager>().areAllGuardiansAwakened();
    }

    void DropIngredient()
    {
        Vector3 position = transform.position;
        GameObject ingredient = Instantiate(Ingredient, position + new Vector3 (0.0f,3.0f,0.0f), Quaternion.identity);
        ingredient.SetActive(true);
    }
}
