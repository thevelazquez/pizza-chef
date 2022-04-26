using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TradeMaker : MonoBehaviour
{
    public Trade[] trades;
    public string offer;
    [SerializeField]
    int maxTrades = 1;
    InventoryController inventory;
    bool IsTradeComplete = false;

    void Start() {
        inventory = FindObjectOfType<InventoryController>();
        foreach (Trade x in trades) {
            x.Init();
        }
    }

    public void CheckIfTradable(string item) {
        if (IsTradeComplete) {
            Debug.Log("The trade is complete");
            return;
        }
        inventory.RemoveItem(item);
        foreach (Trade x in trades) {
            if (item == x.take) {
                if (x.count > 0) {
                    x.count--;
                    Debug.Log($"TEST {x.GetInitialCount()}");
                    StartCoroutine(VerifyTrade());

                    return;
                }/* else {
                    Debug.Log($"{x.take}s has been satisfied");
                    inventory.CollectedItem(item);
                    return;
                }*/
            }
        }
        inventory.CollectedItem(item);
        Debug.Log($"{item} is not the desired item");
    }

    //check if no more items are being requested
    IEnumerator VerifyTrade() {
        yield return 0;
        foreach(Trade x in trades) {
            if (x.count > 0) {
                yield break;
            }
        }
        maxTrades--;
        inventory.CollectedItem(offer);
        if (maxTrades == 0) {
            IsTradeComplete = true;
            yield break;
        }
        ResetTrades();
    }

    void ResetTrades() {
        foreach(Trade x in trades) {
            Debug.Log("Resetting trades");
            x.count = x.GetInitialCount();
        }
    }

    public bool GetIsTradeComplete() {
        return IsTradeComplete;
    }
}
