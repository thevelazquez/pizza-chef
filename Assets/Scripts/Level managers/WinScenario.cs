using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System.Linq;

public class WinScenario : MonoBehaviour
{
    public List<string> requiredItems;

    // Update is called once per frame
    void Update()
    {
        hasAllItems();
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
            StartCoroutine(BackToHub());
        }
    }

    IEnumerator BackToHub() {
        yield return new WaitForSeconds(2);
        SceneManager.LoadScene("WinMenu");
    }
}
