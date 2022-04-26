using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Level1Manager : MonoBehaviour
{
    public GameObject collectibles;
    public GameObject winDisplay;
    
    void Update() {
        CheckWinCondition();
    }

    void CheckWinCondition() {
        foreach (Transform child in collectibles.transform) {
            if (child.gameObject.activeInHierarchy) {
                return;
            }
        }
        Win();
    }

    void Win() {
        winDisplay.SetActive(true);
    }
}
