using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PauseMenuNew : MonoBehaviour
{
    public static bool isPaused = false;
    public GameObject aboutScreen;
    public GameObject creditScreen;
    public GameObject controlScreen;
    public GameObject optionScreen;
    public GameObject genScreen;
    public GameObject HealthScreen;
    public GameObject ObjectiveScreen;
    public GameObject UIScreen;
    public DialogueManager dialogue;
    //public GameObject InvScreen;
    public AudioMixer audioMixer;
    public Dropdown resolutionDropdown;
    Resolution[] resolutions;

    public GameObject Menu;

    Canvas MenuCanvas;

    // Start is called before the first frame update
    void Start()
    {
        resolutions = Screen.resolutions;

        resolutionDropdown.ClearOptions();
        
        List<string> options = new List<string>();

        int currentResolutionIndex = 0;

        for (int i = 0; i < resolutions.Length; i++)
        {
            string option = resolutions[i].width + " x " + resolutions[i].height;
            options.Add(option);

            if (resolutions[i].width == Screen.currentResolution.width && resolutions[i].height == Screen.currentResolution.height)
            {
                currentResolutionIndex = i;
            }
        }
        
        resolutionDropdown.AddOptions(options);
        resolutionDropdown.value = currentResolutionIndex;
        resolutionDropdown.RefreshShownValue();
        
        Resume();
        MenuCanvas = Menu.GetComponent<Canvas>();
        // invDisplay.enabled = false; 
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.P) | Input.GetButtonDown("Menu Button"))
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
        HealthScreen.SetActive(true);
        ObjectiveScreen.SetActive(true);
        dialogue.Show();
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
        optionScreen.SetActive(false);
        HealthScreen.SetActive(false);
        ObjectiveScreen.SetActive(false);
        dialogue.Hide();
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

    public void LoadOptions()
    {
        genScreen.SetActive(false);
        optionScreen.SetActive(true);
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
        optionScreen.SetActive(false);
        genScreen.SetActive(true);
    }

    public void SetVolume (float volume)
    {
        audioMixer.SetFloat("Volume", volume);
    }

    public void SetResolution (int resolutionIndex)
    {
        Resolution resolution = resolutions[resolutionIndex];
        Screen.SetResolution(resolution.width, resolution.height, Screen.fullScreen);
    }

    public void SetFullScreen (bool isFullScreen)
    {
        Screen.fullScreen = isFullScreen;
    }

    public void QuitGame()
    {
        Debug.Log("Quit Game");
        Application.Quit();
    }
}
