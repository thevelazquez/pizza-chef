using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Level2Manager : MonoBehaviour
{
    public GameObject player;
    [SerializeField]
    float killFloorY;
    void Update()
    {
        if (player.transform.position.y < killFloorY) {
            SceneManager.LoadScene("Level2");
        }
    }
    public void areAllGuardiansAwakened() {
        foreach (GuardianController guardian in FindObjectsOfType<GuardianController>()) {
            if (!guardian.isAwakened) {
                return;
            }
        }
        FindObjectOfType<InventoryController>().CollectedItem("Tomato");
        SceneManager.LoadScene("HubScene");
    }
}
