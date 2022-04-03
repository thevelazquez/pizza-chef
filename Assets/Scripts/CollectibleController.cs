using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectibleController : MonoBehaviour
{
    public string itemName;
    public GameObject inventory;
    InventoryController invScript;

    // Start is called before the first frame update
    void Start()
    {
        invScript = inventory.GetComponent<InventoryController>();
        //invScript.CreateListItem(itemName);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnDisable() {
        invScript.CollectedItem(itemName);
    }
}
