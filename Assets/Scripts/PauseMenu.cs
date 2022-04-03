using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    public static bool isPaused = false;
    public GameObject aboutScreen;
    public GameObject creditScreen;
    public GameObject controlScreen;
    public GameObject genScreen;
    public GameObject UIScreen;
    public GameObject InventoryCanvas;

    // Start is called before the first frame update
    void Start()
    {
        Resume();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.P))
        {
            if (isPaused)
            {
                Resume();
            }
            else
            {
                Pause();
            }
        }

        if (Input.GetKeyDown(KeyCode.I)) 
        {
            Canvas invDisplay = InventoryCanvas.GetComponent<Canvas>();
            
            if (invDisplay.enabled) 
            {
                invDisplay.enabled = false;
            } 
            else 
            {
                invDisplay.enabled = true;
            }
        }   
    }

    public void Resume()
    {
        Time.timeScale = 1f;
        isPaused = false;
        UIScreen.SetActive(false);
    }

    public void Pause()
    {
        Time.timeScale = 0f;
        isPaused = true;
        UIScreen.SetActive(true);
        genScreen.SetActive(true);
        aboutScreen.SetActive(false);
        creditScreen.SetActive(false);
        controlScreen.SetActive(false);
        
        Cursor.lockState = CursorLockMode.None;
    }
    
    public void GoToMM()
    {
        SceneManager.LoadScene("MainMenu");
        Time.timeScale = 1f;
    }

    public void LoadAbout()
    {
        genScreen.SetActive(false);
        aboutScreen.SetActive(true);
    }

    public void LoadControls()
    {
        Debug.Log("Show Controls");
        aboutScreen.SetActive(false);
        controlScreen.SetActive(true);
    }

    public void LoadCredits()
    {
        Debug.Log("Show Credits");
        aboutScreen.SetActive(false);
        creditScreen.SetActive(true);
    }

    public void ReturntoAbout()
    {
        controlScreen.SetActive(false);
        creditScreen.SetActive(false);
        aboutScreen.SetActive(true);
    }

    public void ReturntoPause()
    {
        aboutScreen.SetActive(false);
        genScreen.SetActive(true);
    }

    public void QuitGame()
    {
        Debug.Log("Quit Game");
        Application.Quit();
    }
}
