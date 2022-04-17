using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TradeMaker : MonoBehaviour
{
    public Trade[] trades;
    public string offer;
    InventoryController inventory;
    bool IsTradeComplete = false;

    void Start() {
        inventory = FindObjectOfType<InventoryController>();
    }

    public void CheckIfTradable(string item) {
        if (IsTradeComplete) {
            Debug.Log("The trade is complete");
            return;
        }
        foreach (Trade x in trades) {
            if (item == x.take) {
                if (x.count > 0) {
                    x.count--;
                    VerifyTrade();
                    return;
                } else {
                    Debug.Log($"{x.take}s has been satisfied");
                    inventory.CollectedItem(item);
                    return;
                }
            }
        }
        inventory.CollectedItem(item);
        Debug.Log($"{item} is not the desired item");
    }

    //check if no more items are being requested
    void VerifyTrade() {
        foreach(Trade x in trades) {
            if (x.count > 0) {
                return;
            }
        }
        inventory.CollectedItem(offer);
        IsTradeComplete = true;
    }
}
