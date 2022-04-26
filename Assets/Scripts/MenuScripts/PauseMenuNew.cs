using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenuNew : MonoBehaviour
{
    public static bool isPaused = false;
    public GameObject aboutScreen;
    public GameObject creditScreen;
    public GameObject controlScreen;
    public GameObject genScreen;
    public GameObject UIScreen;
    //public GameObject InvScreen;

    public GameObject Menu;

    Canvas MenuCanvas;

    // Start is called before the first frame update
    void Start()
    {
        Resume();
        MenuCanvas = Menu.GetComponent<Canvas>();
        // invDisplay.enabled = false; 
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.P))
        {
            if (isPaused)
            {
                UIScreen.SetActive(false);
                HideCursor();
                Resume();
            }
            else
            {
                UIScreen.SetActive(true);
                //InvScreen.SetActive(false);
                ActivateCursor();
                Pause();
            }
        }
        /*else if (Input.GetKeyDown(KeyCode.I))
        {
            if (isPaused)
            {
                InvScreen.SetActive(false);
                HideCursor();
                Resume();
            }
            else
            {
                InvScreen.SetActive(true);
                UIScreen.SetActive(false);
                ActivateCursor();
                Pause();
            }
            /* if (invDisplay.enabled) 
             {
                 invDisplay.enabled = false;
                 HideCursor();

             }
             else 
             {
                 invDisplay.enabled = true;
                 ActivateCursor();
             }
            */
        //}
    }

    public void Resume()
    {
        Time.timeScale = 1f;
        isPaused = false;
        UIScreen.SetActive(false);
        HideCursor();
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
        ActivateCursor();
    }

    public void ActivateCursor()
    {
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }

    public void HideCursor()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
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
