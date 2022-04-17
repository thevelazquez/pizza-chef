using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ListItemController : MonoBehaviour
{
    public TextMeshProUGUI title;
    public TextMeshProUGUI countTxt;

    InventoryController inventory;
    
    int quantity = 0;
    //int current = 0;
    // Start is called before the first frame update
    void Start()
    {
        inventory = transform.parent.parent.gameObject.GetComponent<InventoryController>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SetText (string name, int x) {
        title.text = name;
        quantity = x;
        ConstructCount();
    }

    void ConstructCount() {
        countTxt.text = $"{quantity}";
    }

    public void AddToCount() {
        quantity++;
        ConstructCount();
    }
    public void SubtractFromCount() {
        inventory.RemoveItem(title.text);
    }
}
