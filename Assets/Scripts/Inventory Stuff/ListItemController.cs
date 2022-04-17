using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ListItemController : MonoBehaviour
{
    public TextMeshProUGUI title;
    public TextMeshProUGUI countTxt;
    //InventoryController inventory;
    RaffaController player;
    int quantity = 0;
    Image getImage;
    //int current = 0;
    // Start is called before the first frame update
    void Start()
    {
        //inventory = transform.parent.parent.gameObject.GetComponent<InventoryController>();
        player = FindObjectOfType<RaffaController>();
        getImage = GetComponent<Image>();
        
    }

    // Update is called once per frame
    void Update()
    {
        if (player.withinTradingRange) {
            getImage.color = Color.green;
        } else {
            getImage.color = Color.white;
        }
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
        if (player.withinTradingRange) {
            player.GetTraderReference().GetComponent<TradeMaker>().CheckIfTradable(title.text);
        }
    }
}
