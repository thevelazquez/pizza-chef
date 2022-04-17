using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AUIController : MonoBehaviour
{
    Canvas AUICanvas;
    
    // Start is called before the first frame update
    void Start()
    {
        AUICanvas = GetComponent<Canvas>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.I)) {
            if (AUICanvas.enabled) {
                AUICanvas.enabled = false;
            } else {
                AUICanvas.enabled = true;
            }
        }
        if (PauseMenuNew.isPaused) {
            AUICanvas.enabled = false;
        }
    }
}
