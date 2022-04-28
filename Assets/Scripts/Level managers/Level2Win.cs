using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System.Linq;

public class Level2Win : MonoBehaviour
{
    public GameObject player;
    [SerializeField]
    float killFloorY;
    public GameObject goddessDialogue;
    public GameObject goddessTrader;
    public GameObject winText;
    public List<string> requiredItems;

    void Update()
    {
        if (player.transform.position.y < killFloorY) {
            StartCoroutine(player.GetComponent<HPscript>().Lose());
        }

        hasAllItems();
    }
    public void areAllGuardiansAwakened() {
        foreach (GuardianController guardian in FindObjectsOfType<GuardianController>()) {
            if (!guardian.isAwakened) {
                return;
            }
        }
        InitiateGoddessTrader();
        //FindObjectOfType<InventoryController>().CollectedItem("Tomato");
        //SceneManager.LoadScene("HubScene");
    }

    public void InitiateGoddessTrader() {
        goddessDialogue.SetActive(false);
        goddessTrader.SetActive(true);
    }

    public void hasAllItems() {
        List<string> requiredItemsCopy = requiredItems.ToList();
        int i;
        foreach (string item in InventoryController.items) {
            i=0;
            foreach (string tmp in requiredItemsCopy) {
                if (item == tmp) {
                    requiredItemsCopy.RemoveAt(i);
                    Debug.Log("removing item...");
                    break;
                }

                i++;
            }
        }
        
        string result = "";
        foreach (string item in requiredItemsCopy) {
            result += ", " + item;
        }
        Debug.Log($"{result}");
        if (result == "") {
            Debug.Log("Collected all items");
            winText.SetActive(true);
        }
    }
}
