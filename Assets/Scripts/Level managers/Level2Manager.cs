using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Level2Manager : MonoBehaviour
{
    public GameObject player;
    [SerializeField]
    float killFloorY;
    public GameObject goddessDialogue;
    public GameObject goddessTrader;
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
        InitiateGoddessTrader();
        //FindObjectOfType<InventoryController>().CollectedItem("Tomato");
        //SceneManager.LoadScene("HubScene");
    }

    public void InitiateGoddessTrader() {
        goddessDialogue.SetActive(false);
        goddessTrader.SetActive(true);
    }
}
