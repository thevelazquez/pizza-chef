using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class CollectibleController : MonoBehaviour
{
    public string itemName;
    public GameObject inventoryPanel;
    InventoryController invScript;
    public GameObject HitParticle;

    // Start is called before the first frame update
    void Start()
    {
        invScript = inventoryPanel.GetComponent<InventoryController>();
        //invScript.CreateListItem(itemName);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnDisable() {
        invScript.CollectedItem(itemName);
        Instantiate(HitParticle, new Vector3(gameObject.transform.position.x, transform.position.y, gameObject.transform.position.z), gameObject.transform.rotation);

    }
    /* void OnDrawGizmos() {
        Handles.Label(transform.position, itemName);
    } */
}
