using UnityEngine;

public class Interactible : MonoBehaviour
{
    public float radius = 3f;

    void OnDrawGizmosSelected ()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, radius);
    }
    
}
