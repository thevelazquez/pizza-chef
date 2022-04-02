using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseMenu : MonoBehaviour
{
        public GameObject pauseCanvasMenu;
        public bool isPaused;
    // Start is called before the first frame update
 

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown("p"))
            {
                Debug.Log("Test");
                if(isPaused)
                {
                    CloseMenu();
                }
                else
                {
                        OpenMenu();
                }
        }
        
    }
public void OpenMenu()
    {
        pauseCanvasMenu.SetActive(true);
        isPaused = true;
    }
public void CloseMenu()
    {
        pauseCanvasMenu.SetActive(false);
        isPaused = false;
    }
}
