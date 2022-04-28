using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public GameObject aboutScreen;
    public GameObject creditScreen;
    public GameObject controlScreen;
    public GameObject optionScreen;
    public GameObject genScreen;
    public AudioMixer audioMixer;
    public Dropdown resolutionDropdown;
    Resolution[] resolutions;
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

        aboutScreen.SetActive(false);
        creditScreen.SetActive(false);
        controlScreen.SetActive(false);
        optionScreen.SetActive(false);
    }

    public void StartGame()
    {
        InventoryController.items.Clear();
        SceneManager.LoadScene("HubScene");
    }


    public void LoadAboutScreen()
    {
        genScreen.SetActive(false);
        aboutScreen.SetActive(true);
    }

    public void LoadCreditScreen()
    {
        aboutScreen.SetActive(false);
        creditScreen.SetActive(true);
    }

    public void LoadControlsScreen()
    {
        aboutScreen.SetActive(false);
        controlScreen.SetActive(true);
    }

    public void LoadOptionsScreen()
    {
        aboutScreen.SetActive(false);
        optionScreen.SetActive(true);
    }

    public void ReturntoAbout()
    {
        controlScreen.SetActive(false);
        creditScreen.SetActive(false);
        aboutScreen.SetActive(true);
    }

    public void ReturntoGenScreen()
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
