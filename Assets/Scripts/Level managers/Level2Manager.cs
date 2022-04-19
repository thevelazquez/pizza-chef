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
}
